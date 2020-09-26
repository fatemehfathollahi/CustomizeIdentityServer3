using System.Collections.Generic;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientDTO
	{
		public ClientDTO()
		{
			ClientClaims = new List<ClientClaimDTO>();
			ClientCorsOrigins = new List<ClientCorsOriginDTO>();
			ClientCustomGrantTypes = new List<ClientCustomGrantTypeDTO>();
			ClientIdPRestrictions = new List<ClientIdPRestrictionDTO>();
			ClientPostLogoutRedirectUris = new List<ClientPostLogoutRedirectUriDTO>();
			ClientRedirectUris = new List<ClientRedirectUriDTO>();
			ClientScopes = new List<ClientScopeDTO>();
			ClientSecrets = new List<ClientSecretDTO>();
			Roles = new List<RoleDTO>();
			Users = new List<UserDTO>();
			Permissions = new List<PermissionDTO>();
			UserPermissions = new List<UserClientPermissionDTO>();

			Enabled = true;
			RequireConsent = false;
			AllowRememberConsent = true;
			AllowAccessToAllGrantTypes = true;
			AllowAccessTokensViaBrowser = true;
			Flow = 1;
			AllowClientCredentialsOnly = false;
			LogoutSessionRequired = true;
			RequireSignOutPrompt = true;
			AllowAccessToAllScopes = false;
			SlidingRefreshTokenLifetime = 1296000;
			RefreshTokenUsage = 1;
			UpdateAccessTokenOnRefresh = false;
			RefreshTokenExpiration = 1;
			AccessTokenType = 1;
			EnableLocalLogin = false;
			IncludeJwtId = false;
			AlwaysSendClientClaims = false;
			PrefixClientClaims = true;
			EnableLocalLogin = true;
		}

		public int Id { get; set; }
		public bool Enabled { get; set; }  //
		public string EnableDescription => Enabled ? "Active" : "Dont Active";
		public string ClientId { get; set; } //
		public string ClientName { get; set; } //
		public string ClientUri { get; set; }    //
		public byte[] LogoUri { get; set; }        //
		public bool RequireConsent { get; set; }     //false
		public bool AllowRememberConsent { get; set; }      //true
		public bool AllowAccessTokensViaBrowser { get; set; }     //true
		public int Flow { get; set; }   //1
		public bool AllowClientCredentialsOnly { get; set; } //true
		public string LogoutUri { get; set; }   //
		public bool LogoutSessionRequired { get; set; } //true
		public bool RequireSignOutPrompt { get; set; }   //true
		public bool AllowAccessToAllScopes { get; set; }   //
		public int IdentityTokenLifetime { get; set; }  // sanie  300
		public int AccessTokenLifetime { get; set; }   //saniye	  3600
		public int AuthorizationCodeLifetime { get; set; }   //saniye 300
		public int AbsoluteRefreshTokenLifetime { get; set; }  //saniye   2592000s
		public int SlidingRefreshTokenLifetime { get; set; } //1296000s
		public int RefreshTokenUsage { get; set; }
		public bool UpdateAccessTokenOnRefresh { get; set; } //true
		public int RefreshTokenExpiration { get; set; }
		public int AccessTokenType { get; set; }//0
		public bool EnableLocalLogin { get; set; } //false
		public bool IncludeJwtId { get; set; } //false
		public bool AlwaysSendClientClaims { get; set; }//false
		public bool PrefixClientClaims { get; set; }//true
		public bool AllowAccessToAllGrantTypes { get; set; } //false
		public virtual ICollection<ClientClaimDTO> ClientClaims { get; set; }
		public virtual ICollection<ClientCorsOriginDTO> ClientCorsOrigins { get; set; }
		public virtual ICollection<ClientCustomGrantTypeDTO> ClientCustomGrantTypes { get; set; }
		public virtual ICollection<ClientIdPRestrictionDTO> ClientIdPRestrictions { get; set; }
		public virtual ICollection<ClientPostLogoutRedirectUriDTO> ClientPostLogoutRedirectUris { get; set; }
		public virtual ICollection<ClientRedirectUriDTO> ClientRedirectUris { get; set; }
		public virtual ICollection<ClientScopeDTO> ClientScopes { get; set; }
		public virtual ICollection<ClientSecretDTO> ClientSecrets { get; set; }
		public virtual ICollection<PermissionDTO> Permissions { get; set; }
		public virtual ICollection<RoleDTO> Roles { get; set; }
		public virtual ICollection<UserDTO> Users { get; set; }
		public virtual ICollection<UserClientPermissionDTO> UserPermissions { get; set; }
		public int TotalCount { get; set; }
		public bool PhotoChanged { get; set; }
	}
}