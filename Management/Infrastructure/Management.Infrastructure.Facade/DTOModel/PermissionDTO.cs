using System.Collections.Generic;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class PermissionDTO
	{
		public PermissionDTO()
		{
			Clients = new List<ClientDTO>();
			Roles = new List<RoleDTO>();
			Users = new List<UserDTO>();
			UserPermissions = new List<UserClientPermissionDTO>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<ClientDTO> Clients { get; set; }
		public virtual ICollection<RoleDTO> Roles { get; set; }
		public virtual ICollection<UserDTO> Users { get; set; }
		public virtual ICollection<UserClientPermissionDTO> UserPermissions { get; set; }
	}
}