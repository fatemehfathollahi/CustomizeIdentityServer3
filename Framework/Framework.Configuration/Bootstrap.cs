using Castle.Windsor;
using Framework.Configuration.Core;
using Framework.Configuration.Crosscutting;
using Framework.Configuration.Persistence;

namespace Framework.Configuration
{
	public static class Bootstrap
	{
		public static void Initialize(IWindsorContainer container)
		{
			container.Register(
				new CrosscuttingConfig(),
				new CoreConfig(),
				new PersistenceConfig()
			);
		}
	}
}