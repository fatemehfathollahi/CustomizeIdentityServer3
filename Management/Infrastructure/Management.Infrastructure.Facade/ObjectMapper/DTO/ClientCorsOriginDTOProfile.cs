using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class ClientCorsOriginDTOProfile : Profile
	{
		public ClientCorsOriginDTOProfile()
		{
			CreateMap<ClientCorsOrigin, ClientCorsOriginDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Origin, opts => opts.MapFrom(src => src.Origin))
				.ForMember(desc => desc.Client, opts => opts.MapFrom(src => src.Client))
				.MaxDepth(5);
		}
	}
}