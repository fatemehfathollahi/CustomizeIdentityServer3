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
	public class PermissionService : IPermissionService
	{
		#region Fields

		private IPermissionRepository _permissionRepository;
		private IClientRepository _clientRepository;
		private IUserRepository _userRepository;
		private IUserClientPermissionRepository _userClientPermissionRepository;
		private QueryDbContext _queryDbContext;

		#endregion Fields

		#region Ctors

		public PermissionService(IPermissionRepository permissionRepository, IClientRepository clientRepository
									, IUserRepository userRepository, IUserClientPermissionRepository userClientPermissionRepository
									, QueryDbContext queryDbContext)
		{
			_permissionRepository = permissionRepository;
			_clientRepository = clientRepository;
			_userRepository = userRepository;
			_userClientPermissionRepository = userClientPermissionRepository;
			_queryDbContext = queryDbContext;
		}

		#endregion Ctors

		#region IPermissionService Members

		[NonTransactional]
		public IEnumerable<Permission> GetPermissions()
		{
			return _permissionRepository.GetAllAsQuery()
				.ToList();
		}

		[NonTransactional]
		public Permission FindPermissionByID(int id)
		{
			//return _permissionRepository.Get(id);

			var tempPermission = (from per in _queryDbContext.Permissions
								  where per.Id == id
								  select new
								  {
									  Id = per.Id,
									  Name = per.Name,
									  Roles = per.Roles.Select(o => new
									  {
										  Id = o.Id,
										  Description = o.Description,
										  Name = o.Name
									  }).ToList(),
									  Clients = per.Clients.Select(o => new
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
									  }).ToList(),
									  Users = per.Users.Select(o => new
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
										  UserName = o.UserName,
										  UserProfile = o.UserProfile
									  }).ToList(),
									  UserPermissions = per.UserPermissions.Select(o => new
									  {
										  Id = o.Id
									  }).ToList()
								  }).FirstOrDefault();

			Permission permission = new Permission()
			{
				Id = tempPermission.Id,
				Name = tempPermission.Name,
			};
			foreach (var role in tempPermission.Roles)
			{
				permission.Roles.Add(new Role
				{
					Id = role.Id,
					Description = role.Description,
					Name = role.Name
				});
			}

			foreach (var o in tempPermission.Clients)
			{
				permission.Clients.Add(new Client
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
				});
			}

			foreach (var o in tempPermission.Users)
			{
				permission.Users.Add(new User
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
					UserName = o.UserName,
					UserProfile = new UserProfile
					{
						FirstName = o.UserProfile.FirstName,
						LastName = o.UserProfile.LastName,
						Email = o.UserProfile.Email,
						Mobile = o.UserProfile.Mobile,
						InternalPhone = o.UserProfile.InternalPhone,
						InternalPhoneCode = o.UserProfile.InternalPhoneCode,
						EmployeeNumber = o.UserProfile.EmployeeNumber,
						Birthdate = o.UserProfile.Birthdate,
						ProfileImageUrl = o.UserProfile.ProfileImageUrl,
						NationalCode = o.UserProfile.NationalCode,
						PersonCode = o.UserProfile.PersonCode,
						UserID = o.UserProfile.UserID
					}
				});
			}

			foreach (var o in tempPermission.UserPermissions)
			{
				permission.UserPermissions.Add(new UserClientPermission
				{
					Id = o.Id
				});
			}
			return permission;
		}

		[NonTransactional]
		public Permission FindPermissionByName(string name)
		{
			Permission permission = _permissionRepository.Get(o => o.Name == name).FirstOrDefault();
			if (permission != null)
			{
				return FindPermissionByID(permission.Id);
			}

			return null;
		}

		[NonTransactional]
		public bool ExistsPermissionName(int id, string name)
		{
			return _permissionRepository.Get(o => o.Id != id && o.Name == name).Any();
		}

		public void CreatePermission(Permission permission)
		{
			_permissionRepository.Insert(permission);
		}

		public void DeletePermission(Permission permission)
		{
			Permission temp = _permissionRepository.Get(o => o.Id == permission.Id).FirstOrDefault();
			if (temp != null)
			{
				if (temp.UserPermissions.Count > 0)
				{
					List<int> tlist = new List<int>();
					foreach (UserClientPermission item in temp.UserPermissions)
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
				if (temp.Clients.Count > 0)
				{
					List<int> tlist = new List<int>();
					foreach (Client item in temp.Clients)
					{
						tlist.Add(item.Id);
					}
					foreach (int item in tlist)
					{
						Client tRow = _clientRepository.Get(item);
						if (tRow != null)
						{
							tRow.Permissions.Remove(temp);
						}
					}
				}
				if (temp.Users.Count > 0)
				{
					List<int> tlist = new List<int>();
					foreach (User item in temp.Users)
					{
						tlist.Add(item.Id);
					}
					foreach (int item in tlist)
					{
						User tRow = _userRepository.Get(item);
						if (tRow != null)
						{
							tRow.Permissions.Remove(temp);
						}
					}
				}
				_permissionRepository.Delete(temp);
			}
		}

		public void UpdatePermission(Permission permission)
		{
			Permission currentPermission = _permissionRepository.Get(permission.Id);

			currentPermission.Name = permission.Name;

			_permissionRepository.Update(currentPermission);
		}

		#endregion IPermissionService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_permissionRepository != null)
			{
				_permissionRepository.Dispose();
				_permissionRepository = null;
			}
			_queryDbContext = null;
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}