namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientPostLogoutRedirectUriDTO
	{
		public int Id { get; set; }
		public string Uri { get; set; }
		public virtual ClientDTO Client { get; set; }
	}
}