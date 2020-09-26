namespace SecurityService.Infrastructure.Models
{
	public partial class ClientCorsOrigin
	{
		public int Id { get; set; }
		public string Origin { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}