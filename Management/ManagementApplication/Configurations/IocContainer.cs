using Castle.Windsor;
using Castle.Windsor.Installer;
using Framework.Core.Common.CastleWinsdor;
using Framework.Core.Contracts.ServiceLocator;
using Framework.Core.ServiceLocator;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace ManagementApplication.Configurations
{
	public class IocContainer
	{
		private static IWindsorContainer _container;

		public static void Setup()
		{
			_container = new WindsorContainer();

			Framework.Configuration.Bootstrap.Initialize(_container);
			Management.Infrastructure.Configuration.Bootstrap.Initialize(_container);

			_container.Install(FromAssembly.This());

			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new CastleApiControllerActivator(_container));

			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_container));

			ServiceLocator.Current = new WindsorServiceLocator(_container);
		}
	}
}