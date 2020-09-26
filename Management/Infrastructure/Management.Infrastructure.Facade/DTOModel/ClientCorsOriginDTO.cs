namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ClientCorsOriginDTO
	{
		public int Id { get; set; }
		public string Origin { get; set; }
		public virtual ClientDTO Client { get; set; }
	}
}