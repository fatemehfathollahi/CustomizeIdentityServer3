using System.Collections.Generic;

namespace Management.Infrastructure.Facade.DTOModel
{
	public partial class ScopeDTO
	{
		public ScopeDTO()
		{
			ScopeClaims = new List<ScopeClaimDTO>();
			ScopeSecrets = new List<ScopeSecretDTO>();
		}

		public int Id { get; set; }
		public bool Enabled { get; set; }  //
		public string Name { get; set; }  //
		public string DisplayName { get; set; }   //
		public string Description { get; set; }
		public bool Required { get; set; } //
		public bool Emphasize { get; set; }  //
		public int Type { get; set; }      //
		public bool IncludeAllClaimsForUser { get; set; } //
		public string ClaimsRule { get; set; }   //
		public bool ShowInDiscoveryDocument { get; set; }  //
		public bool AllowUnrestrictedIntrospection { get; set; }  //
		public virtual ICollection<ScopeClaimDTO> ScopeClaims { get; set; }
		public virtual ICollection<ScopeSecretDTO> ScopeSecrets { get; set; }
	}
}