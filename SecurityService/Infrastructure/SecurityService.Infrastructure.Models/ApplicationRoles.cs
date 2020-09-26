using SecurityService.Infrastructure.Core;
using System.Collections.Generic;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationRoles : IGroup<int>
	{
		public ApplicationRoles()
		{
			this.Permissions = new List<ApplicationPermission>();
			this.Users = new List<ApplicationUser>();
			this.Clients = new List<Client>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<Client> Clients { get; set; }
		public virtual ICollection<ApplicationPermission> Permissions { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
	}
}