using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class PermissionDTOProfile : Profile
	{
		public PermissionDTOProfile()
		{
			CreateMap<Permission, PermissionDTO>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(desc => desc.Roles, opts => opts.MapFrom(src => src.Roles))
				.ForMember(desc => desc.Users, opts => opts.MapFrom(src => src.Users))
				.ForMember(desc => desc.Clients, opts => opts.MapFrom(src => src.Clients))
				.ForMember(desc => desc.UserPermissions, opts => opts.MapFrom(src => src.UserPermissions))
				.MaxDepth(5);
		}
	}
}