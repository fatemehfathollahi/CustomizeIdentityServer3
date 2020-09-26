namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientIdPRestrictionDTO
	{
		public int Id { get; set; }
		public string Provider { get; set; }
		public virtual ClientDTO Client { get; set; }
	}
}