using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ClientIdPRestrictionProfile : Profile
	{
		public ClientIdPRestrictionProfile()
		{
			CreateMap<ClientIdPRestrictionDTO, ClientIdPRestriction>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Provider, opts => opts.MapFrom(src => src.Provider))
				.ForMember(desc => desc.Client_Id, opts => opts.MapFrom(src => src.Client.Id))
				.MaxDepth(5);
		}
	}
}