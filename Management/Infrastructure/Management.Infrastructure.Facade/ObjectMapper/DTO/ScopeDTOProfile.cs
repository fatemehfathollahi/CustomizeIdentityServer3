using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ScopeDTOProfile : Profile
	{
		public ScopeDTOProfile()
		{
			CreateMap<Scope, ScopeDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(desc => desc.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(desc => desc.AllowUnrestrictedIntrospection, opts => opts.MapFrom(src => src.AllowUnrestrictedIntrospection))
				.ForMember(desc => desc.ClaimsRule, opts => opts.MapFrom(src => src.ClaimsRule))
				.ForMember(desc => desc.DisplayName, opts => opts.MapFrom(src => src.DisplayName))
				.ForMember(desc => desc.Emphasize, opts => opts.MapFrom(src => src.Emphasize))
				.ForMember(desc => desc.Enabled, opts => opts.MapFrom(src => src.Enabled))
				.ForMember(desc => desc.IncludeAllClaimsForUser, opts => opts.MapFrom(src => src.IncludeAllClaimsForUser))
				.ForMember(desc => desc.Required, opts => opts.MapFrom(src => src.Required))
				.ForMember(desc => desc.ScopeClaims, opts => opts.MapFrom(src => src.ScopeClaims))
				.ForMember(desc => desc.ScopeSecrets, opts => opts.MapFrom(src => src.ScopeSecrets))
				.ForMember(desc => desc.ShowInDiscoveryDocument, opts => opts.MapFrom(src => src.ShowInDiscoveryDocument))
				.ForMember(desc => desc.Type, opts => opts.MapFrom(src => src.Type))
				.MaxDepth(5);
		}
	}
}