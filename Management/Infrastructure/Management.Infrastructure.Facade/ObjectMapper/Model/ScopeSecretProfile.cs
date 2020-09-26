using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ScopeSecretProfile : Profile
	{
		public ScopeSecretProfile()
		{
			CreateMap<ScopeSecretDTO, ScopeSecret>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(desc => desc.Expiration, opts => opts.MapFrom(src => src.Expiration))
				.ForMember(desc => desc.Scope, opts => opts.MapFrom(src => src.Scope))
				.ForMember(desc => desc.Type, opts => opts.MapFrom(src => src.Type))
				.ForMember(desc => desc.Value, opts => opts.MapFrom(src => src.Value))
				.MaxDepth(5);
		}
	}
}