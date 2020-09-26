using ApplicationStoreApplication.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;

namespace ApplicationStoreApplication.Configurations
{
	public class CastleInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromAssemblyContaining<HomeController>()
								  .BasedOn<ApiController>()
								  .LifestylePerWebRequest());
		}
	}
}