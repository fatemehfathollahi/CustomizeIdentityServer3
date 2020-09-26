namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class UserLoginDTO
	{
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
		public virtual UserDTO User { get; set; }
	}
}