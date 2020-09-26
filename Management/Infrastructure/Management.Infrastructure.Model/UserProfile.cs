using Framework.Core.Contracts.Model;
using System;

namespace Management.Infrastructure.Models
{
	public partial class UserProfile : IEntityObject
	{
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
		public virtual int UserID { get; set; }
		public virtual User User { get; set; }
	}
}