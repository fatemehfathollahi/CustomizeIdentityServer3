using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserPermission, ApplicationUserClaim>
	{
		public ApplicationUser()
		{
			Groups = new List<ApplicationRoles>();
			Clients = new List<Client>();
			UserPermissions = new List<UserClientPermissions>();

			//UserProfile = new ApplicationUserProfile();
			//UserProfile.User = this;
			//UserProfile.UserID = this.Id;
		}

		public ApplicationUserProfile UserProfile { get; set; }
		public virtual ICollection<ApplicationRoles> Groups { get; set; }
		public virtual ICollection<Client> Clients { get; set; }
		public virtual ICollection<UserClientPermissions> UserPermissions { get; set; }
	}
}