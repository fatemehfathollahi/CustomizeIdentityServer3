using Framework.Core.Contracts.Model;
using System.Collections.Generic;

namespace Management.Infrastructure.Models
{
	public partial class Permission : IEntityObject
	{
		public Permission()
		{
			Roles = new List<Role>();
			Users = new List<User>();
			Clients = new List<Client>();
			UserPermissions = new List<UserClientPermission>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<Client> Clients { get; set; }
		public virtual ICollection<UserClientPermission> UserPermissions { get; set; }
	}
}