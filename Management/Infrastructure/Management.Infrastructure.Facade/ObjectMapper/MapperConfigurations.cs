using AutoMapper.Configuration;
using Management.Infrastructure.Facade.ObjectMapper.DTO;
using Management.Infrastructure.Facade.ObjectMapper.Model;

namespace Management.Infrastructure.Facade.ObjectMapper
{
	public static class MapperConfigurations
	{
		public static void MapperInitialize(MapperConfigurationExpression config)
		{
			#region Dto Profiles

			config.AddProfile<ClientClaimDTOProfile>();
			config.AddProfile<ClientCorsOriginDTOProfile>();
			config.AddProfile<ClientCustomGrantTypeDTOProfile>();
			config.AddProfile<ClientDTOProfile>();
			config.AddProfile<ClientListDTOProfile>();
			config.AddProfile<ClientIdPRestrictionDTOProfile>();
			config.AddProfile<ClientPostLogoutRedirectUriDTOProfile>();
			config.AddProfile<ClientRedirectUriDTOProfile>();
			config.AddProfile<ClientScopeDTOProfile>();
			config.AddProfile<ClientSecretDTOProfile>();
			config.AddProfile<PermissionDTOProfile>();
			config.AddProfile<PermissionListDTOProfile>();
			config.AddProfile<RoleDTOProfile>();
			config.AddProfile<ScopeClaimDTOProfile>();
			config.AddProfile<ScopeSecretDTOProfile>();
			config.AddProfile<UserClaimDTOProfile>();
			config.AddProfile<UserDTOProfile>();
			config.AddProfile<UserListDTOProfile>();
			config.AddProfile<UserLoginDTOProfile>();
			config.AddProfile<UserProfileDTOProfile>();
			config.AddProfile<ScopeDTOProfile>();
			config.AddProfile<UserClientPermissionDTOProfile>();

			#endregion Dto Profiles

			#region Model Profiles

			config.AddProfile<ClientClaimProfile>();
			config.AddProfile<ClientCorsOriginProfile>();
			config.AddProfile<ClientCustomGrantTypeProfile>();
			config.AddProfile<ClientProfile>();
			config.AddProfile<ClientIdPRestrictionProfile>();
			config.AddProfile<ClientPostLogoutRedirectUriProfile>();
			config.AddProfile<ClientRedirectUriProfile>();
			config.AddProfile<ClientScopeProfile>();
			config.AddProfile<ClientSecretProfile>();
			config.AddProfile<PermissionProfile>();
			config.AddProfile<RoleProfile>();
			config.AddProfile<ScopeClaimProfile>();
			config.AddProfile<ScopeSecretProfile>();
			config.AddProfile<UserClaimProfile>();
			config.AddProfile<UserProfile>();
			config.AddProfile<UserLoginProfile>();
			config.AddProfile<UserProfileProfile>();
			config.AddProfile<ScopeProfile>();
			config.AddProfile<UserClientPermissionProfile>();

			#endregion Model Profiles
		}
	}
}