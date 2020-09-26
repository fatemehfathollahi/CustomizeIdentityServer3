using Framework.Core.Contracts.Services;
using Management.Infrastructure.Facade.DTOModel;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.FacadeService.Contracts
{
	public interface IUserFacadeService : IFacadeService
	{
		#region User

		IEnumerable<UserDTO> GetUsers();

		IEnumerable<UserListDTO> GetUsers(UserDTO dto, int PageIndex, int PageSize);

		int GetTotalCount(UserDTO dto);

		UserDTO FindUserById(int userId);

		UserDTO FindUserByName(string userName);

		bool ExistsUerName(int id, string userName);

		void CreateUser(UserDTO user);

		void UpdateUser(UserDTO user);

		void UpdatePassword(int userId, string password);

		void DeleteUser(UserDTO user);

		void DeleteUser(int id);

		void UpdateUserProfile(int userId, UserProfileDTO profile);

		bool IsSamePassword(int userId, string newPassword);

		#endregion User

		#region UserClaim

		IEnumerable<UserClaimDTO> GetUserClaims(int userId);

		void AddUserClaim(int userId, UserClaimDTO claim);

		void RemoveUserClaim(int userId, UserClaimDTO claim);

		void UpdateUserClaim(int userId, UserClaimDTO claim);

		#endregion UserClaim

		#region UserRoles

		void AddToRole(int userId, string roleName);

		void RemoveFromRole(int userId, string roleName);

		IEnumerable<RoleDTO> GetRoles(int userId);

		#endregion UserRoles
	}
}