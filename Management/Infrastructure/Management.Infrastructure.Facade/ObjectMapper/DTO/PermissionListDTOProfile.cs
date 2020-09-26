using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class PermissionListDTOProfile : Profile
	{
		public PermissionListDTOProfile()
		{
			CreateMap<Permission, PermissionListDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Name, opts => opts.MapFrom(src => src.Name))
				.MaxDepth(5);
		}
	}
}