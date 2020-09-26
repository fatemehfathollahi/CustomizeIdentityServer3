using Framework.Core.Contracts.Services;
using Management.Infrastructure.Facade.DTOModel;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.FacadeService.Contracts
{
	public interface IRoleFacadeService : IFacadeService
	{
		#region Role Members

		IEnumerable<RoleDTO> GetRoles();

		IEnumerable<RoleDTO> GetRolesByClient(int clientId);

		RoleDTO FindRoleById(int id);

		RoleDTO FindRoleByName(string name);

		void CreateRole(RoleDTO role);

		void CreateRole(int clientId, RoleDTO role);

		void UpdateRole(RoleDTO role);

		void DeleteRole(RoleDTO role);

		#endregion Role Members

		#region Permissions Members

		IEnumerable<PermissionDTO> GetRolePermissions(int roleId);

		void AddRolePermissions(int roleId, params PermissionDTO[] permissions);

		void RemoveRolePermissions(int roleId, params PermissionDTO[] permissions);

		#endregion Permissions Members
	}
}