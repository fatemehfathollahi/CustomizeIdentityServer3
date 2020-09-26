using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public partial class UserLogin : IEntityObject
	{
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}