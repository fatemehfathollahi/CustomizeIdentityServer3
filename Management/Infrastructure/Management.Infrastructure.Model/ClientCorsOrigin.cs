using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public partial class ClientCorsOrigin : IEntityObject
	{
		public int Id { get; set; }
		public string Origin { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}