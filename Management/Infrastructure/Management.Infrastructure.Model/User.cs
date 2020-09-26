using Framework.Core.Contracts.Model;
using System;
using System.Collections.Generic;

namespace Management.Infrastructure.Models
{
	public partial class User : IEntityObject
	{
		public User()
		{
			UserClaims = new List<UserClaim>();
			UserLogins = new List<UserLogin>();
			Clients = new List<Client>();
			Permissions = new List<Permission>();
			Roles = new List<Role>();
			UserPermissions = new List<UserClientPermission>();

			//this.UserProfile = new UserProfile();
			//UserProfile.User = this;
			//UserProfile.UserID = this.Id;
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
		public virtual ICollection<UserClaim> UserClaims { get; set; }
		public virtual ICollection<UserLogin> UserLogins { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		public virtual ICollection<Client> Clients { get; set; }
		public virtual ICollection<Permission> Permissions { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		public virtual ICollection<UserClientPermission> UserPermissions { get; set; }
	}
}