using System;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ScopeSecretDTO
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public Nullable<System.DateTimeOffset> Expiration { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
		public virtual ScopeDTO Scope { get; set; }
	}
}