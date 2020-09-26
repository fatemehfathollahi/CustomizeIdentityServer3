using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public partial class ClientRedirectUri : IEntityObject
	{
		public int Id { get; set; }
		public string Uri { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}