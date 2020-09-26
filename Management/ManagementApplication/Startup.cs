using ManagementApplication.Configurations;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

[assembly: OwinStartup(typeof(ManagementApplication.Startup))]

namespace ManagementApplication
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = "Cookies"
			});

			app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
			{
				ClientId = "ADM",
				Authority = Constants.BaseAddress,
				RedirectUri = Properties.Settings.Default.ClientAddress,
				ResponseType = "id_token token",
				Scope = "openid profile email roles admgr",

				UseTokenLifetime = false,
				SignInAsAuthenticationType = "Cookies",

				ClientSecret = Properties.Settings.Default.ClientSecret,

				Notifications = new OpenIdConnectAuthenticationNotifications
				{
					SecurityTokenValidated = async n =>
					{
						List<Claim> Claims = new List<Claim>();

						// get userinfo data
						var userInfoClient = new UserInfoClient(new Uri(Constants.UserInfoEndpoint), n.ProtocolMessage.AccessToken);

						var userInfo = await userInfoClient.GetAsync();
						userInfo.Claims.ToList().ForEach(ui => Claims.Add(new Claim(ui.Item1, ui.Item2)));

						var nid = new ClaimsIdentity(Claims, n.AuthenticationTicket.Identity.AuthenticationType, "name", "role");

						// keep the id_token for logout
						nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

						// add access token for sample API
						nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

						// keep track of access token expiration
						nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

						n.AuthenticationTicket = new AuthenticationTicket(nid, n.AuthenticationTicket.Properties);
					},

					RedirectToIdentityProvider = n =>
					{
						if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
						{
							var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

							if (idTokenHint != null)
							{
								n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
							}
						}

						return Task.FromResult(0);
					}
				}
			});
		}
	}
}