namespace SecurityService.Infrastructure.Models
{
	public partial class ClientPostLogoutRedirectUri
	{
		public int Id { get; set; }
		public string Uri { get; set; }
		public int Client_Id { get; set; }
		public virtual Client Client { get; set; }
	}
}