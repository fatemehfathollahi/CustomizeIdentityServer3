using Castle.Windsor;
using Management.Infrastructure.Configuration.Data;
using Management.Infrastructure.Configuration.Facade;
using Management.Infrastructure.Configuration.Service;

namespace Management.Infrastructure.Configuration
{
	public static class Bootstrap
	{
		public static void Initialize(IWindsorContainer container)
		{
			container.Register(
				new DataConfig(),
				new ServiceConfig(),
				new FacadeConfig()
			);
		}
	}
}