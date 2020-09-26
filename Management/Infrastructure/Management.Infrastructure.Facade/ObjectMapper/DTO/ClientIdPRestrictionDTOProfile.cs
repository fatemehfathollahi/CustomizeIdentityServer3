using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ClientIdPRestrictionDTOProfile : Profile
	{
		public ClientIdPRestrictionDTOProfile()
		{
			CreateMap<ClientIdPRestriction, ClientIdPRestrictionDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Provider, opts => opts.MapFrom(src => src.Provider))
				.ForMember(desc => desc.Client, opts => opts.MapFrom(src => src.Client))
				.MaxDepth(5);
		}
	}
}