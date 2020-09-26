using Framework.Core.Common.Attributes;
using Management.Infrastructure.Data;
using Management.Infrastructure.Models;
using Management.Infrastructure.Models.Repositories;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Infrastructure.Service
{
	public class ScopeService : IScopeService
	{
		#region Fields

		private IScopeRepository _scopeRepository;
		private IScopeClaimRepository _scopeClaimRepository;
		private IScopeSecretRepository _scopeSecretRepository;
		private QueryDbContext _queryDbContext;

		#endregion Fields

		#region Ctor

		public ScopeService(IScopeRepository scopeRepository, IScopeClaimRepository scopeClaimRepository,
										IScopeSecretRepository scopeSecretRepository, QueryDbContext queryDbContext)
		{
			_scopeRepository = scopeRepository;
			_scopeClaimRepository = scopeClaimRepository;
			_scopeSecretRepository = scopeSecretRepository;
			_queryDbContext = queryDbContext;
		}

		#endregion Ctor

		#region IScopeService Members

		#region Scopes

		[NonTransactional]
		public IEnumerable<Scope> GetAllScopes()
		{
			return _scopeRepository.GetAllAsQuery()
				.ToList();
		}

		[NonTransactional]
		public Scope GetScopeByID(int id)
		{
			//return _scopeRepository.Get(id);
			var scopTemp = (from sc in _queryDbContext.Scopes
							where sc.Id == id
							select new
							{
								Id = sc.Id,
								Enabled = sc.Enabled,
								Name = sc.Name,
								DisplayName = sc.DisplayName,
								Description = sc.Description,
								Required = sc.Required,
								Emphasize = sc.Emphasize,
								Type = sc.Type,
								IncludeAllClaimsForUser = sc.IncludeAllClaimsForUser,
								ClaimsRule = sc.ClaimsRule,
								ShowInDiscoveryDocument = sc.ShowInDiscoveryDocument,
								AllowUnrestrictedIntrospection = sc.AllowUnrestrictedIntrospection,
								ScopeClaims = sc.ScopeClaims.Select(p => new
								{
									Id = p.Id,
									Name = p.Name,
									Description = p.Description,
									AlwaysIncludeInIdToken = p.AlwaysIncludeInIdToken,
									Scope_Id = p.Scope_Id
								}).ToList(),
								ScopeSecrets = sc.ScopeSecrets.Select(p => new
								{
									Id = p.Id,
									Description = p.Description,
									Expiration = p.Expiration,
									Type = p.Type,
									Value = p.Value,
									Scope_Id = p.Scope_Id,
								}).ToList(),
							}).FirstOrDefault();

			Scope scope = new Scope
			{
				Id = scopTemp.Id,
				Enabled = scopTemp.Enabled,
				Name = scopTemp.Name,
				DisplayName = scopTemp.DisplayName,
				Description = scopTemp.Description,
				Required = scopTemp.Required,
				Emphasize = scopTemp.Emphasize,
				Type = scopTemp.Type,
				IncludeAllClaimsForUser = scopTemp.IncludeAllClaimsForUser,
				ClaimsRule = scopTemp.ClaimsRule,
				ShowInDiscoveryDocument = scopTemp.ShowInDiscoveryDocument,
				AllowUnrestrictedIntrospection = scopTemp.AllowUnrestrictedIntrospection,
			};
			foreach (var p in scopTemp.ScopeClaims)
			{
				scope.ScopeClaims.Add(new ScopeClaim
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description,
					AlwaysIncludeInIdToken = p.AlwaysIncludeInIdToken,
					Scope_Id = p.Scope_Id
				});
			}
			foreach (var p in scopTemp.ScopeSecrets)
			{
				scope.ScopeSecrets.Add(new ScopeSecret
				{
					Id = p.Id,
					Description = p.Description,
					Expiration = p.Expiration,
					Type = p.Type,
					Value = p.Value,
					Scope_Id = p.Scope_Id
				});
			}
			return scope;
		}

		public bool ExistsName(int id, string name)
		{
			return _scopeRepository.Get(o => o.Id != id && o.Name == name).Any();
		}

		public void AddScope(Scope scope)
		{
			_scopeRepository.Insert(scope);
		}

		public void UpdateScope(Scope scope)
		{
			Scope currentScope = _scopeRepository.Get(scope.Id);

			if (currentScope != null)
			{
				currentScope.AllowUnrestrictedIntrospection = scope.AllowUnrestrictedIntrospection;
				currentScope.ClaimsRule = scope.ClaimsRule;
				currentScope.Description = scope.Description;
				currentScope.DisplayName = scope.DisplayName;
				currentScope.Emphasize = scope.Emphasize;
				currentScope.Enabled = scope.Enabled;
				currentScope.IncludeAllClaimsForUser = scope.IncludeAllClaimsForUser;
				currentScope.Name = scope.Name;
				currentScope.Required = scope.Required;
				currentScope.ShowInDiscoveryDocument = scope.ShowInDiscoveryDocument;
				currentScope.Type = scope.Type;
				_scopeRepository.Update(currentScope);

				List<int> tempDeleteListId = new List<int>();
				foreach (ScopeClaim item in currentScope.ScopeClaims)
				{
					ScopeClaim tRow = scope.ScopeClaims.Where(o => o.Id == item.Id && item.Scope_Id == currentScope.Id).FirstOrDefault();
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ScopeClaim item in scope.ScopeClaims)
				{
					if (item.Id < 0)
					{
						_scopeClaimRepository.Insert(new ScopeClaim
						{
							AlwaysIncludeInIdToken = item.AlwaysIncludeInIdToken,
							Description = item.Description,
							Name = item.Name,
							Scope_Id = currentScope.Id
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ScopeClaim tRow = currentScope.ScopeClaims.Where(o => o.Scope_Id == currentScope.Id && o.Id == item).FirstOrDefault();
					if (tRow != null)
					{
						_scopeClaimRepository.Delete(tRow.Id);
					}
				}
				////////////////////////
				tempDeleteListId.Clear();
				foreach (ScopeSecret item in currentScope.ScopeSecrets)
				{
					ScopeSecret tRow = scope.ScopeSecrets.Where(o => o.Id == item.Id && item.Scope_Id == currentScope.Id).FirstOrDefault();
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ScopeSecret item in scope.ScopeSecrets)
				{
					if (item.Id < 0)
					{
						_scopeSecretRepository.Insert(new ScopeSecret
						{
							Description = item.Description,
							Expiration = item.Expiration,
							Scope_Id = currentScope.Id,
							Type = item.Type,
							Value = item.Value
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ScopeSecret tRow = currentScope.ScopeSecrets.Where(o => o.Scope_Id == currentScope.Id && o.Id == item).FirstOrDefault();
					if (tRow != null)
					{
						_scopeSecretRepository.Delete(tRow.Id);
					}
				}
			}
		}

		public void DeleteScope(Scope scope)
		{
			// _scopeRepository.Delete(scope);
			Scope currentClient = _scopeRepository.Get(scope.Id);
			if (currentClient != null)
			{
				foreach (ScopeClaim item in scope.ScopeClaims)
				{
					_scopeClaimRepository.Delete(item.Id);
				}

				foreach (ScopeSecret item in scope.ScopeSecrets)
				{
					_scopeSecretRepository.Delete(item.Id);
				}

				_scopeRepository.Delete(scope.Id);
			}
		}

		#endregion Scopes

		#region ScopeClaims

		[NonTransactional]
		public IEnumerable<ScopeClaim> GetAllClaimScope(int scopeId)
		{
			return _scopeClaimRepository.GetAllAsQuery()
				.Where(o => o.Scope_Id == scopeId).ToList();
		}

		public void AddScopeClaim(int scopeId, params ScopeClaim[] scopeClaims)
		{
			Scope scope = _scopeRepository.Get(scopeId);

			if (scope != null)
			{
				foreach (ScopeClaim item in scopeClaims)
				{
					item.Scope = scope;
					item.Scope_Id = scopeId;

					_scopeClaimRepository.Insert(item);
				}
			}
		}

		public void UpdateScopeClaim(int scopeId, ScopeClaim scopeClaims)
		{
			Scope scope = _scopeRepository.Get(scopeId);
			ScopeClaim scopeClaim = _scopeClaimRepository.Get(scopeClaims.Id);

			if (scopeClaim != null && scope != null)
			{
				scopeClaim.AlwaysIncludeInIdToken = scopeClaims.AlwaysIncludeInIdToken;
				scopeClaim.Description = scopeClaims.Description;
				scopeClaim.Name = scopeClaims.Name;
				scopeClaim.Scope_Id = scopeId;
				scopeClaim.Scope = scope;
			}

			_scopeClaimRepository.Update(scopeClaim);
		}

		public void DeleteScopeClaim(int scopeId, params ScopeClaim[] scopeClaims)
		{
			Scope scope = _scopeRepository.Get(scopeId);

			if (scope != null)
			{
				foreach (ScopeClaim item in scopeClaims)
				{
					item.Scope = scope;
					item.Scope_Id = scopeId;

					_scopeClaimRepository.Delete(item);
				}
			}
		}

		#endregion ScopeClaims

		#region ScopeSecrets

		[NonTransactional]
		public IEnumerable<ScopeSecret> GetAllScopeSecrets(int scopeId)
		{
			return _scopeSecretRepository.GetAllAsQuery()
				.Where(o => o.Scope_Id == scopeId).ToList();
		}

		public void AddScopeSecret(int scopeId, params ScopeSecret[] scopeSecrets)
		{
			Scope scope = _scopeRepository.Get(scopeId);

			if (scope != null)
			{
				foreach (ScopeSecret item in scopeSecrets)
				{
					item.Scope_Id = scopeId;
					item.Scope = scope;

					_scopeSecretRepository.Insert(item);
				}
			}
		}

		public void DeleteScopeSecret(int scopeId, params ScopeSecret[] scopeSecrets)
		{
			Scope scope = _scopeRepository.Get(scopeId);

			if (scope != null)
			{
				foreach (ScopeSecret item in scopeSecrets)
				{
					item.Scope_Id = scopeId;
					item.Scope = scope;

					_scopeSecretRepository.Delete(item);
				}
			}
		}

		#endregion ScopeSecrets

		#endregion IScopeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_scopeRepository != null)
			{
				_scopeRepository.Dispose();
				_scopeRepository = null;
			}

			if (_scopeClaimRepository != null)
			{
				_scopeClaimRepository.Dispose();
				_scopeClaimRepository = null;
			}

			if (_scopeSecretRepository != null)
			{
				_scopeSecretRepository.Dispose();
				_scopeSecretRepository = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}