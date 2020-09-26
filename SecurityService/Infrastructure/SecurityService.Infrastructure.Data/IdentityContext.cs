using Microsoft.AspNet.Identity.EntityFramework;
using SecurityService.Infrastructure.Data.Configuration;
using SecurityService.Infrastructure.Models;
using System.Data.Entity;

namespace SecurityService.Infrastructure.Data
{
	public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationPermission, int, ApplicationUserLogin, ApplicationUserPermission, ApplicationUserClaim>
	{
		static IdentityContext()
		{
			//Database.SetInitializer<IdentityContext>(new IdentityContextInitializer());
		}

		public IdentityContext(string connString)
			: base(connString)
		{
			this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<ApplicationRoles> Groups { get; set; }
		public DbSet<ApplicationUserProfile> UserProfile { get; set; }
		public DbSet<ClientClaim> ClientClaims { get; set; }
		public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
		public DbSet<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
		public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
		public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
		public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<ClientScope> ClientScopes { get; set; }
		public DbSet<ClientSecret> ClientSecrets { get; set; }
		public DbSet<UserClientPermissions> UserClientPermissions { get; set; }
		//
		public DbSet<ApplicationUserPermission> UserPermissions { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new ClientClaimMap());
			modelBuilder.Configurations.Add(new ClientCorsOriginMap());
			modelBuilder.Configurations.Add(new ClientCustomGrantTypeMap());
			modelBuilder.Configurations.Add(new ClientIdPRestrictionMap());
			modelBuilder.Configurations.Add(new ClientPostLogoutRedirectUriMap());
			modelBuilder.Configurations.Add(new ClientRedirectUriMap());
			modelBuilder.Configurations.Add(new ClientMap());
			modelBuilder.Configurations.Add(new ClientScopeMap());
			modelBuilder.Configurations.Add(new ClientSecretMap());
			modelBuilder.Configurations.Add(new UserClientPermissionMap());
			modelBuilder.Configurations.Add<ApplicationUserProfile>(new ApplicationIdentityUserProfileMap());
			modelBuilder.Configurations.Add<ApplicationRoles>(new ApplicationGroupMap());

			modelBuilder.Entity<ApplicationUser>().ToTable("Users");
			modelBuilder.Entity<ApplicationPermission>().ToTable("Permissions");
			modelBuilder.Entity<ApplicationUserPermission>().ToTable("UserPermissions");
			modelBuilder.Entity<ApplicationUserPermission>().Property(o => o.RoleId).HasColumnName("PermissionId");
			modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
			modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");

			modelBuilder.Entity<ApplicationUser>()
				.HasRequired(iu => iu.UserProfile)
				.WithRequiredPrincipal(iup => iup.User)
				.WillCascadeOnDelete();
		}
	}
}