using System.Collections.Generic;

namespace SecurityService.Infrastructure.Models
{
	public partial class Client
	{
		public Client()
		{
			this.ClientClaims = new List<ClientClaim>();
			this.ClientCorsOrigins = new List<ClientCorsOrigin>();
			this.ClientCustomGrantTypes = new List<ClientCustomGrantType>();
			this.ClientIdPRestrictions = new List<ClientIdPRestriction>();
			this.ClientPostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>();
			this.ClientRedirectUris = new List<ClientRedirectUri>();
			this.ClientScopes = new List<ClientScope>();
			this.ClientSecrets = new List<ClientSecret>();
			this.Permissions = new List<ApplicationPermission>();
			this.Roles = new List<ApplicationRoles>();
			this.Users = new List<ApplicationUser>();
			this.UserPermissions = new List<UserClientPermissions>();
		}

		public int Id { get; set; }
		public bool Enabled { get; set; }
		public string ClientId { get; set; }
		public string ClientName { get; set; }
		public string ClientUri { get; set; }
		public string LogoUri { get; set; }
		public bool RequireConsent { get; set; }
		public bool AllowRememberConsent { get; set; }
		public bool AllowAccessTokensViaBrowser { get; set; }
		public int Flow { get; set; }
		public bool AllowClientCredentialsOnly { get; set; }
		public string LogoutUri { get; set; }
		public bool LogoutSessionRequired { get; set; }
		public bool RequireSignOutPrompt { get; set; }
		public bool AllowAccessToAllScopes { get; set; }
		public int IdentityTokenLifetime { get; set; }
		public int AccessTokenLifetime { get; set; }
		public int AuthorizationCodeLifetime { get; set; }
		public int AbsoluteRefreshTokenLifetime { get; set; }
		public int SlidingRefreshTokenLifetime { get; set; }
		public int RefreshTokenUsage { get; set; }
		public bool UpdateAccessTokenOnRefresh { get; set; }
		public int RefreshTokenExpiration { get; set; }
		public int AccessTokenType { get; set; }
		public bool EnableLocalLogin { get; set; }
		public bool IncludeJwtId { get; set; }
		public bool AlwaysSendClientClaims { get; set; }
		public bool PrefixClientClaims { get; set; }
		public bool AllowAccessToAllGrantTypes { get; set; }
		public virtual ICollection<ClientClaim> ClientClaims { get; set; }
		public virtual ICollection<ClientCorsOrigin> ClientCorsOrigins { get; set; }
		public virtual ICollection<ClientCustomGrantType> ClientCustomGrantTypes { get; set; }
		public virtual ICollection<ClientIdPRestriction> ClientIdPRestrictions { get; set; }
		public virtual ICollection<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }
		public virtual ICollection<ClientRedirectUri> ClientRedirectUris { get; set; }
		public virtual ICollection<ClientScope> ClientScopes { get; set; }
		public virtual ICollection<ClientSecret> ClientSecrets { get; set; }
		public virtual ICollection<ApplicationPermission> Permissions { get; set; }

		public virtual ICollection<ApplicationRoles> Roles { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
		public virtual ICollection<UserClientPermissions> UserPermissions { get; set; }
	}
}