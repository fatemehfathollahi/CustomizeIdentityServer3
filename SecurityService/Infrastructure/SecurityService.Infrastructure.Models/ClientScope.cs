namespace SecurityService.Infrastructure.Models
{
	public partial class ClientScope
	{
		public int Id { get; set; }
		public string Scope { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}