using Framework.Core.Contracts.Model;
using System.Collections.Generic;

namespace Management.Infrastructure.Models
{
	public partial class Client : IEntityObject
	{
		public Client()
		{
			ClientClaims = new List<ClientClaim>();
			ClientCorsOrigins = new List<ClientCorsOrigin>();
			ClientCustomGrantTypes = new List<ClientCustomGrantType>();
			ClientIdPRestrictions = new List<ClientIdPRestriction>();
			ClientPostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>();
			ClientRedirectUris = new List<ClientRedirectUri>();
			ClientScopes = new List<ClientScope>();
			ClientSecrets = new List<ClientSecret>();
			Permissions = new List<Permission>();
			Roles = new List<Role>();
			Users = new List<User>();
			UserPermissions = new List<UserClientPermission>();
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
		public virtual ICollection<Permission> Permissions { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<UserClientPermission> UserPermissions { get; set; }
	}
}