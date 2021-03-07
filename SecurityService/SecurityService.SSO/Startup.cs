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
using System.Collections.Generic;

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
<<<<<<< HEAD
            

=======
           
>>>>>>> 0d20e5961a45f9fbdc3d8f4865af2742226075e2
            app.Map("/core", identity =>
            {
                IdentityServerServiceFactory idSvrFactory = IdentityServiceFactory.Configure(strConnectionString);


                //config view -- fathollahi
                idSvrFactory.ViewService =
<<<<<<< HEAD
               new Registration<IViewService, MvcViewService<LogonWorkflowController>>();//  use mvc view .fathollahi
              // new Registration<IViewService>(typeof(ApplicationViewService)); // use angular view  .fathollahi
=======
             // new Registration<IViewService, MvcViewService<LogonWorkflowController>>(); // use mvc view  
              new Registration<IViewService>(typeof(ApplicationViewService));  // use angular view - if want to use this view comment top line and uncomment this view




>>>>>>> 0d20e5961a45f9fbdc3d8f4865af2742226075e2
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
<<<<<<< HEAD
                    //AuthenticationOptions = new AuthenticationOptions
                    //{
                    //    LoginPageLinks = new List<LoginPageLink>()
                    //  {
                    //   new LoginPageLink()
                    //   {
                    //       Href = "ResetPassword",
                    //       Text = "Reset Your Password",
                    //       Type = "resetPassword"
                    //   }
                    //   }
                    //},
=======
                    AuthenticationOptions  = new AuthenticationOptions
                    {
                        LoginPageLinks = new List<LoginPageLink>()
                      {
                       new LoginPageLink()
                       {
                           Href = "resetpassword",
                           Text = "Reset Your Password",
                           Type = "resetPassword"
                       }
                       }
                    },
>>>>>>> 0d20e5961a45f9fbdc3d8f4865af2742226075e2
                    RequireSsl = false,
                    SiteName = "IdentityServer",
                    SigningCertificate = Certificate.LoadCertificate(),
                    Factory = idSvrFactory,
                    EnableWelcomePage = false
                };
                identity.UseIdentityServer(options);
            });
            var httpconfig = new HttpConfiguration();
            WebApiConfig.Register(app, url, httpconfig); // register for web route
            RouteConfig.RegisterRoutes(RouteTable.Routes); //register for api route

            #endregion IdentityService Configuration
        }
    }
}