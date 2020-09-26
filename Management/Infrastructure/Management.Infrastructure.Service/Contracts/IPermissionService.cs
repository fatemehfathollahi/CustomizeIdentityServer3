using Framework.Core.Contracts.Services;
using Management.Infrastructure.Models;
using System.Collections.Generic;

namespace Management.Infrastructure.Service.Contracts
{
	public interface IPermissionService : IService
	{
		IEnumerable<Permission> GetPermissions();

		Permission FindPermissionByID(int id);

		Permission FindPermissionByName(string name);

		bool ExistsPermissionName(int id, string name);

		void CreatePermission(Permission permission);

		void UpdatePermission(Permission permission);

		void DeletePermission(Permission permission);
	}
}