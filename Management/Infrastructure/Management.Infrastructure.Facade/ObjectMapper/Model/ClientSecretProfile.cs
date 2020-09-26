using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ClientSecretProfile : Profile
	{
		public ClientSecretProfile()
		{
			CreateMap<ClientSecretDTO, ClientSecret>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Expiration, opts => opts.MapFrom(src => src.Expiration))
				.ForMember(desc => desc.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(desc => desc.Type, opts => opts.MapFrom(src => src.Type))
				.ForMember(desc => desc.Value, opts => opts.MapFrom(src => src.Value))
				.ForMember(desc => desc.Client_Id, opts => opts.MapFrom(src => src.Client.Id))
				.MaxDepth(5);
		}
	}
}