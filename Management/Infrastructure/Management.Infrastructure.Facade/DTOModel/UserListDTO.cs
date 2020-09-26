using System;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class UserListDTO
	{
		public UserListDTO()
		{
		}

		public int Id { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string PasswordHash { get; set; }

		public string SecurityStamp { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public DateTime? LockoutEndDateUtc { get; set; }
		public bool LockoutEnabled { get; set; }
		public int AccessFailedCount { get; set; }
		public string UserName { get; set; }
		public virtual UserProfileDTO UserProfile { get; set; }

		//
		public int TotalCount { get; set; }

		public string FirstName { get => UserProfile.FirstName; set => UserProfile.FirstName = value; }
		public string LastName { get => UserProfile.LastName; set => UserProfile.LastName = value; }
		public string Mobile { get => UserProfile.Mobile; set => UserProfile.Mobile = value; }
		public string InternalPhone { get => UserProfile.InternalPhone; set => UserProfile.InternalPhone = value; }
		public string InternalPhoneCode { get => UserProfile.InternalPhoneCode; set => UserProfile.InternalPhoneCode = value; }
		public string EmployeeNumber { get => UserProfile.EmployeeNumber; set => UserProfile.EmployeeNumber = value; }
		public string NationalCode { get => UserProfile.NationalCode; set => UserProfile.NationalCode = value; }
		public string PersonCode { get => UserProfile.PersonCode; set => UserProfile.PersonCode = value; }
	}
}