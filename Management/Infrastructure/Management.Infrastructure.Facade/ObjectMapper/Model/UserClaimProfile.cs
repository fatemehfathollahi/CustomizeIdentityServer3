using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.Model
{
	internal class UserClaimProfile : Profile
	{
		public UserClaimProfile()
		{
			CreateMap<UserClaimDTO, UserClaim>()
				.ForMember(desc => desc.ClaimType, opts => opts.MapFrom(src => src.ClaimType))
				.ForMember(desc => desc.ClaimValue, opts => opts.MapFrom(src => src.ClaimValue))
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.UserId, opts => opts.MapFrom(src => src.User.Id))
				.MaxDepth(5);
		}
	}
}