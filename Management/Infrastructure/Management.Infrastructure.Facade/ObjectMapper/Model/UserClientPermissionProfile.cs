using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class UserClientPermissionProfile : Profile
	{
		public UserClientPermissionProfile()
		{
			CreateMap<UserClientPermissionDTO, UserClientPermission>()
			.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.User, opts => opts.MapFrom(src => src.User))
				.ForMember(desc => desc.Permission, opts => opts.MapFrom(src => src.Permission))
				.ForMember(desc => desc.Client, opts => opts.MapFrom(src => src.Client))
				.MaxDepth(5);
		}
	}
}