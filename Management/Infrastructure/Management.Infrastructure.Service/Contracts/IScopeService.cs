using Framework.Core.Contracts.Services;
using Management.Infrastructure.Models;
using System.Collections.Generic;

namespace Management.Infrastructure.Service.Contracts
{
	public interface IScopeService : IService
	{
		#region Scopes

		IEnumerable<Scope> GetAllScopes();

		Scope GetScopeByID(int id);

		bool ExistsName(int id, string name);

		void AddScope(Scope scope);

		void UpdateScope(Scope scope);

		void DeleteScope(Scope scope);

		#endregion Scopes

		#region ScopeClaims

		IEnumerable<ScopeClaim> GetAllClaimScope(int scopeId);

		void AddScopeClaim(int scopeId, params ScopeClaim[] scopeClaims);

		void UpdateScopeClaim(int scopeId, ScopeClaim scopeClaims);

		void DeleteScopeClaim(int scopeId, params ScopeClaim[] scopeClaims);

		#endregion ScopeClaims

		#region ScopeSecrets

		IEnumerable<ScopeSecret> GetAllScopeSecrets(int scopeId);

		void AddScopeSecret(int scopeId, params ScopeSecret[] scopeSecrets);

		void DeleteScopeSecret(int scopeId, params ScopeSecret[] scopeSecrets);

		#endregion ScopeSecrets
	}
}