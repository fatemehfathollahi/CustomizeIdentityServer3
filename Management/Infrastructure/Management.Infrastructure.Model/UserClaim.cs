using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public partial class UserClaim : IEntityObject
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
		public virtual User User { get; set; }
	}
}