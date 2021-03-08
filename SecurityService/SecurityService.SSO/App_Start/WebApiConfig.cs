using IdentityServer3.Core.Configuration;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace SecurityService.SSO.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(IAppBuilder app, string identityServerUrl, System.Web.Http.HttpConfiguration config)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.Map("/api", inner =>
            {
               
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "v0/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                inner.UseWebApi(config);
            });

            app.Map("/api", inner =>
            {
                //config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "DevApi",
                    routeTemplate: "dev/v0/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                inner.UseWebApi(config);
            });
        }
    }
}