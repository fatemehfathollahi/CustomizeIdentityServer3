using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core.Contracts.Services;
using Framework.Crosscutting.Transactions;
using Management.Infrastructure.Service;

namespace Management.Infrastructure.Configuration.Service
{
	internal class ServiceConfig : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Classes.FromAssemblyContaining<ClientService>()
					.BasedOn<IService>()
					.WithService.FromInterface()
					.LifestyleBoundToNearest<IFacadeService>()
					.Configure(a => a.Interceptors<TransactionInterceptor>())
			);
		}
	}
}