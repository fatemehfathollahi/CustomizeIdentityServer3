using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class ClientCorsOriginProfile : Profile
	{
		public ClientCorsOriginProfile()
		{
			CreateMap<ClientCorsOriginDTO, ClientCorsOrigin>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Origin, opts => opts.MapFrom(src => src.Origin))
				.ForMember(desc => desc.Client_Id, opts => opts.MapFrom(src => src.Client.Id))
				.MaxDepth(5);
		}
	}
}