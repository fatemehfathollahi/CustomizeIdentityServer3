using Framework.Core.Contracts.Services;
using Management.Infrastructure.Facade.DTOModel;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.FacadeService.Contracts
{
	public interface IScopeFacadeService : IFacadeService
	{
		#region Scopes

		IEnumerable<ScopeDTO> GetAllScopes();

		ScopeDTO GetScopeByID(int id);

		bool ExistsName(int id, string name);

		void AddScope(ScopeDTO scope);

		void UpdateScope(ScopeDTO scope);

		void DeleteScope(ScopeDTO scope);

		#endregion Scopes

		#region ScopeClaims

		IEnumerable<ScopeClaimDTO> GetAllClaimScope(int scopeId);

		void AddScopeClaim(int scopeId, params ScopeClaimDTO[] scopeClaims);

		void UpdateScopeClaim(int scopeId, ScopeClaimDTO scopeClaims);

		void DeleteScopeClaim(int scopeId, params ScopeClaimDTO[] scopeClaims);

		#endregion ScopeClaims

		#region ScopeSecrets

		IEnumerable<ScopeSecretDTO> GetAllScopeSecrets(int scopeId);

		void AddScopeSecret(int scopeId, params ScopeSecretDTO[] scopeSecrets);

		void DeleteScopeSecret(int scopeId, params ScopeSecretDTO[] scopeSecrets);

		#endregion ScopeSecrets
	}
}