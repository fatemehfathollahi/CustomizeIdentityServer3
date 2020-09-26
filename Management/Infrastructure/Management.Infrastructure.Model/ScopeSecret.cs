using Framework.Core.Contracts.Model;
using System;

namespace Management.Infrastructure.Models
{
	public partial class ScopeSecret : IEntityObject
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public Nullable<System.DateTimeOffset> Expiration { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
		public int Scope_Id { get; set; }
		public virtual Scope Scope { get; set; }
	}
}