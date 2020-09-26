using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ClientClaimDTOProfile : Profile
	{
		public ClientClaimDTOProfile()
		{
			CreateMap<ClientClaim, ClientClaimDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Type, opts => opts.MapFrom(src => src.Type))
				.ForMember(desc => desc.Value, opts => opts.MapFrom(src => src.Value))
				.ForMember(desc => desc.Client, opts => opts.MapFrom(src => src.Client))
				.MaxDepth(5);
		}
	}
}