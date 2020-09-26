using Framework.Core.Common.Attributes;
using Management.Infrastructure.Models;
using Management.Infrastructure.Models.Repositories;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Infrastructure.Service
{
	public class UserClientPermissionService : IUserClientPermissionService
	{
		#region Fields

		private IClientRepository _clientRepository;
		private IUserRepository _userRepository;
		private IPermissionRepository _permissionRepository;
		private IUserClientPermissionRepository _userClientPermissionRepository;

		#endregion Fields

		#region Ctors

		public UserClientPermissionService(IClientRepository clientRepository
			, IUserRepository clientUserRepository
			, IPermissionRepository permissionRepository
			, IUserClientPermissionRepository userClientPermissionRepository)
		{
			_clientRepository = clientRepository;
			_userRepository = clientUserRepository;
			_permissionRepository = permissionRepository;
			_userClientPermissionRepository = userClientPermissionRepository;
		}

		#endregion Ctors

		#region Implemented Method

		public void Add(UserClientPermission client)
		{
			throw new NotImplementedException();
		}

		public void Delete(UserClientPermission client)
		{
			throw new NotImplementedException();
		}

		[NonTransactional]
		public IEnumerable<UserClientPermission> Get()
		{
			List<UserClientPermission> result = new List<UserClientPermission>();
			List<Client> client = _clientRepository.GetAllAsQuery()
				.ToList();
			int id = 0;
			foreach (Client o in client)
			{
				foreach (Permission it in o.Permissions)
				{
					id++;
					result.Add(new UserClientPermission
					{
						Id = id,
						Client = new Client
						{
							Id = o.Id,
							Enabled = o.Enabled,
							ClientId = o.ClientId,
							ClientUri = o.ClientUri,
							LogoUri = o.LogoUri,
							RequireConsent = o.RequireConsent,
							AllowAccessToAllGrantTypes = o.AllowAccessToAllGrantTypes,
							AllowAccessToAllScopes = o.AllowAccessToAllScopes,
							AllowAccessTokensViaBrowser = o.AllowAccessTokensViaBrowser,
							AllowClientCredentialsOnly = o.AllowClientCredentialsOnly,
							AllowRememberConsent = o.AllowRememberConsent,
							Flow = o.Flow,
							LogoutSessionRequired = o.LogoutSessionRequired,
							RequireSignOutPrompt = o.RequireSignOutPrompt,
							AlwaysSendClientClaims = o.AlwaysSendClientClaims,
							AbsoluteRefreshTokenLifetime = o.AbsoluteRefreshTokenLifetime,
							AccessTokenLifetime = o.AccessTokenLifetime,
							AccessTokenType = o.AccessTokenType,
							AuthorizationCodeLifetime = o.AuthorizationCodeLifetime,
							ClientName = o.ClientName,
							EnableLocalLogin = o.EnableLocalLogin,
							IdentityTokenLifetime = o.IdentityTokenLifetime,
							IncludeJwtId = o.IncludeJwtId,
							LogoutUri = o.LogoutUri,
							PrefixClientClaims = o.PrefixClientClaims,
							RefreshTokenExpiration = o.RefreshTokenExpiration,
							RefreshTokenUsage = o.RefreshTokenUsage,
							SlidingRefreshTokenLifetime = o.SlidingRefreshTokenLifetime,
							UpdateAccessTokenOnRefresh = o.UpdateAccessTokenOnRefresh
						},
						Permission = new Permission
						{
							Id = it.Id,
							Name = it.Name
						},
						User = new User { UserProfile = new UserProfile() }
					});
				}
			}

			return result;
		}

		[NonTransactional]
		public UserClientPermission Get(int id)
		{
			return _userClientPermissionRepository.Get(id);
		}

		[NonTransactional]
		public IEnumerable<UserClientPermission> Get(UserClientPermission model)
		{
			throw new NotImplementedException();
		}

		public void Update(UserClientPermission client)
		{
			throw new NotImplementedException();
		}

		#endregion Implemented Method

		#region IDisposable Members

		public void Dispose()
		{
			if (_clientRepository != null)
			{
				_clientRepository.Dispose();
				_clientRepository = null;
			}

			if (_userRepository != null)
			{
				_userRepository.Dispose();
				_userRepository = null;
			}
			if (_permissionRepository != null)
			{
				_permissionRepository.Dispose();
				_permissionRepository = null;
			}
			if (_userClientPermissionRepository != null)
			{
				_userClientPermissionRepository.Dispose();
				_userClientPermissionRepository = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}