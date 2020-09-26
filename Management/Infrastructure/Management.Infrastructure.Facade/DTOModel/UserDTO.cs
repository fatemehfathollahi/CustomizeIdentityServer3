using System;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class UserDTO
	{
		public UserDTO()
		{
			UserClaims = new List<UserClaimDTO>();
			UserLogins = new List<UserLoginDTO>();
			Clients = new List<ClientDTO>();
			Permissions = new List<PermissionDTO>();
			Roles = new List<RoleDTO>();
			UserProfile = new UserProfileDTO();
			UserPermissions = new List<UserClientPermissionDTO>();
		}

		public int Id { get; set; }
		public string Email { get; set; }
		public bool EmailConfirmed { get; set; }
		public string PasswordHash { get; set; }

		public string SecurityStamp { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
		public bool LockoutEnabled { get; set; }
		public int AccessFailedCount { get; set; }
		public string UserName { get; set; }
		public virtual ICollection<UserClaimDTO> UserClaims { get; set; }
		public virtual ICollection<UserLoginDTO> UserLogins { get; set; }
		public virtual UserProfileDTO UserProfile { get; set; }
		public virtual ICollection<ClientDTO> Clients { get; set; }
		public virtual ICollection<PermissionDTO> Permissions { get; set; }
		public virtual ICollection<RoleDTO> Roles { get; set; }
		public virtual ICollection<UserClientPermissionDTO> UserPermissions { get; set; }

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