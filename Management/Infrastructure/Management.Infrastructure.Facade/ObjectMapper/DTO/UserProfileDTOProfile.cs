using AutoMapper;
using Management.Infrastructure.Facade.DTOModel;

namespace Management.Infrastructure.Facade.ObjectMapper.DTO
{
	internal class UserProfileDTOProfile : Profile
	{
		public UserProfileDTOProfile()
		{
			CreateMap<Management.Infrastructure.Models.UserProfile, UserProfileDTO>()
				.ForMember(desc => desc.Birthdate, opts => opts.MapFrom(src => src.Birthdate))
				.ForMember(desc => desc.Email, opts => opts.MapFrom(src => src.Email))
				.ForMember(desc => desc.EmployeeNumber, opts => opts.MapFrom(src => src.EmployeeNumber))
				.ForMember(desc => desc.FirstName, opts => opts.MapFrom(src => src.FirstName))
				.ForMember(desc => desc.InternalPhone, opts => opts.MapFrom(src => src.InternalPhone))
				.ForMember(desc => desc.InternalPhoneCode, opts => opts.MapFrom(src => src.InternalPhoneCode))
				.ForMember(desc => desc.LastName, opts => opts.MapFrom(src => src.LastName))
				.ForMember(desc => desc.Mobile, opts => opts.MapFrom(src => src.Mobile))
				.ForMember(desc => desc.NationalCode, opts => opts.MapFrom(src => src.NationalCode))
				.ForMember(desc => desc.PersonCode, opts => opts.MapFrom(src => src.PersonCode))
				.ForMember(desc => desc.ProfileImageUrl, opts => opts.MapFrom(src => src.ProfileImageUrl))
				//.ForMember(desc => desc.User, opts => opts.MapFrom(src => src.User))
				.ForMember(desc => desc.Id, opts => opts.MapFrom(src => src.User.Id))
				.MaxDepth(5);
		}
	}
}