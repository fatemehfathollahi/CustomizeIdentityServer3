namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientCustomGrantTypeDTO
	{
		public int Id { get; set; }
		public string GrantType { get; set; }
		public virtual ClientDTO Client { get; set; }
	}
}