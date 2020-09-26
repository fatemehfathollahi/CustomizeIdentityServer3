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
	public class ClientService : IClientService
	{
		#region Fields

		private IClientRepository _clientRepository;
		private IClientClaimRepository _clientClaimRepository;
		private IClientCorsOriginRepository _clientCorsOriginRepository;
		private IClientIdPRestrictionRepository _clientIdPRestrictionRepository;
		private IClientCustomGrantTypeRepository _clientCustomGrantTypeRepository;
		private IClientPostLogoutRedirectUriRepository _clientPostLogoutRedirectUriRepository;
		private IClientRedirectUriRepository _clientRedirectUriRepository;
		private IClientScopeRepository _clientScopeRepository;
		private IClientSecretRepository _clientSecretRepository;
		private IRoleRepository _clientRoleRepository;
		private IUserRepository _clientUserRepository;
		private IRoleRepository _roleRepository;
		private IPermissionRepository _permissionRepository;
		private IUserClientPermissionRepository _userClientPermissionRepository;
		private QueryDbContext _queryDbContext;

		#endregion Fields

		#region Ctors

		public ClientService(IClientRepository clientRepository
			, IClientCustomGrantTypeRepository clientCustomGrantTypeRepository
			, IClientClaimRepository clientClaimRepository
			, IClientCorsOriginRepository clientCorsOriginRepository
			, IClientIdPRestrictionRepository clientIdPRestrictionRepository
			, IClientPostLogoutRedirectUriRepository clientPostLogoutRedirectUriRepository
			, IClientRedirectUriRepository clientRedirectUriRepository
			, IClientScopeRepository clientScopeRepository
			, IClientSecretRepository clientSecretRepository
			, IRoleRepository clientRoleRepository
			, IUserRepository clientUserRepository
			, IRoleRepository roleRepository
			, IPermissionRepository permissionRepository
			, IUserClientPermissionRepository userClientPermissionRepository
			, QueryDbContext queryDbContext
			)
		{
			_clientRepository = clientRepository;
			_clientClaimRepository = clientClaimRepository;
			_clientCustomGrantTypeRepository = clientCustomGrantTypeRepository;
			_clientCorsOriginRepository = clientCorsOriginRepository;
			_clientPostLogoutRedirectUriRepository = clientPostLogoutRedirectUriRepository;
			_clientRedirectUriRepository = clientRedirectUriRepository;
			_clientScopeRepository = clientScopeRepository;
			_clientSecretRepository = clientSecretRepository;
			_clientRoleRepository = clientRoleRepository;
			_clientUserRepository = clientUserRepository;
			_clientIdPRestrictionRepository = clientIdPRestrictionRepository;
			_roleRepository = roleRepository;
			_permissionRepository = permissionRepository;
			_userClientPermissionRepository = userClientPermissionRepository;
			_queryDbContext = queryDbContext;
		}

		#endregion Ctors

		#region IClientService Members

		#region Client

		[NonTransactional]
		public IEnumerable<Client> GetAllClients()
		{
			return _clientRepository.GetAllAsQuery()
				.Where(o => o.ClientId != "AppStore").ToList();
		}

		[NonTransactional]
		public IEnumerable<Client> GetAllClientsByUserName(string userName)
		{
			List<Client> result = new List<Client>();

			User currentUser = _clientUserRepository
				.GetAllAsQuery().FirstOrDefault(o => o.UserName == userName);

			if (currentUser != null)
			{
				result = new List<Client>();

				IList<Client> UserClients =
				currentUser
					.UserPermissions.Where(o => o.User.UserName == userName && o.Client.ClientId != "AppStore")
					.Select(p => p.Client).Distinct()
					.ToList();

				foreach (Client item in UserClients)
				{
					Client client = GetClientById(item.Id);
					result.Add(client);
				}
			}

			return result;
		}

		[NonTransactional]
		public Client GetClientById(int id)
		{
			Client temp = _clientRepository.Get(id);
			var clientName = (from o in _queryDbContext.Clients
							  where o.Id == id
							  select new
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
								  UpdateAccessTokenOnRefresh = o.UpdateAccessTokenOnRefresh,
								  Roles = o.Roles.Select(p => new
								  {
									  Id = p.Id,
									  Description = p.Description,
									  Name = p.Name
								  }).ToList(),
								  Users = o.Users.Select(p => new
								  {
									  Id = p.Id,
									  Email = p.Email,
									  AccessFailedCount = p.AccessFailedCount,
									  EmailConfirmed = p.EmailConfirmed,
									  LockoutEnabled = p.LockoutEnabled,
									  LockoutEndDateUtc = p.LockoutEndDateUtc,
									  PasswordHash = p.PasswordHash,
									  PhoneNumber = p.PhoneNumber,
									  PhoneNumberConfirmed = p.PhoneNumberConfirmed,
									  SecurityStamp = p.SecurityStamp,
									  TwoFactorEnabled = p.TwoFactorEnabled,
									  UserName = p.UserName,
									  UserProfile = p.UserProfile
								  }).ToList(),
								  UserPermissions = o.UserPermissions.Select(p => new
								  {
									  Id = p.Id
								  }).ToList(),
								  Permissions = o.Permissions.Select(p => new
								  {
									  Id = p.Id,
									  Name = p.Name
								  }).ToList(),
								  ClientSecrets = o.ClientSecrets.Select(p => new
								  {
									  Id = p.Id,
									  Value = p.Value,
									  Type = p.Type,
									  Description = p.Description,
									  Expiration = p.Expiration,
									  Client_Id = p.Client_Id
								  }),
								  ClientScopes = o.ClientScopes.Select(p => new
								  {
									  Id = p.Id,
									  Scope = p.Scope,
									  Client_Id = p.Client_Id
								  }).ToList(),
								  ClientRedirectUris = o.ClientRedirectUris.Select(p => new
								  {
									  Id = p.Id,
									  Uri = p.Uri,
									  Client_Id = p.Client_Id
								  }).ToList(),
								  ClientPostLogoutRedirectUris = o.ClientPostLogoutRedirectUris.Select(p => new
								  {
									  Id = p.Id,
									  Uri = p.Uri,
									  Client_Id = p.Client_Id
								  }).ToList(),
								  ClientIdPRestrictions = o.ClientIdPRestrictions.Select(p => new
								  {
									  Id = p.Id,
									  Provider = p.Provider,
									  Client_Id = p.Client_Id
								  }).ToList(),
								  ClientCustomGrantTypes = o.ClientCustomGrantTypes.Select(p => new
								  {
									  Id = p.Id,
									  GrantType = p.GrantType,
									  Client_Id = p.Client_Id
								  }).ToList(),
								  ClientCorsOrigins = o.ClientCorsOrigins.Select(p => new
								  {
									  Id = p.Id,
									  Origin = p.Origin,
									  Client_Id = p.Client_Id
								  }).ToList(),
								  ClientClaims = o.ClientClaims.Select(p => new
								  {
									  Id = p.Id,
									  Type = p.Type,
									  Value = p.Value,
									  Client_Id = p.Client_Id
								  }).ToList(),
							  }).FirstOrDefault();

			Client client = new Client
			{
				Id = clientName.Id,
				Enabled = clientName.Enabled,
				ClientId = clientName.ClientId,
				ClientUri = clientName.ClientUri,
				LogoUri = clientName.LogoUri,
				RequireConsent = clientName.RequireConsent,
				AllowAccessToAllGrantTypes = clientName.AllowAccessToAllGrantTypes,
				AllowAccessToAllScopes = clientName.AllowAccessToAllScopes,
				AllowAccessTokensViaBrowser = clientName.AllowAccessTokensViaBrowser,
				AllowClientCredentialsOnly = clientName.AllowClientCredentialsOnly,
				AllowRememberConsent = clientName.AllowRememberConsent,
				Flow = clientName.Flow,
				LogoutSessionRequired = clientName.LogoutSessionRequired,
				RequireSignOutPrompt = clientName.RequireSignOutPrompt,
				AlwaysSendClientClaims = clientName.AlwaysSendClientClaims,
				AbsoluteRefreshTokenLifetime = clientName.AbsoluteRefreshTokenLifetime,
				AccessTokenLifetime = clientName.AccessTokenLifetime,
				AccessTokenType = clientName.AccessTokenType,
				AuthorizationCodeLifetime = clientName.AuthorizationCodeLifetime,
				ClientName = clientName.ClientName,
				EnableLocalLogin = clientName.EnableLocalLogin,
				IdentityTokenLifetime = clientName.IdentityTokenLifetime,
				IncludeJwtId = clientName.IncludeJwtId,
				LogoutUri = clientName.LogoutUri,
				PrefixClientClaims = clientName.PrefixClientClaims,
				RefreshTokenExpiration = clientName.RefreshTokenExpiration,
				RefreshTokenUsage = clientName.RefreshTokenUsage,
				SlidingRefreshTokenLifetime = clientName.SlidingRefreshTokenLifetime,
				UpdateAccessTokenOnRefresh = clientName.UpdateAccessTokenOnRefresh
			};
			foreach (var role in clientName.Roles)
			{
				client.Roles.Add(new Role
				{
					Id = role.Id,
					Description = role.Description,
					Name = role.Name
				});
			}
			foreach (var o in clientName.Users)
			{
				User tempUser = new User
				{
					Id = o.Id,
					Email = o.Email,
					AccessFailedCount = o.AccessFailedCount,
					EmailConfirmed = o.EmailConfirmed,
					LockoutEnabled = o.LockoutEnabled,
					LockoutEndDateUtc = o.LockoutEndDateUtc,
					PasswordHash = o.PasswordHash,
					PhoneNumber = o.PhoneNumber,
					PhoneNumberConfirmed = o.PhoneNumberConfirmed,
					SecurityStamp = o.SecurityStamp,
					TwoFactorEnabled = o.TwoFactorEnabled,
					UserName = o.UserName
				};
				tempUser.UserProfile = new UserProfile();
				if (o.UserProfile != null)
				{
					tempUser.UserProfile.FirstName = o.UserProfile.FirstName;
					tempUser.UserProfile.LastName = o.UserProfile.LastName;
					tempUser.UserProfile.Email = o.UserProfile.Email;
					tempUser.UserProfile.Mobile = o.UserProfile.Mobile;
					tempUser.UserProfile.InternalPhone = o.UserProfile.InternalPhone;
					tempUser.UserProfile.InternalPhoneCode = o.UserProfile.InternalPhoneCode;
					tempUser.UserProfile.EmployeeNumber = o.UserProfile.EmployeeNumber;
					tempUser.UserProfile.Birthdate = o.UserProfile.Birthdate;
					tempUser.UserProfile.ProfileImageUrl = o.UserProfile.ProfileImageUrl;
					tempUser.UserProfile.NationalCode = o.UserProfile.NationalCode;
					tempUser.UserProfile.PersonCode = o.UserProfile.PersonCode;
					tempUser.UserProfile.UserID = o.UserProfile.UserID;
				}
				client.Users.Add(tempUser);
			}

			foreach (var o in clientName.UserPermissions)
			{
				client.UserPermissions.Add(new UserClientPermission
				{
					Id = o.Id
				});
			}

			foreach (var p in clientName.Permissions)
			{
				client.Permissions.Add(new Permission
				{
					Id = p.Id,
					Name = p.Name
				});
			}
			foreach (var p in clientName.ClientSecrets)
			{
				client.ClientSecrets.Add(new ClientSecret
				{
					Id = p.Id,
					Value = p.Value,
					Type = p.Type,
					Description = p.Description,
					Expiration = p.Expiration,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientScopes)
			{
				client.ClientScopes.Add(new ClientScope
				{
					Id = p.Id,
					Scope = p.Scope,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientRedirectUris)
			{
				client.ClientRedirectUris.Add(new ClientRedirectUri
				{
					Id = p.Id,
					Uri = p.Uri,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientPostLogoutRedirectUris)
			{
				client.ClientPostLogoutRedirectUris.Add(new ClientPostLogoutRedirectUri
				{
					Id = p.Id,
					Uri = p.Uri,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientIdPRestrictions)
			{
				client.ClientIdPRestrictions.Add(new ClientIdPRestriction
				{
					Id = p.Id,
					Provider = p.Provider,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientCustomGrantTypes)
			{
				client.ClientCustomGrantTypes.Add(new ClientCustomGrantType
				{
					Id = p.Id,
					GrantType = p.GrantType,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientCorsOrigins)
			{
				client.ClientCorsOrigins.Add(new ClientCorsOrigin
				{
					Id = p.Id,
					Origin = p.Origin,
					Client_Id = p.Client_Id
				});
			}

			foreach (var p in clientName.ClientClaims)
			{
				client.ClientClaims.Add(new ClientClaim
				{
					Id = p.Id,
					Type = p.Type,
					Value = p.Value,
					Client_Id = p.Client_Id
				});
			}

			return client;
		}

		[NonTransactional]
		public Client GetClientByName(string name)
		{
			Client client = _clientRepository.GetAllAsQuery()
				.Where(o => o.ClientName == name).FirstOrDefault();

			if (client != null)
			{
				return GetClientById(client.Id);
			}

			return null;
		}

		[NonTransactional]
		public Client GetClientByAdminUsername(string username)
		{
			Client client = _clientRepository.GetAllAsQuery()
				.Where(o => o.Users.Any(u => u.UserName == username)).FirstOrDefault();

			if (client != null)
			{
				return GetClientById(client.Id);
			}

			return null;
		}

		public bool ExistsClientId(int id, string ClientId)
		{
			return _clientRepository
				.GetAllAsQuery().Any(o => o.Id != id && o.ClientId == ClientId);
		}

		public bool ExistsClientName(int id, string clientName)
		{
			return _clientRepository
				.GetAllAsQuery().Any(o => o.Id != id && o.ClientName == clientName);
		}

		public void AddClient(Client client)
		{
			List<int> permissionIdList = new List<int>();
			foreach (Permission item in client.Permissions)
			{
				permissionIdList.Add(item.Id);
			}
			client.Permissions.Clear();
			foreach (int item in permissionIdList)
			{
				Permission tRow = _permissionRepository.Get(item);
				if (tRow != null)
				{
					client.Permissions.Add(tRow);
				}
			}

			_clientRepository.Insert(client);
		}

		public void UpdateClient(Client newValueClient)
		{
			Client client = _clientRepository.Get(newValueClient.Id);

			if (client != null)
			{
				client.AbsoluteRefreshTokenLifetime = newValueClient.AbsoluteRefreshTokenLifetime;
				client.AccessTokenLifetime = newValueClient.AccessTokenLifetime;
				client.AccessTokenType = newValueClient.AccessTokenType;
				client.AllowAccessToAllGrantTypes = newValueClient.AllowAccessToAllGrantTypes;
				client.AllowAccessToAllScopes = newValueClient.AllowAccessToAllScopes;
				client.AllowAccessTokensViaBrowser = newValueClient.AllowAccessTokensViaBrowser;
				client.AllowClientCredentialsOnly = newValueClient.AllowClientCredentialsOnly;
				client.AllowRememberConsent = newValueClient.AllowRememberConsent;
				client.AlwaysSendClientClaims = newValueClient.AlwaysSendClientClaims;
				client.AuthorizationCodeLifetime = newValueClient.AuthorizationCodeLifetime;
				client.ClientName = newValueClient.ClientName;
				client.ClientUri = newValueClient.ClientUri;
				client.Enabled = newValueClient.Enabled;
				client.EnableLocalLogin = newValueClient.EnableLocalLogin;
				client.Flow = newValueClient.Flow;
				client.IdentityTokenLifetime = newValueClient.IdentityTokenLifetime;
				client.IncludeJwtId = newValueClient.IncludeJwtId;
				client.LogoUri = newValueClient.LogoUri;
				client.LogoutSessionRequired = newValueClient.LogoutSessionRequired;
				client.LogoutUri = newValueClient.LogoutUri;
				client.PrefixClientClaims = newValueClient.PrefixClientClaims;
				client.RefreshTokenExpiration = newValueClient.RefreshTokenExpiration;
				client.RefreshTokenUsage = newValueClient.RefreshTokenUsage;
				client.RequireConsent = newValueClient.RequireConsent;
				client.RequireSignOutPrompt = newValueClient.RequireSignOutPrompt;
				client.SlidingRefreshTokenLifetime = newValueClient.SlidingRefreshTokenLifetime;
				client.UpdateAccessTokenOnRefresh = newValueClient.UpdateAccessTokenOnRefresh;

				List<int> tempDeleteListId = new List<int>();
				foreach (ClientCorsOrigin item in client.ClientCorsOrigins)
				{
					ClientCorsOrigin tRow = newValueClient.ClientCorsOrigins.FirstOrDefault(o => o.Id == item.Id && item.Client_Id == client.Id);
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ClientCorsOrigin item in newValueClient.ClientCorsOrigins)
				{
					ClientCorsOrigin tRow = client.ClientCorsOrigins.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item.Id);
					if (tRow == null)
					{
						_clientCorsOriginRepository.Insert(new ClientCorsOrigin
						{
							Client_Id = client.Id,
							Origin = item.Origin
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ClientCorsOrigin tRow = client.ClientCorsOrigins.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item);
					if (tRow != null)
					{
						_clientCorsOriginRepository.Delete(tRow.Id);
					}
				}
				//////////////
				tempDeleteListId.Clear();
				foreach (ClientPostLogoutRedirectUri item in client.ClientPostLogoutRedirectUris)
				{
					ClientPostLogoutRedirectUri tRow = newValueClient.ClientPostLogoutRedirectUris.FirstOrDefault(o => o.Id == item.Id && item.Client_Id == client.Id);
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ClientPostLogoutRedirectUri item in newValueClient.ClientPostLogoutRedirectUris)
				{
					ClientPostLogoutRedirectUri tRow = client.ClientPostLogoutRedirectUris.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item.Id);
					if (tRow == null)
					{
						_clientPostLogoutRedirectUriRepository.Insert(new ClientPostLogoutRedirectUri
						{
							Client_Id = client.Id,
							Uri = item.Uri
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ClientPostLogoutRedirectUri tRow = client.ClientPostLogoutRedirectUris.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item);
					if (tRow != null)
					{
						_clientPostLogoutRedirectUriRepository.Delete(tRow.Id);
					}
				}
				//////////////
				tempDeleteListId.Clear();
				foreach (ClientRedirectUri item in client.ClientRedirectUris)
				{
					ClientRedirectUri tRow = newValueClient.ClientRedirectUris.FirstOrDefault(o => o.Id == item.Id && item.Client_Id == client.Id);
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ClientRedirectUri item in newValueClient.ClientRedirectUris)
				{
					ClientRedirectUri tRow = client.ClientRedirectUris.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item.Id);
					if (tRow == null)
					{
						_clientRedirectUriRepository.Insert(new ClientRedirectUri
						{
							Client_Id = client.Id,
							Uri = item.Uri
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ClientRedirectUri tRow = client.ClientRedirectUris.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item);
					if (tRow != null)
					{
						_clientRedirectUriRepository.Delete(tRow.Id);
					}
				}
				//////////////
				tempDeleteListId.Clear();
				foreach (ClientScope item in client.ClientScopes)
				{
					ClientScope tRow = newValueClient.ClientScopes.FirstOrDefault(o => o.Id == item.Id && item.Client_Id == client.Id);
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ClientScope item in newValueClient.ClientScopes)
				{
					ClientScope tRow = client.ClientScopes.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item.Id);
					if (tRow == null)
					{
						_clientScopeRepository.Insert(new ClientScope
						{
							Client_Id = client.Id,
							Scope = item.Scope
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ClientScope tRow = client.ClientScopes.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item);
					if (tRow != null)
					{
						_clientScopeRepository.Delete(tRow.Id);
					}
				}
				//////////////
				tempDeleteListId.Clear();
				foreach (ClientSecret item in client.ClientSecrets)
				{
					ClientSecret tRow = newValueClient.ClientSecrets.FirstOrDefault(o => o.Id == item.Id && item.Client_Id == client.Id);
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (ClientSecret item in newValueClient.ClientSecrets)
				{
					ClientSecret tRow = client.ClientSecrets.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item.Id);
					if (tRow == null)
					{
						_clientSecretRepository.Insert(new ClientSecret
						{
							Client_Id = client.Id,
							Description = item.Description,
							Expiration = item.Expiration,
							Type = item.Type,
							Value = item.Value
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					ClientSecret tRow = client.ClientSecrets.FirstOrDefault(o => o.Client_Id == client.Id && o.Id == item);
					if (tRow != null)
					{
						_clientSecretRepository.Delete(tRow.Id);
					}
				}
				///////////////////////
				tempDeleteListId.Clear();
				foreach (Permission item in client.Permissions)
				{
					Permission tRow = newValueClient.Permissions.FirstOrDefault(o => o.Id == item.Id);
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}

				if (tempDeleteListId.Count > 0)
				{
					foreach (int item in tempDeleteListId)
					{
						UserClientPermission upRow = _userClientPermissionRepository.GetAllAsQuery()
							.FirstOrDefault(o => o.Client.Id == client.Id && o.Permission.Id == item);

						if (upRow != null)
						{
							_userClientPermissionRepository.Delete(upRow.Id);
						}

						Permission tRow = _permissionRepository.Get(item);
						tRow?.Clients.Remove(client);
					}
				}
				foreach (Permission item in newValueClient.Permissions)
				{
					if (tempDeleteListId.All(o => o != item.Id))
					{
						Permission tRow = _permissionRepository.Get(item.Id);
						if (tRow != null)
						{
							client.Permissions.Add(tRow);
						}
					}
				}
				///////////////////////
				//tempDeleteListId.Clear();
				//foreach (var item in client.Users)
				//{
				//	var tRow = newValueClient.Users.Where(o => o.Id == item.Id).FirstOrDefault();
				//	if (tRow == null)
				//		tempDeleteListId.Add(item.Id);
				//}

				//if (tempDeleteListId.Count > 0)
				//{
				//	foreach (var item in tempDeleteListId)
				//	{
				//		var tRow = _clientUserRepository.Get(item);
				//		if (tRow != null)
				//			tRow.Clients.Remove(client);

				//	}
				//}
				////////
				_clientRepository.Update(client);
			}
		}

		public void DeleteClient(Client client)
		{
			Client currentClient = _clientRepository.Get(client.Id);
			if (currentClient != null)
			{
				//_clientRepository.Delete(currentClient);
				foreach (ClientClaim item in currentClient.ClientClaims)
				{
					_clientClaimRepository.Delete(item.Id);
				}

				foreach (ClientCorsOrigin item in currentClient.ClientCorsOrigins)
				{
					_clientCorsOriginRepository.Delete(item.Id);
				}

				foreach (ClientCustomGrantType item in currentClient.ClientCustomGrantTypes)
				{
					_clientCustomGrantTypeRepository.Delete(item.Id);
				}

				foreach (ClientIdPRestriction item in currentClient.ClientIdPRestrictions)
				{
					_clientIdPRestrictionRepository.Delete(item.Id);
				}

				foreach (ClientPostLogoutRedirectUri item in currentClient.ClientPostLogoutRedirectUris)
				{
					_clientPostLogoutRedirectUriRepository.Delete(item.Id);
				}

				foreach (ClientRedirectUri item in currentClient.ClientRedirectUris)
				{
					_clientRedirectUriRepository.Delete(item.Id);
				}

				foreach (ClientScope item in currentClient.ClientScopes)
				{
					_clientScopeRepository.Delete(item.Id);
				}

				foreach (ClientSecret item in currentClient.ClientSecrets)
				{
					_clientSecretRepository.Delete(item.Id);
				}

				if (currentClient.UserPermissions.Count > 0)
				{
					List<int> tlist = new List<int>();
					foreach (UserClientPermission item in currentClient.UserPermissions)
					{
						tlist.Add(item.Id);
					}
					foreach (int item in tlist)
					{
						UserClientPermission tRow = _userClientPermissionRepository.Get(item);
						if (tRow != null)
						{
							_userClientPermissionRepository.Delete(tRow.Id);
						}
					}
				}
				List<int> tempDeleteListId = new List<int>();
				if (currentClient.Users.Count > 0)
				{
					List<int> tlist = new List<int>();
					foreach (User item in currentClient.Users)
					{
						tlist.Add(item.Id);
					}
					foreach (int item in tlist)
					{
						User tRow = _clientUserRepository.Get(item);
						tRow?.Clients.Remove(currentClient);
					}
				}
				tempDeleteListId.Clear();
				if (currentClient.Roles.Count > 0)
				{
					List<int> tlist = new List<int>();
					foreach (Role item in currentClient.Roles)
					{
						tlist.Add(item.Id);
					}
					foreach (int item in tlist)
					{
						Role tRow = _roleRepository.Get(item);
						tRow?.Clients.Remove(currentClient);
					}
				}

				_clientRepository.Delete(client.Id);
			}
		}

		#endregion Client

		#region ClientClaim

		[NonTransactional]
		public IEnumerable<ClientClaim> GetClientClaim(int clientId)
		{
			return _clientClaimRepository.GetAllAsQuery()
				.Where(o => o.Client_Id == clientId).ToList();
		}

		public void AddClientClaim(int clientId, params ClientClaim[] clientClaim)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientClaim item in clientClaim)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientClaimRepository.Insert(item);
				}
			}
		}

		public void DeleteClientClaim(int clientId, params ClientClaim[] clientClaim)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientClaim item in clientClaim)
				{
					_clientClaimRepository.Delete(item);
				}
			}
		}

		public void UpdateClientClaim(int clientId, ClientClaim clientClaim)
		{
			Client client = _clientRepository.Get(clientId);
			ClientClaim currentClientClaim = _clientClaimRepository.Get(clientClaim.Id);

			if (client != null && currentClientClaim != null)
			{
				currentClientClaim.Type = clientClaim.Type;
				currentClientClaim.Value = clientClaim.Value;
			}

			_clientClaimRepository.Update(currentClientClaim);
		}

		#endregion ClientClaim

		#region ClientCorsOrigin

		[NonTransactional]
		public IEnumerable<ClientCorsOrigin> GetClientCorsOrigin(int clientId)
		{
			return _clientCorsOriginRepository.GetAllAsQuery()
				.Where(o => o.Client_Id == clientId).ToList();
		}

		public void AddClientCorsOrigin(int clientId, params ClientCorsOrigin[] corsOrgin)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientCorsOrigin item in corsOrgin)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientCorsOriginRepository.Insert(item);
				}
			}
		}

		public void DeleteClientCorsOrigin(int clientId, params ClientCorsOrigin[] corsOrgin)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientCorsOrigin item in corsOrgin)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientCorsOriginRepository.Delete(item);
				}
			}
		}

		public void UpdateClientCorsOrigin(int clientId, ClientCorsOrigin corsOrgin)
		{
			Client client = _clientRepository.Get(clientId);
			ClientCorsOrigin currentClientCorsOrigin = _clientCorsOriginRepository.Get(corsOrgin.Id);

			if (client != null && currentClientCorsOrigin != null)
			{
				currentClientCorsOrigin.Origin = corsOrgin.Origin;
			}

			_clientCorsOriginRepository.Update(currentClientCorsOrigin);
		}

		#endregion ClientCorsOrigin

		#region ClientCustomGrantTypes

		public IEnumerable<ClientCustomGrantType> GetClientCustomGrantTypes(int clientId)
		{
			return _clientCustomGrantTypeRepository.GetAllAsQuery()
				.ToList();
		}

		public void AddClientCustomGrantType(int clientId, params ClientCustomGrantType[] customGrantType)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientCustomGrantType item in customGrantType)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientCustomGrantTypeRepository.Insert(item);
				}
			}
		}

		public void DeleteClientCustomGrantType(int clientId, params ClientCustomGrantType[] customGrantType)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientCustomGrantType item in customGrantType)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientCustomGrantTypeRepository.Delete(item);
				}
			}
		}

		public void UpdateClientCustomGrantType(int clientId, ClientCustomGrantType customGrantType)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				ClientCustomGrantType oldValue = _clientCustomGrantTypeRepository.Get(customGrantType.Id);

				if (oldValue != null)
				{
					oldValue.GrantType = customGrantType.GrantType;
				}

				_clientCustomGrantTypeRepository.Insert(oldValue);
			}
		}

		#endregion ClientCustomGrantTypes

		#region ClientIdPRestrictions

		public IEnumerable<ClientIdPRestriction> GetClientIdPRestrictions(int clientId)
		{
			return _clientIdPRestrictionRepository.GetAllAsQuery().ToList();
		}

		public void AddClientIdPRestriction(int clientId, params ClientIdPRestriction[] idPRestriction)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientIdPRestriction item in idPRestriction)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientIdPRestrictionRepository.Insert(item);
				}
			}
		}

		public void DeleteClientIdPRestriction(int clientId, params ClientIdPRestriction[] idPRestriction)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientIdPRestriction item in idPRestriction)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientIdPRestrictionRepository.Insert(item);
				}
			}
		}

		public void UpdateClientIdPRestriction(int clientId, ClientIdPRestriction idPRestriction)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				ClientIdPRestriction oldValue = _clientIdPRestrictionRepository.Get(idPRestriction.Id);

				if (oldValue != null)
				{
					oldValue.Provider = idPRestriction.Provider;
				}

				_clientIdPRestrictionRepository.Insert(oldValue);
			}
		}

		#endregion ClientIdPRestrictions

		#region ClientPostLogoutRedirectUri

		[NonTransactional]
		public IEnumerable<ClientPostLogoutRedirectUri> GetClientPostLogoutRedirectUri(int clientId)
		{
			return _clientPostLogoutRedirectUriRepository.GetAllAsQuery()
				.Where(o => o.Client_Id == clientId).ToList();
		}

		public void AddClientPostLogoutRedirectUri(int clientId, params ClientPostLogoutRedirectUri[] postLogoutRedirectUri)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientPostLogoutRedirectUri item in postLogoutRedirectUri)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientPostLogoutRedirectUriRepository.Insert(item);
				}
			}
		}

		public void DeleteClientPostLogoutRedirectUri(int clientId, params ClientPostLogoutRedirectUri[] postLogoutRedirectUri)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientPostLogoutRedirectUri item in postLogoutRedirectUri)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientPostLogoutRedirectUriRepository.Delete(item);
				}
			}
		}

		public void UpdateClientPostLogoutRedirectUri(int clientId, ClientPostLogoutRedirectUri postLogoutRedirectUri)
		{
			Client client = _clientRepository.Get(clientId);
			ClientPostLogoutRedirectUri currentClientPostLogoutRedirectUri = _clientPostLogoutRedirectUriRepository.Get(postLogoutRedirectUri.Id);

			if (client != null && currentClientPostLogoutRedirectUri != null)
			{
				currentClientPostLogoutRedirectUri.Uri = postLogoutRedirectUri.Uri;
			}

			_clientPostLogoutRedirectUriRepository.Update(currentClientPostLogoutRedirectUri);
		}

		#endregion ClientPostLogoutRedirectUri

		#region ClientRedirectUri

		[NonTransactional]
		public IEnumerable<ClientRedirectUri> GetClientRedirectUri(int clientId)
		{
			return _clientRedirectUriRepository.GetAllAsQuery()
				.Where(o => o.Client_Id == clientId).ToList();
		}

		public void AddClientRedirectUri(int clientId, params ClientRedirectUri[] redirectUri)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientRedirectUri item in redirectUri)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientRedirectUriRepository.Insert(item);
				}
			}
		}

		public void DeleteClientRedirectUri(int clientId, params ClientRedirectUri[] redirectUri)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientRedirectUri item in redirectUri)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientRedirectUriRepository.Delete(item);
				}
			}
		}

		public void UpdateClientRedirectUri(int clientId, ClientRedirectUri redirectUri)
		{
			Client client = _clientRepository.Get(clientId);
			ClientRedirectUri currentClientRedirectUri = _clientRedirectUriRepository.Get(redirectUri.Id);

			if (client != null && currentClientRedirectUri != null)
			{
				currentClientRedirectUri.Uri = redirectUri.Uri;
			}

			_clientRedirectUriRepository.Update(currentClientRedirectUri);
		}

		#endregion ClientRedirectUri

		#region ClientScope

		[NonTransactional]
		public IEnumerable<ClientScope> GetClientScope(int clientId)
		{
			return _clientScopeRepository.GetAllAsQuery()
				.Where(o => o.Client_Id == clientId).ToList();
		}

		public void AddClientClientScope(int clientId, params ClientScope[] scope)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientScope item in scope)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientScopeRepository.Insert(item);
				}
			}
		}

		public void DeleteClientScope(int clientId, params ClientScope[] scope)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientScope item in scope)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientScopeRepository.Delete(item);
				}
			}
		}

		#endregion ClientScope

		#region ClientSecret

		[NonTransactional]
		public IEnumerable<ClientSecret> GetClientSecret(int clientId)
		{
			return _clientSecretRepository.GetAllAsQuery()
				.Where(o => o.Client_Id == clientId).ToList();
		}

		public void AddClientSecret(int clientId, params ClientSecret[] secret)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientSecret item in secret)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientSecretRepository.Insert(item);
				}
			}
		}

		public void DeleteClientSecret(int clientId, params ClientSecret[] secret)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (ClientSecret item in secret)
				{
					item.Client = client;
					item.Client_Id = clientId;

					_clientSecretRepository.Delete(item);
				}
			}
		}

		#endregion ClientSecret

		#region ClientRole

		[NonTransactional]
		public IEnumerable<Role> GetClientRole(int clientId)
		{
			return _clientRoleRepository.GetAllAsQuery()
				.Where(o => o.Clients.Any(c => c.Id == clientId)).ToList();
		}

		public void AddClientRole(int clientId, params Role[] role)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (Role item in role)
				{
					item.Clients.Add(client);

					_clientRoleRepository.Insert(item);
				}
			}
		}

		public void DeleteClientRole(int clientId, params Role[] role)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (Role item in role)
				{
					item.Clients.Remove(client);

					_clientRoleRepository.Delete(item);
				}
			}
		}

		#endregion ClientRole

		#region ClientUser

		[NonTransactional]
		public IEnumerable<User> GetClientUser(int clientId)
		{
			return _clientUserRepository.GetAllAsQuery()
				.Where(o => o.Clients.Any(c => c.Id == clientId)).ToList();
		}

		public void AddClientUser(int clientId, params User[] user)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (User item in user)
				{
					item.Clients.Add(client);

					_clientUserRepository.Insert(item);
				}
			}
		}

		public void DeleteClientUser(int clientId, params User[] user)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (User item in user)
				{
					item.Clients.Remove(client);

					_clientUserRepository.Delete(item);
				}
			}
		}

		#endregion ClientUser

		#region ClientPermission

		public IEnumerable<Permission> GetClientPermission(int clientId)
		{
			return _permissionRepository.GetAllAsQuery()
				.Where(o => o.Clients.Any(c => c.Id == clientId)).ToList();
		}

		public void AddClientPermission(int clientId, params Permission[] permission)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (Permission item in permission)
				{
					item.Clients.Add(client);

					_permissionRepository.Insert(item);
				}
			}
		}

		public void DeleteClientPermission(int clientId, params Permission[] permission)
		{
			Client client = _clientRepository.Get(clientId);

			if (client != null)
			{
				foreach (Permission item in permission)
				{
					item.Clients.Add(client);

					_permissionRepository.Delete(item);
				}
			}
		}

		public void UpdateClientPermission(int clientId, Permission permission)
		{
			Client client = _clientRepository.Get(clientId);
			Permission currentClientPermission = _permissionRepository.Get(permission.Id);

			if (client != null && currentClientPermission != null)
			{
				currentClientPermission.Name = permission.Name;
			}

			_permissionRepository.Update(currentClientPermission);
		}

		#endregion ClientPermission

		#endregion IClientService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_clientRepository != null)
			{
				_clientRepository.Dispose();
				_clientRepository = null;
			}

			if (_clientClaimRepository != null)
			{
				_clientClaimRepository.Dispose();
				_clientClaimRepository = null;
			}
			if (_clientCorsOriginRepository != null)
			{
				_clientCorsOriginRepository.Dispose();
				_clientCorsOriginRepository = null;
			}

			if (_clientPostLogoutRedirectUriRepository != null)
			{
				_clientPostLogoutRedirectUriRepository.Dispose();
				_clientPostLogoutRedirectUriRepository = null;
			}
			if (_clientRedirectUriRepository != null)
			{
				_clientRedirectUriRepository.Dispose();
				_clientRedirectUriRepository = null;
			}

			if (_clientScopeRepository != null)
			{
				_clientScopeRepository.Dispose();
				_clientScopeRepository = null;
			}
			if (_clientSecretRepository != null)
			{
				_clientSecretRepository.Dispose();
				_clientSecretRepository = null;
			}

			if (_clientRoleRepository != null)
			{
				_clientRoleRepository.Dispose();
				_clientRoleRepository = null;
			}

			if (_clientUserRepository != null)
			{
				_clientUserRepository.Dispose();
				_clientUserRepository = null;
			}
			if (_permissionRepository != null)
			{
				_permissionRepository.Dispose();
				_permissionRepository = null;
			}
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}