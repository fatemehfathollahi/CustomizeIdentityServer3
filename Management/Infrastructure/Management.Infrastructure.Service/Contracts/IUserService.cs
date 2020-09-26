using Framework.Core.Contracts.Services;
using Management.Infrastructure.Models;
using System.Collections.Generic;

namespace Management.Infrastructure.Service.Contracts
{
	public interface IUserService : IService
	{
		#region User

		IEnumerable<User> GetUsers();

		IEnumerable<User> GetUsers(User dto, int PageIndex, int PageSize);

		int GetTotalCount(User dto);

		User FindUserById(int userId);

		User FindUserByName(string userName);

		bool ExistsUerName(int id, string userName);

		void CreateUser(User user);

		void UpdateUser(User user);

		void UpdatePassword(int userId, string password);

		void DeleteUser(User user);

		void DeleteUser(int id);

		void UpdateUserProfile(int userId, UserProfile profile);

		#endregion User

		#region UserClaim

		IEnumerable<UserClaim> GetUserClaims(int userId);

		void AddUserClaim(int userId, UserClaim claim);

		void RemoveUserClaim(int userId, UserClaim claim);

		void UpdateUserClaim(int userId, UserClaim claim);

		#endregion UserClaim

		#region UserRoles

		void AddToRole(int userId, string roleName);

		void RemoveFromRole(int userId, string roleName);

		IEnumerable<Role> GetRoles(int userId);

		#endregion UserRoles
	}
}