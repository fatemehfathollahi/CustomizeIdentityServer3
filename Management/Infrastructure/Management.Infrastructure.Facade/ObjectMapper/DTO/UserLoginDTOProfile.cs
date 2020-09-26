using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class UserLoginDTOProfile : Profile
	{
		public UserLoginDTOProfile()
		{
			CreateMap<UserLogin, UserLoginDTO>()
				.ForMember(desc => desc.LoginProvider, opts => opts.MapFrom(src => src.LoginProvider))
				.ForMember(desc => desc.ProviderKey, opts => opts.MapFrom(src => src.ProviderKey))
				.ForMember(desc => desc.User, opts => opts.MapFrom(src => src.User))
				.MaxDepth(5);
		}
	}
}