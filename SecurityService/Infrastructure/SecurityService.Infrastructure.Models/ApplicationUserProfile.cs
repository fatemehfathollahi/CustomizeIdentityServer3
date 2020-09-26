using System;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationUserProfile
	{
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string InternalPhone { get; set; }
		public string InternalPhoneCode { get; set; }
		public string EmployeeNumber { get; set; }
		public Nullable<System.DateTime> Birthdate { get; set; }
		public string ProfileImageUrl { get; set; }
		public string NationalCode { get; set; }
		public string PersonCode { get; set; }
		public virtual ApplicationUser User { get; set; }
	}
}