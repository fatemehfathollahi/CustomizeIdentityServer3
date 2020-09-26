using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.EntityFramework;
using SecurityService.SSO.Infrastructure.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SecurityService.SSO.IdentityService
{
	internal class IdentityServiceFactory
	{
		public static IdentityServerServiceFactory Configure(string connString)
		{
			IdentityServerServiceFactory factory = new IdentityServerServiceFactory();

			EntityFrameworkServiceOptions efConfig = new EntityFrameworkServiceOptions
			{
				ConnectionString = connString,
			};

			// these two calls just pre-populate the test DB from the in-memory config
			ConfigureClients(Clients.Get(), efConfig);
			ConfigureScopes(Scopes.Get(), efConfig);

			factory.RegisterConfigurationServices(efConfig);
			factory.RegisterOperationalServices(efConfig);

			return factory;
		}

		public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
		{
			using (ClientConfigurationDbContext db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
			{
				if (!db.Clients.Any())
				{
					foreach (Client c in clients)
					{
						IdentityServer3.EntityFramework.Entities.Client e = c.ToEntity();
						db.Clients.Add(e);
					}
					db.SaveChanges();
				}
			}
		}

		public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
		{
			using (ScopeConfigurationDbContext db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
			{
				if (!db.Scopes.Any())
				{
					foreach (Scope s in scopes)
					{
						IdentityServer3.EntityFramework.Entities.Scope e = s.ToEntity();
						db.Scopes.Add(e);
					}
					db.SaveChanges();
				}
			}
		}
	}
}