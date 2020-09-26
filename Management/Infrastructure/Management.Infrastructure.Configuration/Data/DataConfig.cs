using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core.Common.CastleWinsdor;
using Framework.Core.Contracts.Repository;
using Framework.Core.Contracts.Services;
using Framework.Persistences.EF.DataStore;
using Management.Infrastructure.Data;
using Management.Infrastructure.Models.Repositories;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Management.Infrastructure.Configuration.Data
{
	internal class DataConfig : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Component.For<DbConnection>().UsingFactoryMethod<DbConnection>(a =>
				{
					string connectionString = ConfigurationManager.ConnectionStrings["SecurityDBContext"].ConnectionString;
					DbConnection dbConnection = new SqlConnection(connectionString);
					dbConnection.Open();

					return dbConnection;
				})
				.LifestyleScoped<HybridLifeStyleScopeAccessor>()
				.OnDestroy(a =>
				{
					a.Close();
					//a.Dispose();
				}),

				Component.For<DataContext>()
					.ImplementedBy<ManagerDBContext>()
					.UsingFactoryMethod(() =>
					{
						DbConnection dbConnection = kernel.Resolve<DbConnection>();
						return new ManagerDBContext(dbConnection);
					})
				.LifestyleBoundToNearest<IService>(),

				Component.For<QueryDbContext>().LifestylePerWebRequest(),

				Classes.FromAssemblyContaining<ClientClaimRepository>()
					.BasedOn<IRepository>()
					.WithService.FromInterface()
					.LifestyleBoundToNearest<IService>()
			);
		}
	}
}