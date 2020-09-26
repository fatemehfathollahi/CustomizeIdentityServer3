namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class UserClaimDTO
	{
		public int Id { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
		public virtual UserDTO User { get; set; }
	}
}