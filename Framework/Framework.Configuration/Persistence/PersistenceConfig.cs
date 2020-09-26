using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core.Contracts.Services;
using Framework.Persistences.Contracts.UnitOfWork;
using Framework.Persistences.EF.UnitOfWork;

namespace Framework.Configuration.Persistence
{
	internal class PersistenceConfig : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Component.For<IUnitOfWork>()
					.ImplementedBy<UnitOfWork>()
					.LifestyleBoundToNearest<IService>()
			);
		}
	}
}