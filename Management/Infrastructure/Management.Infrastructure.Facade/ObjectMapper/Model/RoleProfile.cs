using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class RoleProfile : Profile
	{
		public RoleProfile()
		{
			CreateMap<RoleDTO, Role>()
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(desc => desc.Description, opts => opts.MapFrom(src => src.Description))
				.ForMember(desc => desc.Permissions, opts => opts.MapFrom(src => src.Permissions))
				.ForMember(desc => desc.Users, opts => opts.MapFrom(src => src.Users))
				.ForMember(desc => desc.Clients, opts => opts.MapFrom(src => src.Clients))
				.MaxDepth(5);
		}
	}
}