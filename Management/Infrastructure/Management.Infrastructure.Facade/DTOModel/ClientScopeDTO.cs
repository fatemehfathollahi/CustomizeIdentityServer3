namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientScopeDTO
	{
		public int Id { get; set; }
		public string Scope { get; set; }
		public virtual ClientDTO Client { get; set; }
	}
}