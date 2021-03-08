using Framework.Core.Contracts.Security;
using Framework.Core.Security;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using Management.Infrastructure.Models;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Management.Infrastructure.Facade.FacadeServices
{
	public class UserFacadeService : IUserFacadeService
	{
		#region Internal Fields

		internal IMapper Mapper;
		internal IUserService UserService;
		internal IClientService ClientService;
		internal IPermissionService PermissionService;
		private readonly IPasswordHasher _hasher;

		#endregion Internal Fields

		#region Ctor

		public UserFacadeService(IMapper mapper, IUserService userService, IClientService clientService, IPermissionService permissionService)
		{
			Mapper = mapper;
			UserService = userService;
			_hasher = new PasswordHasher();
			ClientService = clientService;
			PermissionService = permissionService;
		}

		#endregion Ctor

		#region IPermissionFacadeService Members

		#region User

		public IEnumerable<UserDTO> GetUsers()
		{
			IEnumerable<User> users = UserService.GetUsers();

			return Mapper.Map<List<UserDTO>>(users);
		}

		public IEnumerable<UserListDTO> GetUsers(UserDTO dto, int PageIndex, int PageSize)
		{
			var model = Mapper.Map<User>(dto);
			IEnumerable<User> result = UserService.GetUsers(model, PageIndex, PageSize);
			return Mapper.Map<List<UserListDTO>>(result);
		}

		public int GetTotalCount(UserDTO dto)
		{
			var model = Mapper.Map<User>(dto);
			return UserService.GetTotalCount(model);
		}

		public UserDTO FindUserById(int userId)
		{
			User users = UserService.FindUserById(userId);
			users.PasswordHash = "";
			return Mapper.Map<UserDTO>(users);
		}

		public UserDTO FindUserByName(string userName)
		{
			User users = UserService.FindUserByName(userName);

			return Mapper.Map<UserDTO>(users);
		}

		public bool IsSamePassword(int userId, string password)
		{
			string userPass = GetUserPassword(userId);
			return _hasher.VerifyHashedPassword(userPass, password) == PasswordVerificationResult.Success;
		}

		public void CreateUser(UserDTO user)
		{
			var model = Mapper.Map<User>(user);

			model.PasswordHash = _hasher.HashPassword(model.PasswordHash);
            model.PhoneNumber = user.Mobile;//for recovery password add by fathollahi 99/12/177
			UserService.CreateUser(model);
		}

		public void UpdateUser(UserDTO user)
		{
			var model = Mapper.Map<User>(user);

			model.PasswordHash = _hasher.HashPassword(model.PasswordHash);

			UserService.UpdateUser(model);
		}

		public void UpdatePassword(int userId, string password)
		{
			string PasswordHash = _hasher.HashPassword(password);
			UserService.UpdatePassword(userId, PasswordHash);
		}

		public void DeleteUser(UserDTO user)
		{
			var model = Mapper.Map<User>(user);

			UserService.DeleteUser(model);
		}

		public bool ExistsUerName(int id, string userName)
		{
			return UserService.ExistsUerName(id, userName);
		}

		public void DeleteUser(int id)
		{
			UserService.DeleteUser(id);
		}

		public void UpdateUserProfile(int userId, UserProfileDTO profile)
		{
			var model = Mapper.Map<UserProfile>(profile);

			UserService.UpdateUserProfile(userId, model);
		}

		private string GetUserPassword(int id)
		{
			return UserService.FindUserById(id).PasswordHash;
		}

		#endregion User

		#region UserClaim

		public IEnumerable<UserClaimDTO> GetUserClaims(int userId)
		{
			IEnumerable<UserClaim> userClaims = UserService.GetUserClaims(userId);

			return Mapper.Map<List<UserClaimDTO>>(userClaims);
		}

		public void AddUserClaim(int userId, UserClaimDTO claim)
		{
			UserService.AddUserClaim(userId, Mapper.Map<UserClaim>(claim));
		}

		public void RemoveUserClaim(int userId, UserClaimDTO claim)
		{
			UserService.RemoveUserClaim(userId, Mapper.Map<UserClaim>(claim));
		}

		public void UpdateUserClaim(int userId, UserClaimDTO claim)
		{
			UserService.UpdateUserClaim(userId, Mapper.Map<UserClaim>(claim));
		}

		#endregion UserClaim

		#region UserRoles

		public void AddToRole(int userId, string roleName)
		{
			UserService.AddToRole(userId, roleName);
		}

		public void RemoveFromRole(int userId, string roleName)
		{
			UserService.RemoveFromRole(userId, roleName);
		}

		public IEnumerable<RoleDTO> GetRoles(int userId)
		{
			IEnumerable<Role> roles = UserService.GetRoles(userId);

			return Mapper.Map<List<RoleDTO>>(roles);
		}

		#endregion UserRoles

		#endregion IPermissionFacadeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (Mapper != null)
			{
				Mapper = null;
			}

			if (UserService != null)
			{
				UserService.Dispose();
				UserService = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}