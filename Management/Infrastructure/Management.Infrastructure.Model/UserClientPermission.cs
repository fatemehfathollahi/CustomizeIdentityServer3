using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public class UserClientPermission : IEntityObject
	{
		public UserClientPermission()
		{
			////this.User = new User();
			////this.Permission = new Permission();
			////this.Client = new Models.Client();
		}

		public int Id { get; set; }

		//public virtual int User_Id { get; set; }
		public virtual User User { get; set; }

		//public virtual int Client_Id { get; set; }
		public virtual Client Client { get; set; }

		//public virtual int Permission_Id { get; set; }
		public virtual Permission Permission { get; set; }
	}
}