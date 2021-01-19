using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using MvcViewServiceSample;
using Owin;
using SecurityService.Infrastructure.Data;
using SecurityService.SSO.Controllers;
using SecurityService.SSO.IdentityService;
using SecurityService.SSO.Infrastructure;
using SecurityService.SSO.Infrastructure.Configuration;
using SecurityService.SSO.Services;
using Serilog;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Http;
using SecurityService.SSO.App_Start;

namespace SecurityService.SSO
{
   
    internal class Startup
	{
		public void Configuration(IAppBuilder app)
		{
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            var url = ConfigurationManager.AppSettings["IdentityServerURI"];
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



            #region IdentityServiceMVC Configuration
            //var factory = new IdentityServerServiceFactory()
            //          .UseInMemoryUsers(Users.Get())
            //          .UseInMemoryClients(Clients.Get())
            //          .UseInMemoryScopes(Scopes.Get());

            //// Use the Mvc View Service instead of the default
            //factory.ViewService = new Registration<IViewService, MvcViewService<LogonWorkflowController>>();

            //// These registrations are also needed since these are dealt with using non-standard construction
            //factory.Register(new Registration<HttpContext>(resolver => HttpContext.Current));
            //factory.Register(new Registration<HttpContextBase>(resolver => new HttpContextWrapper(resolver.Resolve<HttpContext>())));
            //factory.Register(new Registration<HttpRequestBase>(resolver => resolver.Resolve<HttpContextBase>().Request));
            //factory.Register(new Registration<HttpResponseBase>(resolver => resolver.Resolve<HttpContextBase>().Response));
            //factory.Register(new Registration<HttpServerUtilityBase>(resolver => resolver.Resolve<HttpContextBase>().Server));
            //factory.Register(new Registration<HttpSessionStateBase>(resolver => resolver.Resolve<HttpContextBase>().Session));



            //var options = new IdentityServerOptions
            //{
            //    SigningCertificate = Certificate.LoadCertificate(),
            //    Factory = factory
            //};

            //app.Map("/core", idsrvApp =>
            //{
            //    idsrvApp.UseIdentityServer(options);
            //});
            #endregion
            #region IdentityService Configuration

            app.Map("/core", identity =>
            {
                IdentityServerServiceFactory idSvrFactory = IdentityServiceFactory.Configure(strConnectionString);

                idSvrFactory.ViewService =
                new Registration<IViewService, MvcViewService<LogonWorkflowController>>();
              // new Registration<IViewService>(typeof(ApplicationViewService));
                idSvrFactory.CorsPolicyService =
                    new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });

                idSvrFactory.ConfigureIdentityUserService(strConnectionString);

                // These registrations are also needed since these are dealt with using non-standard construction
                idSvrFactory.Register(new Registration<HttpContext>(resolver => HttpContext.Current));
                idSvrFactory.Register(new Registration<HttpContextBase>(resolver => new HttpContextWrapper(resolver.Resolve<HttpContext>())));
                idSvrFactory.Register(new Registration<HttpRequestBase>(resolver => resolver.Resolve<HttpContextBase>().Request));
                idSvrFactory.Register(new Registration<HttpResponseBase>(resolver => resolver.Resolve<HttpContextBase>().Response));
                idSvrFactory.Register(new Registration<HttpServerUtilityBase>(resolver => resolver.Resolve<HttpContextBase>().Server));
                idSvrFactory.Register(new Registration<HttpSessionStateBase>(resolver => resolver.Resolve<HttpContextBase>().Session));

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
            var httpconfig = new HttpConfiguration();
            WebApiConfig.Register(app, url, httpconfig);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            #endregion IdentityService Configuration
        }
    }
}