using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Models;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class UserClaimDTOProfile : Profile
	{
		public UserClaimDTOProfile()
		{
			CreateMap<UserClaim, UserClaimDTO>()
				.ForMember(desc => desc.ClaimType, opts => opts.MapFrom(src => src.ClaimType))
				.ForMember(desc => desc.ClaimValue, opts => opts.MapFrom(src => src.ClaimValue))
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(desc => desc.User, opts => opts.MapFrom(src => src.User))
				.MaxDepth(5);
		}
	}
}