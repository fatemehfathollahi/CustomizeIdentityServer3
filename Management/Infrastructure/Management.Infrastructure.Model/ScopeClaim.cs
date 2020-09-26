using Framework.Core.Contracts.Model;

namespace Management.Infrastructure.Models
{
	public partial class ScopeClaim : IEntityObject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool AlwaysIncludeInIdToken { get; set; }
		public int Scope_Id { get; set; }
		public virtual Scope Scope { get; set; }
	}
}