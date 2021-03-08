using Framework.Persistences.EF.DataStore;
using Management.Infrastructure.Data.Configurations;
using Management.Infrastructure.Models;
using System.Data.Common;
using System.Data.Entity;

namespace Management.Infrastructure.Data
{
	public class QueryDbContext : DataContext
	{
		public DbSet<ClientClaim> ClientClaims { get; set; }
		public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
		public DbSet<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
		public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
		public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
		public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<ClientScope> ClientScopes { get; set; }
		public DbSet<ClientSecret> ClientSecrets { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<ScopeClaim> ScopeClaims { get; set; }
		public DbSet<Scope> Scopes { get; set; }
		public DbSet<ScopeSecret> ScopeSecrets { get; set; }
		public DbSet<UserClaim> UserClaims { get; set; }
		public DbSet<UserLogin> UserLogins { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserClientPermission> UserClientPermissions { get; set; }

		public QueryDbContext() : base("Name=SecurityDBContext")
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public QueryDbContext(DbConnection dbConnection) : base(dbConnection, false)
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new ClientClaimMap());
			modelBuilder.Configurations.Add(new ClientCorsOriginMap());
			modelBuilder.Configurations.Add(new ClientCustomGrantTypeMap());
			modelBuilder.Configurations.Add(new ClientIdPRestrictionMap());
			modelBuilder.Configurations.Add(new ClientPostLogoutRedirectUriMap());
			modelBuilder.Configurations.Add(new ClientRedirectUriMap());
			modelBuilder.Configurations.Add(new ClientMap());
			modelBuilder.Configurations.Add(new ClientScopeMap());
			modelBuilder.Configurations.Add(new ClientSecretMap());
			modelBuilder.Configurations.Add(new PermissionMap());
			modelBuilder.Configurations.Add(new RoleMap());
			modelBuilder.Configurations.Add(new ScopeClaimMap());
			modelBuilder.Configurations.Add(new ScopeMap());
			modelBuilder.Configurations.Add(new ScopeSecretMap());
			modelBuilder.Configurations.Add(new UserClaimMap());
			modelBuilder.Configurations.Add(new UserLoginMap());
			modelBuilder.Configurations.Add(new UserProfileMap());
			modelBuilder.Configurations.Add(new UserMap());
			modelBuilder.Configurations.Add(new UserClientPermissionMap());

			base.OnModelCreating(modelBuilder);
		}

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}