using System.Collections.Generic;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class RoleDTO
	{
		public RoleDTO()
		{
			Clients = new List<ClientDTO>();
			Permissions = new List<PermissionDTO>();
			Users = new List<UserDTO>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<ClientDTO> Clients { get; set; }
		public virtual ICollection<PermissionDTO> Permissions { get; set; }
		public virtual ICollection<UserDTO> Users { get; set; }

		////
		public int ClientId { get; set; }
	}
}