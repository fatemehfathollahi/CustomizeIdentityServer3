using Framework.Core.Contracts.Services;
using Management.Infrastructure.Facade.DTOModel;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.FacadeService.Contracts
{
	public interface IPermissionFacadeService : IFacadeService
	{
		IEnumerable<PermissionListDTO> GetPermissions();

		PermissionDTO FindPermissionByID(int id);

		PermissionDTO FindPermissionByName(string name);

		void CreatePermission(PermissionDTO permission);

		void UpdatePermission(PermissionDTO permission);

		void DeletePermission(PermissionDTO permission);

		bool ExistsPermissionName(int id, string Name);
	}
}