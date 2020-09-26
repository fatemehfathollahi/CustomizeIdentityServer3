namespace Management.Infrastructure.Facade.DTOModel
{
	public class UserClientPermissionDTO
	{
		public UserClientPermissionDTO()
		{
			User = new UserDTO();
			Permission = new PermissionDTO();
			Client = new ClientDTO();
		}

		public int Id { get; set; }

		public int User_Id
		{
			get => (User != null ? User.Id : 0);
			set
			{
				if (User != null)
				{
					User.Id = value;
				}
			}
		}

		public string User_Name => (User != null ? User.FirstName + " " + User.LastName : "");

		public UserDTO User { get; set; }

		public int Client_Id
		{
			get => (Client != null ? Client.Id : 0);
			set
			{
				if (Client != null)
				{
					Client.Id = value;
				}
			}
		}

		public string Client_Name => (Client != null ? Client.ClientName : "");
		public ClientDTO Client { get; set; }

		public int Permission_Id
		{
			get => (Permission != null ? Permission.Id : 0);
			set
			{
				if (Permission != null)
				{
					Permission.Id = value;
				}
			}
		}

		public string Permission_Name => (Permission != null ? Permission.Name : "");
		public PermissionDTO Permission { get; set; }
	}
}