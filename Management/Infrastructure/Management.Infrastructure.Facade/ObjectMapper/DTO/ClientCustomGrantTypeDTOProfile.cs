using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ClientCustomGrantTypeDTOProfile : Profile
	{
		public ClientCustomGrantTypeDTOProfile()
		{
			CreateMap<ClientCustomGrantType, ClientCustomGrantTypeDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.GrantType, opts => opts.MapFrom(src => src.GrantType))
				.ForMember(desc => desc.Client, opts => opts.MapFrom(src => src.Client))
				.MaxDepth(5);
		}
	}
}