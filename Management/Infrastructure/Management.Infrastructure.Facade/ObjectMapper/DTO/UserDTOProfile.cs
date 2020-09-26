using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class UserDTOProfile : Profile
	{
		public UserDTOProfile()
		{
			CreateMap<User, UserDTO>()
				.ForMember(desc => desc.AccessFailedCount, opts => opts.MapFrom(src => src.AccessFailedCount))
				.ForMember(desc => desc.Clients, opts => opts.MapFrom(src => src.Clients))
				.ForMember(desc => desc.Email, opts => opts.MapFrom(src => src.Email))
				.ForMember(desc => desc.EmailConfirmed, opts => opts.MapFrom(src => src.EmailConfirmed))
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.LockoutEnabled, opts => opts.MapFrom(src => src.LockoutEnabled))
				.ForMember(desc => desc.LockoutEndDateUtc, opts => opts.MapFrom(src => src.LockoutEndDateUtc))
				.ForMember(desc => desc.PasswordHash, opts => opts.MapFrom(src => src.PasswordHash))
				.ForMember(desc => desc.Permissions, opts => opts.MapFrom(src => src.Permissions))
				.ForMember(desc => desc.PhoneNumber, opts => opts.MapFrom(src => src.PhoneNumber))
				.ForMember(desc => desc.PhoneNumberConfirmed, opts => opts.MapFrom(src => src.PhoneNumberConfirmed))
				.ForMember(desc => desc.Roles, opts => opts.MapFrom(src => src.Roles))
				.ForMember(desc => desc.SecurityStamp, opts => opts.MapFrom(src => src.SecurityStamp))
				.ForMember(desc => desc.TwoFactorEnabled, opts => opts.MapFrom(src => src.TwoFactorEnabled))
				.ForMember(desc => desc.UserClaims, opts => opts.MapFrom(src => src.UserClaims))
				.ForMember(desc => desc.UserLogins, opts => opts.MapFrom(src => src.UserLogins))
				.ForMember(desc => desc.UserName, opts => opts.MapFrom(src => src.UserName))
				.ForMember(desc => desc.UserProfile, opts => opts.MapFrom(src => src.UserProfile))
				.ForMember(desc => desc.UserPermissions, opts => opts.MapFrom(src => src.UserPermissions))
				.MaxDepth(5);
		}
	}
}