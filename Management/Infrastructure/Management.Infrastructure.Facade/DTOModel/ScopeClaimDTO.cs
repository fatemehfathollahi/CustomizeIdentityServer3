namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ScopeClaimDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool AlwaysIncludeInIdToken { get; set; }
		public virtual ScopeDTO Scope { get; set; }
	}
}