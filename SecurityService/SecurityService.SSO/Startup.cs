using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Owin;
using SecurityService.Infrastructure.Data;
using SecurityService.SSO.IdentityService;
using SecurityService.SSO.Infrastructure;
using SecurityService.SSO.Infrastructure.Configuration;
using Serilog;
using System.Configuration;
using System.Linq;

namespace SecurityService.SSO
{
	internal class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			string strConnectionString = ConfigurationManager.ConnectionStrings["SecurityDBContext"].Name;
			string strLogPath = ConfigurationManager.AppSettings["LogPath"].ToString();

			IdentityContext context = new IdentityContext(strConnectionString);
			context.Clients.ToList();
			context.Dispose();
			context = null;

			#region Log Configuration

			Log.Logger = new LoggerConfiguration()
			   .MinimumLevel.Debug()
			   .WriteTo.RollingFile(strLogPath + "Log-STS-{Date}.log")
			   .CreateLogger();

			#endregion Log Configuration

			#region IdentityService Configuration

			app.Map("/core", identity =>
			{
				IdentityServerServiceFactory idSvrFactory = IdentityServiceFactory.Configure(strConnectionString);

				idSvrFactory.ViewService =
					new Registration<IViewService>(typeof(ApplicationViewService));
				idSvrFactory.CorsPolicyService =
					new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });

				idSvrFactory.ConfigureIdentityUserService(strConnectionString);

				IdentityServerOptions options = new IdentityServerOptions
				{
					RequireSsl = false,
					SiteName = "IdentityServer",
					SigningCertificate = Certificate.LoadCertificate(),
					Factory = idSvrFactory,
					EnableWelcomePage = false
				};

				identity.UseIdentityServer(options);
			});

			#endregion IdentityService Configuration
		}
	}
}