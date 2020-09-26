using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public partial class ClientClaim : IEntityObject
	{
		public int Id { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}