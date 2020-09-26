using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;
using System;
using AutoMapper;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ClientDTOProfile : Profile
	{
		public ClientDTOProfile()
		{
			CreateMap<ClientDTO, Client>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Enabled, opts => opts.MapFrom(src => src.Enabled))
				.ForMember(desc => desc.ClientId, opts => opts.MapFrom(src => src.ClientId))
				.ForMember(desc => desc.ClientName, opts => opts.MapFrom(src => src.ClientName))
				.ForMember(desc => desc.ClientUri, opts => opts.MapFrom(src => src.ClientUri))
				.ForMember(desc => desc.LogoUri, opts => opts.MapFrom(src => Convert.ToBase64String(src.LogoUri)))
				.ForMember(desc => desc.RequireConsent, opts => opts.MapFrom(src => src.RequireConsent))
				.ForMember(desc => desc.AllowRememberConsent, opts => opts.MapFrom(src => src.AllowRememberConsent))
				.ForMember(desc => desc.AllowAccessTokensViaBrowser, opts => opts.MapFrom(src => src.AllowAccessTokensViaBrowser))
				.ForMember(desc => desc.Flow, opts => opts.MapFrom(src => src.Flow))
				.ForMember(desc => desc.AllowClientCredentialsOnly, opts => opts.MapFrom(src => src.AllowClientCredentialsOnly))
				.ForMember(desc => desc.LogoutUri, opts => opts.MapFrom(src => src.LogoutUri))
				.ForMember(desc => desc.LogoutSessionRequired, opts => opts.MapFrom(src => src.LogoutSessionRequired))
				.ForMember(desc => desc.RequireSignOutPrompt, opts => opts.MapFrom(src => src.RequireSignOutPrompt))
				.ForMember(desc => desc.AllowAccessToAllScopes, opts => opts.MapFrom(src => src.AllowAccessToAllScopes))
				.ForMember(desc => desc.IdentityTokenLifetime, opts => opts.MapFrom(src => src.IdentityTokenLifetime))
				.ForMember(desc => desc.AccessTokenLifetime, opts => opts.MapFrom(src => src.AccessTokenLifetime))
				.ForMember(desc => desc.AuthorizationCodeLifetime, opts => opts.MapFrom(src => src.AuthorizationCodeLifetime))
				.ForMember(desc => desc.AbsoluteRefreshTokenLifetime, opts => opts.MapFrom(src => src.AbsoluteRefreshTokenLifetime))
				.ForMember(desc => desc.SlidingRefreshTokenLifetime, opts => opts.MapFrom(src => src.SlidingRefreshTokenLifetime))
				.ForMember(desc => desc.RefreshTokenUsage, opts => opts.MapFrom(src => src.RefreshTokenUsage))
				.ForMember(desc => desc.UpdateAccessTokenOnRefresh, opts => opts.MapFrom(src => src.UpdateAccessTokenOnRefresh))
				.ForMember(desc => desc.RefreshTokenExpiration, opts => opts.MapFrom(src => src.RefreshTokenExpiration))
				.ForMember(desc => desc.AccessTokenType, opts => opts.MapFrom(src => src.AccessTokenType))
				.ForMember(desc => desc.EnableLocalLogin, opts => opts.MapFrom(src => src.EnableLocalLogin))
				.ForMember(desc => desc.IncludeJwtId, opts => opts.MapFrom(src => src.IncludeJwtId))
				.ForMember(desc => desc.AlwaysSendClientClaims, opts => opts.MapFrom(src => src.AlwaysSendClientClaims))
				.ForMember(desc => desc.PrefixClientClaims, opts => opts.MapFrom(src => src.PrefixClientClaims))
				.ForMember(desc => desc.AllowAccessToAllGrantTypes, opts => opts.MapFrom(src => src.AllowAccessToAllGrantTypes))
				.ForMember(desc => desc.ClientCorsOrigins, opts => opts.MapFrom(src => src.ClientCorsOrigins))
				.ForMember(desc => desc.ClientPostLogoutRedirectUris, opts => opts.MapFrom(src => src.ClientPostLogoutRedirectUris))
				.ForMember(desc => desc.ClientRedirectUris, opts => opts.MapFrom(src => src.ClientRedirectUris))
				.ForMember(desc => desc.ClientScopes, opts => opts.MapFrom(src => src.ClientScopes))
				.ForMember(desc => desc.ClientSecrets, opts => opts.MapFrom(src => src.ClientSecrets))
				.ForMember(desc => desc.Permissions, opts => opts.MapFrom(src => src.Permissions))
				.ForMember(desc => desc.UserPermissions, opts => opts.MapFrom(src => src.UserPermissions))
				.MaxDepth(5);
		}
	}
}