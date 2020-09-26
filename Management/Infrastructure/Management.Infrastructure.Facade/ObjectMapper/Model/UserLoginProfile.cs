using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class UserLoginProfile : Profile
	{
		public UserLoginProfile()
		{
			CreateMap<UserLoginDTO, UserLogin>()
				.ForMember(desc => desc.LoginProvider, opts => opts.MapFrom(src => src.LoginProvider))
				.ForMember(desc => desc.ProviderKey, opts => opts.MapFrom(src => src.ProviderKey))
				.ForMember(desc => desc.UserId, opts => opts.MapFrom(src => src.User.Id))
				.MaxDepth(5);
		}
	}
}