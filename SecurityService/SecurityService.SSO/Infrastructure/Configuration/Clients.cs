/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace SecurityService.SSO.Infrastructure.Configuration
{
	public class Clients
	{
		//

		public static List<Client> Get()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "ADM",
					ClientName = "Administration App",
					Enabled = true,
					ClientSecrets = new List<Secret>
					{
						new Secret("123".Sha256())
					},

					Flow = Flows.Implicit,

					AllowedScopes = new List<string>
					{
						Constants.StandardScopes.OpenId,
						Constants.StandardScopes.Profile,
						Constants.StandardScopes.Email,
						Constants.StandardScopes.Roles,
						Constants.StandardScopes.Address,
						Constants.StandardScopes.OfflineAccess,
						"admgr"
					},
					ClientUri = "http://localhost:20391/" ,
					RequireConsent = false,
					AllowRememberConsent = true,
					RequireSignOutPrompt = true,

					RedirectUris = new List<string>
					{
						"http://localhost:20391/",
					},

					PostLogoutRedirectUris = new List<string>
					{
						"http://localhost:20391/",
					},

					AllowedCorsOrigins = new List<string>{
						"http://localhost:20391/",
					},

					LogoutUri = "http://localhost:20391/Home/SignoutCleanup",
					LogoutSessionRequired = true,

					IdentityTokenLifetime = 360,
					AccessTokenLifetime = 3600,
					AccessTokenType = AccessTokenType.Reference
				},
				new Client
				{
					ClientId = "AppStore",
					ClientName = "Application Store",
					Enabled = true,
					ClientSecrets = new List<Secret>
					{
						new Secret("123".Sha256())
					},

					Flow = Flows.Implicit,

					AllowedScopes = new List<string>
					{
						Constants.StandardScopes.OpenId,
						Constants.StandardScopes.Profile,
						Constants.StandardScopes.Email,
						Constants.StandardScopes.Address,
						Constants.StandardScopes.OfflineAccess
					},
					ClientUri = "http://localhost:16161/" ,
					RequireConsent = false,
					AllowRememberConsent = true,
					RequireSignOutPrompt = true,

					RedirectUris = new List<string>
					{
						"http://localhost:16161/",
					},

					PostLogoutRedirectUris = new List<string>
					{
						"http://localhost:16161/",
					},

					AllowedCorsOrigins = new List<string>{
						"http://localhost:16161/",
					},

					LogoutUri = "http://localhost:16161/Home/SignoutCleanup",
					LogoutSessionRequired = true,

					IdentityTokenLifetime = 360,
					AccessTokenLifetime = 3600,
					AccessTokenType = AccessTokenType.Reference
				},
				new Client
				{
					ClientId = "RIMS",
					ClientName = "RIMS App",
					Enabled = true,
					ClientSecrets = new List<Secret>
					{
						new Secret("123".Sha256())
					},

					Flow = Flows.Implicit,

					AllowedScopes = new List<string>
					{
						Constants.StandardScopes.OpenId,
						Constants.StandardScopes.Profile,
						Constants.StandardScopes.Email,
						Constants.StandardScopes.Roles,
						Constants.StandardScopes.Address,
						Constants.StandardScopes.OfflineAccess
					},
					ClientUri = "http://localhost:13272/" ,
					RequireConsent = false,
					AllowRememberConsent = true,
					RequireSignOutPrompt = true,

					RedirectUris = new List<string>
					{
						"http://localhost:13272/",
					},

					PostLogoutRedirectUris = new List<string>
					{
						"http://localhost:13272/",
					},

					AllowedCorsOrigins = new List<string>{
						"http://localhost:13272/",
					},

					LogoutUri = "http://localhost:13272/Home/SignoutCleanup",
					LogoutSessionRequired = true,

					IdentityTokenLifetime = 360,
					AccessTokenLifetime = 3600,
					AccessTokenType = AccessTokenType.Reference
				}
			};
		}
	}
}