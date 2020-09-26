using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ClientCustomGrantTypeProfile : Profile
	{
		public ClientCustomGrantTypeProfile()
		{
			CreateMap<ClientCustomGrantTypeDTO, ClientCustomGrantType>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.GrantType, opts => opts.MapFrom(src => src.GrantType))
				.ForMember(desc => desc.Client_Id, opts => opts.MapFrom(src => src.Client.Id))
				.MaxDepth(5);
		}
	}
}