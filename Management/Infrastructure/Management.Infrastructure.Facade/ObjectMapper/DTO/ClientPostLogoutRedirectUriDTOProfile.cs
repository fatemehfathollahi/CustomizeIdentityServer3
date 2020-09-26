using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ClientPostLogoutRedirectUriDTOProfile : Profile
	{
		public ClientPostLogoutRedirectUriDTOProfile()
		{
			CreateMap<ClientPostLogoutRedirectUri, ClientPostLogoutRedirectUriDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Uri, opts => opts.MapFrom(src => src.Uri))
				.ForMember(desc => desc.Client, opts => opts.MapFrom(src => src.Client))
				.MaxDepth(5);
		}
	}
}