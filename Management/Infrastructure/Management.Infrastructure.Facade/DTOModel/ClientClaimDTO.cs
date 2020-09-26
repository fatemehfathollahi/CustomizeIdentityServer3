namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientClaimDTO
	{
		public int Id { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
		public virtual ClientDTO Client { get; set; }
	}
}