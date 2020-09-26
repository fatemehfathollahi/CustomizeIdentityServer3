using Framework.Core.Contracts.Services;
using Management.Infrastructure.Models;
using System.Collections.Generic;

namespace Management.Infrastructure.Service.Contracts
{
	public interface IRoleService : IService
	{
		#region Role Members

		IEnumerable<Role> GetRoles();

		IEnumerable<Role> GetRolesByClient(int clientId);

		Role FindRoleById(int id);

		Role FindRoleByName(string name);

		void CreateRole(Role role);

		void CreateRole(int clientId, Role role);

		void UpdateRole(Role role);

		void DeleteRole(Role role);

		#endregion Role Members

		#region Permissions Members

		IEnumerable<Permission> GetRolePermissions(int roleId);

		void AddRolePermissions(int roleId, params Permission[] permissions);

		void RemoveRolePermissions(int roleId, params Permission[] permissions);

		#endregion Permissions Members
	}
}