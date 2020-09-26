using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ScopeClaimDTOProfile : Profile
	{
		public ScopeClaimDTOProfile()
		{
			CreateMap<ScopeClaim, ScopeClaimDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(desc => desc.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(desc => desc.AlwaysIncludeInIdToken, opts => opts.MapFrom(src => src.AlwaysIncludeInIdToken))
				.ForMember(desc => desc.Scope, opts => opts.MapFrom(src => src.Scope))
				.MaxDepth(5);
		}
	}
}