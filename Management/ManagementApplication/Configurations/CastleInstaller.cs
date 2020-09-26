using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ManagementApplication.Api;
using System.Web.Http;

namespace ManagementApplication.Configurations
{
	public class CastleInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromAssemblyContaining<BaseApiController>()
								  .BasedOn<ApiController>()
								  .LifestylePerWebRequest());
		}
	}
}