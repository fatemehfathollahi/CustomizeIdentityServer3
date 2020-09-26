using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationPermission : IdentityRole<int, ApplicationUserPermission>
	{
		public ApplicationPermission()
		{
			Groups = new List<ApplicationRoles>();
			Clients = new List<Client>();
			UserPermissions = new List<UserClientPermissions>();
		}

		public virtual ICollection<ApplicationRoles> Groups { get; set; }
		public virtual ICollection<Client> Clients { get; set; }
		public virtual ICollection<UserClientPermissions> UserPermissions { get; set; }
	}
}