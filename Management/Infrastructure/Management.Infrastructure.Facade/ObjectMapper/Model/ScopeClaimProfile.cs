using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ScopeClaimProfile : Profile
	{
		public ScopeClaimProfile()
		{
			CreateMap<ScopeClaimDTO, ScopeClaim>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(desc => desc.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(desc => desc.AlwaysIncludeInIdToken, opts => opts.MapFrom(src => src.AlwaysIncludeInIdToken))
				.ForMember(desc => desc.Scope_Id, opts => opts.MapFrom(src => src.Scope.Id))
				.MaxDepth(5);
		}
	}
}