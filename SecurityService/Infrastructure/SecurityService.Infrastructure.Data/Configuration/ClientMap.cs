using SecurityService.Infrastructure.Models;
using System.Data.Entity.ModelConfiguration;

namespace SecurityService.Infrastructure.Data.Configuration
{
	public class ClientMap : EntityTypeConfiguration<Client>
	{
		public ClientMap()
		{
			// Primary Key
			this.HasKey(t => t.Id);

			// Properties
			this.Property(t => t.ClientId)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.ClientName)
				.IsRequired()
				.HasMaxLength(200);

			this.Property(t => t.ClientUri)
				.HasMaxLength(2000);

			// Table & Column Mappings
			this.ToTable("Clients");
			this.Property(t => t.Id).HasColumnName("Id");
			this.Property(t => t.Enabled).HasColumnName("Enabled");
			this.Property(t => t.ClientId).HasColumnName("ClientId");
			this.Property(t => t.ClientName).HasColumnName("ClientName");
			this.Property(t => t.ClientUri).HasColumnName("ClientUri");
			this.Property(t => t.LogoUri).HasColumnName("LogoUri");
			this.Property(t => t.RequireConsent).HasColumnName("RequireConsent");
			this.Property(t => t.AllowRememberConsent).HasColumnName("AllowRememberConsent");
			this.Property(t => t.AllowAccessTokensViaBrowser).HasColumnName("AllowAccessTokensViaBrowser");
			this.Property(t => t.Flow).HasColumnName("Flow");
			this.Property(t => t.AllowClientCredentialsOnly).HasColumnName("AllowClientCredentialsOnly");
			this.Property(t => t.LogoutUri).HasColumnName("LogoutUri");
			this.Property(t => t.LogoutSessionRequired).HasColumnName("LogoutSessionRequired");
			this.Property(t => t.RequireSignOutPrompt).HasColumnName("RequireSignOutPrompt");
			this.Property(t => t.AllowAccessToAllScopes).HasColumnName("AllowAccessToAllScopes");
			this.Property(t => t.IdentityTokenLifetime).HasColumnName("IdentityTokenLifetime");
			this.Property(t => t.AccessTokenLifetime).HasColumnName("AccessTokenLifetime");
			this.Property(t => t.AuthorizationCodeLifetime).HasColumnName("AuthorizationCodeLifetime");
			this.Property(t => t.AbsoluteRefreshTokenLifetime).HasColumnName("AbsoluteRefreshTokenLifetime");
			this.Property(t => t.SlidingRefreshTokenLifetime).HasColumnName("SlidingRefreshTokenLifetime");
			this.Property(t => t.RefreshTokenUsage).HasColumnName("RefreshTokenUsage");
			this.Property(t => t.UpdateAccessTokenOnRefresh).HasColumnName("UpdateAccessTokenOnRefresh");
			this.Property(t => t.RefreshTokenExpiration).HasColumnName("RefreshTokenExpiration");
			this.Property(t => t.AccessTokenType).HasColumnName("AccessTokenType");
			this.Property(t => t.EnableLocalLogin).HasColumnName("EnableLocalLogin");
			this.Property(t => t.IncludeJwtId).HasColumnName("IncludeJwtId");
			this.Property(t => t.AlwaysSendClientClaims).HasColumnName("AlwaysSendClientClaims");
			this.Property(t => t.PrefixClientClaims).HasColumnName("PrefixClientClaims");
			this.Property(t => t.AllowAccessToAllGrantTypes).HasColumnName("AllowAccessToAllGrantTypes");

			// Relationships
			this.HasMany(t => t.Roles)
				.WithMany(t => t.Clients)
				.Map(m =>
					{
						m.ToTable("ClientRoles");
						m.MapLeftKey("ClientID");
						m.MapRightKey("RoleID");
					});

			this.HasMany(t => t.Permissions)
				.WithMany(t => t.Clients)
				.Map(m =>
				{
					m.ToTable("ClientPermissions");
					m.MapLeftKey("ClientID");
					m.MapRightKey("PermissionID");
				});

			this.HasMany(t => t.Users)
				.WithMany(t => t.Clients)
				.Map(m =>
					{
						m.ToTable("ClientUsers");
						m.MapLeftKey("ClientID");
						m.MapRightKey("UserID");
					});
		}
	}
}