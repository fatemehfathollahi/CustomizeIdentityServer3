using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core.Contracts.Exceptions;
using Framework.Core.Contracts.Log;
using Framework.Core.Exception;
using Framework.Core.Log;
using Framework.Core.Log.LogStorages;

namespace Framework.Configuration.Core
{
	internal class CoreConfig : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Component.For<ILogStorage>()
							.ImplementedBy<LogStorage>()
							.LifestyleSingleton(),
				//.LifestyleBoundToNearest<ILogFactory>(),

				Component.For<ILogFactory>()
							.ImplementedBy<LogFactory>()
							.LifestyleSingleton(),
				//.LifestyleBoundToNearest<ILogHandler>(),

				Component.For<ILogHandler>()
							.ImplementedBy<LogHandler>()
							.LifestyleSingleton(),
				//.LifestyleBoundToNearest<IExecptionTranslator>(),

				Component.For<IExecptionTranslator>()
							.ImplementedBy<ExceptionTranslator>()
							.LifestyleSingleton(),
				//.LifestyleBoundToNearest<IExceptionHandler>(),

				Component.For<IExceptionHandler>()
							.ImplementedBy<ExceptionHandler>()
			//.LifestyleBoundToNearest<ExceptionInterceptor>()
			);
		}
	}
}