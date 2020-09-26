using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core.Contracts.Services;
using Framework.Crosscutting.Exceptions;
using Framework.Crosscutting.Transactions;

namespace Framework.Configuration.Crosscutting
{
	internal class CrosscuttingConfig : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Component.For<ExceptionInterceptor>()
						 .LifestyleBoundToNearest<IService>(),

				Component.For<TransactionInterceptor>()
						 .LifestyleBoundToNearest<IService>()
			);
		}
	}
}