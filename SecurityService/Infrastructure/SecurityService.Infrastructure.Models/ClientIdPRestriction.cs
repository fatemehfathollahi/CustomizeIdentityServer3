namespace SecurityService.Infrastructure.Models
{
	public partial class ClientIdPRestriction
	{
		public int Id { get; set; }
		public string Provider { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}