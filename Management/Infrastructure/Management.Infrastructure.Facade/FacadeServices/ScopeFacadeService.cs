using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using Management.Infrastructure.Models;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Management.Infrastructure.Facade.FacadeServices
{
	public class ScopeFacadeService : IScopeFacadeService
	{
		#region Internal Fields

		internal IMapper Mapper;
		internal IScopeService ScopeService;

		#endregion Internal Fields

		#region Ctor

		public ScopeFacadeService(IMapper mapper, IScopeService scopeService)
		{
			Mapper = mapper;
			ScopeService = scopeService;
		}

		#endregion Ctor

		#region ScopeFacadeService Members

		#region Scopes

		public IEnumerable<ScopeDTO> GetAllScopes()
		{
			IEnumerable<Scope> scopes = ScopeService.GetAllScopes();

			return Mapper.Map<List<ScopeDTO>>(scopes);
		}

		public ScopeDTO GetScopeByID(int id)
		{
			Scope scopes = ScopeService.GetScopeByID(id);

			return Mapper.Map<ScopeDTO>(scopes);
		}

		public void AddScope(ScopeDTO scope)
		{
			var model = Mapper.Map<Scope>(scope);

			ScopeService.AddScope(model);
		}

		public void UpdateScope(ScopeDTO scope)
		{
			var model = Mapper.Map<Scope>(scope);

			ScopeService.UpdateScope(model);
		}

		public void DeleteScope(ScopeDTO scope)
		{
			var model = Mapper.Map<Scope>(scope);

			ScopeService.DeleteScope(model);
		}

		public bool ExistsName(int id, string name)
		{
			return ScopeService.ExistsName(id, name);
		}

		#endregion Scopes

		#region ScopeClaims

		public IEnumerable<ScopeClaimDTO> GetAllClaimScope(int scopeId)
		{
			IEnumerable<ScopeClaim> claimScopes = ScopeService.GetAllClaimScope(scopeId);

			return Mapper.Map<List<ScopeClaimDTO>>(claimScopes);
		}

		public void AddScopeClaim(int scopeId, params ScopeClaimDTO[] scopeClaims)
		{
			List<ScopeClaim> ScopeClaims = new List<ScopeClaim>();

			foreach (ScopeClaimDTO item in scopeClaims)
			{
				ScopeClaims.Add(Mapper.Map<ScopeClaim>(item));
			}

			if (ScopeClaims.Count > 0)
			{
				ScopeService.AddScopeClaim(scopeId, ScopeClaims.ToArray());
			}
		}

		public void UpdateScopeClaim(int scopeId, ScopeClaimDTO scopeClaims)
		{
			ScopeService.UpdateScopeClaim(scopeId, Mapper.Map<ScopeClaim>(scopeClaims));
		}

		public void DeleteScopeClaim(int scopeId, params ScopeClaimDTO[] scopeClaims)
		{
			List<ScopeClaim> ScopeClaims = new List<ScopeClaim>();

			foreach (ScopeClaimDTO item in scopeClaims)
			{
				ScopeClaims.Add(Mapper.Map<ScopeClaim>(item));
			}

			if (ScopeClaims.Count > 0)
			{
				ScopeService.DeleteScopeClaim(scopeId, ScopeClaims.ToArray());
			}
		}

		#endregion ScopeClaims

		#region ScopeSecrets

		public IEnumerable<ScopeSecretDTO> GetAllScopeSecrets(int scopeId)
		{
			IEnumerable<ScopeSecret> claimScopes = ScopeService.GetAllScopeSecrets(scopeId);

			return Mapper.Map<List<ScopeSecretDTO>>(claimScopes);
		}

		public void AddScopeSecret(int scopeId, params ScopeSecretDTO[] scopeSecrets)
		{
			List<ScopeSecret> ScopeSecrets = new List<ScopeSecret>();

			foreach (ScopeSecretDTO item in scopeSecrets)
			{
				ScopeSecrets.Add(Mapper.Map<ScopeSecret>(item));
			}

			if (ScopeSecrets.Count > 0)
			{
				ScopeService.AddScopeSecret(scopeId, ScopeSecrets.ToArray());
			}
		}

		public void DeleteScopeSecret(int scopeId, params ScopeSecretDTO[] scopeSecrets)
		{
			List<ScopeSecret> ScopeSecrets = new List<ScopeSecret>();

			foreach (ScopeSecretDTO item in scopeSecrets)
			{
				ScopeSecrets.Add(Mapper.Map<ScopeSecret>(item));
			}

			if (ScopeSecrets.Count > 0)
			{
				ScopeService.DeleteScopeSecret(scopeId, ScopeSecrets.ToArray());
			}
		}

		#endregion ScopeSecrets

		#endregion ScopeFacadeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (Mapper != null)
			{
				Mapper = null;
			}

			if (ScopeService != null)
			{
				ScopeService.Dispose();
				ScopeService = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}