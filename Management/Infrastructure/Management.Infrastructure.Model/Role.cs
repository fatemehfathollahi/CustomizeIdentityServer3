using Framework.Core.Contracts.Model;
using System.Collections.Generic;

namespace Management.Infrastructure.Models
{
	public partial class Role : IEntityObject
	{
		public Role()
		{
			Clients = new List<Client>();
			Permissions = new List<Permission>();
			Users = new List<User>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<Client> Clients { get; set; }
		public virtual ICollection<Permission> Permissions { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}