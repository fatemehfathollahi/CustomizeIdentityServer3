using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ClientPostLogoutRedirectUriProfile : Profile
	{
		public ClientPostLogoutRedirectUriProfile()
		{
			CreateMap<ClientPostLogoutRedirectUriDTO, ClientPostLogoutRedirectUri>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Uri, opts => opts.MapFrom(src => src.Uri))
				.ForMember(desc => desc.Client_Id, opts => opts.MapFrom(src => src.Client.Id))
				.MaxDepth(5);
		}
	}
}