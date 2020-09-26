using Framework.Core.Common.Attributes;
using Framework.Core.Common.Predicate;
using Management.Infrastructure.Data;
using Management.Infrastructure.Models;
using Management.Infrastructure.Models.Repositories;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Infrastructure.Service
{
	public class UserService : IUserService
	{
		#region Fields

		private IUserRepository _userRepository;
		private IUserProfileRepository _userProfileRepository;
		private IUserClaimRepository _userClaimRepository;
		private IRoleRepository _roleRepository;
		private IUserClientPermissionRepository _userClientPermissionRepository;
		private IClientRepository _clientRepository;
		private IPermissionRepository _permissionRepository;
		private QueryDbContext _queryDbContext;

		#endregion Fields

		#region Ctor

		public UserService(IUserRepository userRepository, IUserProfileRepository userProfileRepository
			, IUserClaimRepository userClaimRepository
			, IRoleRepository roleRepository
			, IClientRepository clientRepository
			, IPermissionRepository permissionRepository
			, IUserClientPermissionRepository userClientPermissionRepository
			, QueryDbContext queryDbContext
			)
		{
			_userRepository = userRepository;
			_userProfileRepository = userProfileRepository;
			_userClaimRepository = userClaimRepository;
			_roleRepository = roleRepository;
			_userClientPermissionRepository = userClientPermissionRepository;
			_clientRepository = clientRepository;
			_permissionRepository = permissionRepository;
			_queryDbContext = queryDbContext;
		}

		#endregion Ctor

		#region IUserService Members

		#region User

		[NonTransactional]
		public IEnumerable<User> GetUsers()
		{
			return _userRepository.GetAll();
		}

		[NonTransactional]
		public IEnumerable<User> GetUsers(User dto, int PageIndex, int PageSize)
		{
			System.Linq.Expressions.Expression<Func<User, bool>> pred = GetUserExpresion(dto);
			List<User> result = _userRepository
								.GetAllAsQuery()
								.Where(pred)
								.OrderBy(m => m.Id).Skip(PageIndex).Take(PageSize)
						.ToList();

			foreach (User item in result)
			{
				if (item.UserProfile == null)
				{
					item.UserProfile = new UserProfile();
				}
			}

			return result;
		}

		[NonTransactional]
		public int GetTotalCount(User dto)
		{
			System.Linq.Expressions.Expression<Func<User, bool>> pred = GetUserExpresion(dto);
			return _userRepository.Get(pred).Count();
		}

		[NonTransactional]
		public User FindUserById(int userId)
		{
			//var result = _userRepository.Get(userId);
			var clientTemp = (from p in _queryDbContext.Users
							  where p.Id == userId
							  select new
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
								  UserProfile = p.UserProfile,
								  Clients = p.Clients.Select(o => new
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
								  }),
								  Permissions = p.Permissions.Select(o => new
								  {
									  Id = o.Id,
									  Name = o.Name
								  }),
								  Roles = p.Roles.Select(o => new
								  {
									  Id = o.Id,
									  Description = o.Description,
									  Name = o.Name
								  }),
								  UserPermissions = p.UserPermissions.Select(o => new
								  {
									  Id = o.Id,
									  Client = o.Client,
									  Permission = o.Permission
								  }),
								  UserLogins = p.UserLogins.Select(o => new
								  {
									  LoginProvider = o.LoginProvider,
									  ProviderKey = o.ProviderKey,
									  UserId = o.UserId,
								  }),
								  UserClaims = p.UserClaims.Select(o => new
								  {
									  Id = o.Id,
									  UserId = o.UserId,
									  ClaimType = o.ClaimType,
									  ClaimValue = o.ClaimValue
								  }),
							  }).FirstOrDefault();

			User user = new User
			{
				Id = clientTemp.Id,
				Email = clientTemp.Email,
				AccessFailedCount = clientTemp.AccessFailedCount,
				EmailConfirmed = clientTemp.EmailConfirmed,
				LockoutEnabled = clientTemp.LockoutEnabled,
				LockoutEndDateUtc = clientTemp.LockoutEndDateUtc,
				PasswordHash = clientTemp.PasswordHash,
				PhoneNumber = clientTemp.PhoneNumber,
				PhoneNumberConfirmed = clientTemp.PhoneNumberConfirmed,
				SecurityStamp = clientTemp.SecurityStamp,
				TwoFactorEnabled = clientTemp.TwoFactorEnabled,
				UserName = clientTemp.UserName
			};
			user.UserProfile = new UserProfile();
			if (clientTemp.UserProfile != null)
			{
				user.UserProfile.FirstName = clientTemp.UserProfile.FirstName;
				user.UserProfile.LastName = clientTemp.UserProfile.LastName;
				user.UserProfile.Email = clientTemp.UserProfile.Email;
				user.UserProfile.Mobile = clientTemp.UserProfile.Mobile;
				user.UserProfile.InternalPhone = clientTemp.UserProfile.InternalPhone;
				user.UserProfile.InternalPhoneCode = clientTemp.UserProfile.InternalPhoneCode;
				user.UserProfile.EmployeeNumber = clientTemp.UserProfile.EmployeeNumber;
				user.UserProfile.Birthdate = clientTemp.UserProfile.Birthdate;
				user.UserProfile.ProfileImageUrl = clientTemp.UserProfile.ProfileImageUrl;
				user.UserProfile.NationalCode = clientTemp.UserProfile.NationalCode;
				user.UserProfile.PersonCode = clientTemp.UserProfile.PersonCode;
				user.UserProfile.UserID = clientTemp.UserProfile.UserID;
			}
			foreach (var o in clientTemp.Permissions)
			{
				user.Permissions.Add(new Permission
				{
					Id = o.Id,
					Name = o.Name
				});
			}
			foreach (var o in clientTemp.Clients)
			{
				user.Clients.Add(new Client
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
			foreach (var o in clientTemp.Roles)
			{
				user.Roles.Add(new Role
				{
					Id = o.Id,
					Description = o.Description,
					Name = o.Name
				});
			}
			foreach (var o in clientTemp.UserPermissions)
			{
				UserClientPermission uc = new UserClientPermission
				{
					Id = o.Id
				};
				if (o.Client != null)
				{
					uc.Client = new Client
					{
						Id = o.Client.Id,
						Enabled = o.Client.Enabled,
						ClientId = o.Client.ClientId,
						ClientUri = o.Client.ClientUri,
						LogoUri = o.Client.LogoUri,
						RequireConsent = o.Client.RequireConsent,
						AllowAccessToAllGrantTypes = o.Client.AllowAccessToAllGrantTypes,
						AllowAccessToAllScopes = o.Client.AllowAccessToAllScopes,
						AllowAccessTokensViaBrowser = o.Client.AllowAccessTokensViaBrowser,
						AllowClientCredentialsOnly = o.Client.AllowClientCredentialsOnly,
						AllowRememberConsent = o.Client.AllowRememberConsent,
						Flow = o.Client.Flow,
						LogoutSessionRequired = o.Client.LogoutSessionRequired,
						RequireSignOutPrompt = o.Client.RequireSignOutPrompt,
						AlwaysSendClientClaims = o.Client.AlwaysSendClientClaims,
						AbsoluteRefreshTokenLifetime = o.Client.AbsoluteRefreshTokenLifetime,
						AccessTokenLifetime = o.Client.AccessTokenLifetime,
						AccessTokenType = o.Client.AccessTokenType,
						AuthorizationCodeLifetime = o.Client.AuthorizationCodeLifetime,
						ClientName = o.Client.ClientName,
						EnableLocalLogin = o.Client.EnableLocalLogin,
						IdentityTokenLifetime = o.Client.IdentityTokenLifetime,
						IncludeJwtId = o.Client.IncludeJwtId,
						LogoutUri = o.Client.LogoutUri,
						PrefixClientClaims = o.Client.PrefixClientClaims,
						RefreshTokenExpiration = o.Client.RefreshTokenExpiration,
						RefreshTokenUsage = o.Client.RefreshTokenUsage,
						SlidingRefreshTokenLifetime = o.Client.SlidingRefreshTokenLifetime,
						UpdateAccessTokenOnRefresh = o.Client.UpdateAccessTokenOnRefresh
					};
				}
				if (o.Permission != null)
				{
					uc.Permission = new Permission
					{
						Id = o.Permission.Id,
						Name = o.Permission.Name
					};
				}
				user.UserPermissions.Add(uc);
			}
			foreach (var o in clientTemp.UserLogins)
			{
				user.UserLogins.Add(new UserLogin
				{
					LoginProvider = o.LoginProvider,
					ProviderKey = o.ProviderKey,
					UserId = o.UserId,
				});
			}
			foreach (var o in clientTemp.UserClaims)
			{
				user.UserClaims.Add(new UserClaim
				{
					Id = o.Id,
					UserId = o.UserId,
					ClaimType = o.ClaimType,
					ClaimValue = o.ClaimValue
				});
			}
			return user;
		}

		[NonTransactional]
		public User FindUserByName(string userName)
		{
			User users = _userRepository.Get(o => o.UserName == userName).FirstOrDefault();
			if (users != null)
			{
				return FindUserById(users.Id);
			}

			return null;
		}

		[NonTransactional]
		public bool ExistsUerName(int id, string userName)
		{
			return _userRepository.Get(o => o.Id != id && o.UserName == userName).Any();
		}

		public void CreateUser(User user)
		{
			List<UserClientPermission> list = new List<UserClientPermission>();
			foreach (UserClientPermission item in user.UserPermissions)
			{
				item.Client = _clientRepository.Get(item.Client.Id);
				item.Permission = _permissionRepository.Get(item.Permission.Id);
				item.User = user;
			}

			_userRepository.Insert(user);
		}

		public void UpdateUser(User user)
		{
			User currentUser = _userRepository.Get(user.Id);

			if (currentUser != null)
			{
				currentUser.AccessFailedCount = user.AccessFailedCount;
				currentUser.Email = user.Email;
				currentUser.EmailConfirmed = user.EmailConfirmed;
				currentUser.LockoutEnabled = user.LockoutEnabled;
				currentUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
				currentUser.PasswordHash = user.PasswordHash;
				currentUser.PhoneNumber = user.PhoneNumber;
				currentUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
				currentUser.SecurityStamp = user.SecurityStamp;
				currentUser.TwoFactorEnabled = user.TwoFactorEnabled;

				//var pRow = _userProfileRepository.Get(currentUser.Id);
				//if (pRow != null)
				//	_userProfileRepository.Delete(currentUser.Id);

				UserProfile UserProfile = _userProfileRepository.Get(currentUser.Id);

				if (UserProfile != null)
				{
					UserProfile.FirstName = user.UserProfile.FirstName;
					UserProfile.LastName = user.UserProfile.LastName;
					UserProfile.Mobile = user.UserProfile.Mobile;
					UserProfile.NationalCode = user.UserProfile.NationalCode;
					UserProfile.InternalPhone = user.UserProfile.InternalPhone;
					UserProfile.InternalPhoneCode = user.UserProfile.InternalPhoneCode;

					//_userProfileRepository.Insert(new UserProfile
					//{
					//	UserID = user.Id,
					//	FirstName = user.UserProfile.FirstName,
					//	LastName = user.UserProfile.LastName,
					//	Mobile = user.UserProfile.Mobile,
					//	NationalCode = user.UserProfile.NationalCode,
					//	InternalPhone = user.UserProfile.InternalPhone,
					//	InternalPhoneCode = user.UserProfile.InternalPhoneCode
					//});
				}
				List<int> lstTemp = new List<int>();
				foreach (UserClientPermission item in currentUser.UserPermissions)
				{
					lstTemp.Add(item.Id);
				}

				foreach (int item in lstTemp)
				{
					_userClientPermissionRepository.Delete(item);
				}

				foreach (UserClientPermission item in user.UserPermissions)
				{
					_userClientPermissionRepository.Insert(new UserClientPermission
					{
						Client = _clientRepository.Get(item.Client.Id),
						Permission = _permissionRepository.Get(item.Permission.Id),
						User = currentUser
					});
				}

				_userProfileRepository.Update(UserProfile);

				_userRepository.Update(currentUser);
			}
		}

		public void UpdatePassword(int userId, string password)
		{
			User currentUser = _userRepository.Get(userId);

			if (currentUser != null)
			{
				currentUser.PasswordHash = password;

				_userRepository.Update(currentUser);
			}
		}

		public void DeleteUser(int id)
		{
			User user = _userRepository.Get(id);

			if (user != null)
			{
				_userRepository.Delete(user);
			}
		}

		public void DeleteUser(User user)
		{
			List<UserClientPermission> list = new List<UserClientPermission>();
			foreach (UserClientPermission item in user.UserPermissions)
			{
				_userClientPermissionRepository.Delete(item.Id);
			}

			_userProfileRepository.Delete(user.Id);
			_userRepository.Delete(user.Id);
		}

		public void UpdateUserProfile(int userId, UserProfile profile)
		{
			User user = _userRepository.Get(userId);

			if (user != null)
			{
				UserProfile currentUserProfile = _userProfileRepository.Get(user.Id);

				if (currentUserProfile != null)
				{
					currentUserProfile.Birthdate = profile.Birthdate;
					currentUserProfile.Email = profile.Email;
					currentUserProfile.EmployeeNumber = profile.EmployeeNumber;
					currentUserProfile.FirstName = profile.FirstName;
					currentUserProfile.InternalPhone = profile.InternalPhone;
					currentUserProfile.InternalPhoneCode = profile.InternalPhoneCode;
					currentUserProfile.LastName = profile.LastName;
					currentUserProfile.Mobile = profile.Mobile;
					currentUserProfile.NationalCode = profile.NationalCode;
					currentUserProfile.PersonCode = profile.PersonCode;
					currentUserProfile.ProfileImageUrl = profile.ProfileImageUrl;

					_userProfileRepository.Update(currentUserProfile);
				}
			}
		}

		#endregion User

		#region UserClaim

		[NonTransactional]
		public IEnumerable<UserClaim> GetUserClaims(int userId)
		{
			return _userClaimRepository.Get(o => o.UserId == userId);
		}

		public void AddUserClaim(int userId, UserClaim claim)
		{
			User user = _userRepository.Get(userId);

			if (user != null)
			{
				claim.User = user;
				claim.UserId = userId;

				_userClaimRepository.Insert(claim);
			}
		}

		public void UpdateUserClaim(int userId, UserClaim claim)
		{
			User user = _userRepository.Get(userId);
			UserClaim currentUserClaim = _userClaimRepository.Get(claim.Id);

			if (user != null && currentUserClaim != null)
			{
				currentUserClaim.ClaimType = claim.ClaimType;
				currentUserClaim.ClaimValue = claim.ClaimValue;

				_userClaimRepository.Update(currentUserClaim);
			}
		}

		public void RemoveUserClaim(int userId, UserClaim claim)
		{
			User user = _userRepository.Get(userId);

			if (user != null)
			{
				_userClaimRepository.Delete(claim);
			}
		}

		#endregion UserClaim

		#region UserRoles

		[NonTransactional]
		public IEnumerable<Role> GetRoles(int userId)
		{
			return _roleRepository.Get(o => o.Users.Any(r => r.Id == userId));
		}

		public void AddToRole(int userId, string roleName)
		{
			Role role = _roleRepository.Get(o => o.Name == roleName).FirstOrDefault();
			User user = _userRepository.Get(userId);

			if (user != null && role != null)
			{
				user.Roles.Add(role);
				user.UserClaims.Add(new UserClaim { ClaimType = "role", ClaimValue = role.Name });

				_userRepository.Update(user);
			}
		}

		public void RemoveFromRole(int userId, string roleName)
		{
			Role role = _roleRepository.Get(o => o.Name == roleName).FirstOrDefault();
			User user = _userRepository.Get(userId);

			if (user != null && role != null)
			{
				user.Roles.Remove(role);
				user.UserClaims.Remove(new UserClaim { ClaimType = "role", ClaimValue = role.Name });

				_userRepository.Update(user);
			}
		}

		#endregion UserRoles

		#endregion IUserService Members

		#region private Method(s)

		private System.Linq.Expressions.Expression<Func<User, bool>> GetUserExpresion(User user)
		{
			System.Linq.Expressions.Expression<Func<User, bool>> predicate = PredicateBuilder.True<User>();

			if (!string.IsNullOrEmpty(user.UserName))
			{
				predicate = predicate.And(i => i.UserName == user.UserName);
			}

			return predicate;
		}

		#endregion private Method(s)

		#region IDisposable Members

		public void Dispose()
		{
			if (_userRepository != null)
			{
				_userRepository.Dispose();
				_userRepository = null;
			}

			if (_userProfileRepository != null)
			{
				_userProfileRepository.Dispose();
				_userProfileRepository = null;
			}

			if (_userClaimRepository != null)
			{
				_userClaimRepository.Dispose();
				_userClaimRepository = null;
			}

			if (_roleRepository != null)
			{
				_roleRepository.Dispose();
				_roleRepository = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}