using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using Management.Infrastructure.Models;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Management.Infrastructure.Facade.FacadeServices
{
	public class RoleFacadeService : IRoleFacadeService
	{
		#region Internal Fields

		internal IMapper Mapper;
		internal IRoleService RoleService;

		#endregion Internal Fields

		#region Ctor

		public RoleFacadeService(IMapper mapper, IRoleService roleService)
		{
			Mapper = mapper;
			RoleService = roleService;
		}

		#endregion Ctor

		#region IRoleFacadeService Members

		#region Role Members

		public IEnumerable<RoleDTO> GetRoles()
		{
			IEnumerable<Role> roles = RoleService.GetRoles();

			return Mapper.Map<List<RoleDTO>>(roles);
		}

		public IEnumerable<RoleDTO> GetRolesByClient(int clientId)
		{
			IEnumerable<Role> roles = RoleService.GetRolesByClient(clientId);

			return Mapper.Map<List<RoleDTO>>(roles);
		}

		public RoleDTO FindRoleById(int id)
		{
			Role roles = RoleService.FindRoleById(id);

			return Mapper.Map<RoleDTO>(roles);
		}

		public RoleDTO FindRoleByName(string name)
		{
			Role roles = RoleService.FindRoleByName(name);

			return Mapper.Map<RoleDTO>(roles);
		}

		public void CreateRole(RoleDTO role)
		{
			var model = Mapper.Map<Role>(role);

			RoleService.CreateRole(model);
		}

		public void CreateRole(int clientId, RoleDTO role)
		{
			var model = Mapper.Map<Role>(role);

			RoleService.CreateRole(clientId, model);
		}

		public void UpdateRole(RoleDTO role)
		{
			var model = Mapper.Map<Role>(role);

			RoleService.UpdateRole(model);
		}

		public void DeleteRole(RoleDTO role)
		{
			var model = Mapper.Map<Role>(role);

			RoleService.DeleteRole(model);
		}

		#endregion Role Members

		#region Permissions Members

		public IEnumerable<PermissionDTO> GetRolePermissions(int roleId)
		{
			IEnumerable<Permission> rolePermissions = RoleService.GetRolePermissions(roleId);

			return Mapper.Map<List<PermissionDTO>>(rolePermissions);
		}

		public void AddRolePermissions(int roleId, params PermissionDTO[] permissions)
		{
			List<Permission> Permissions = new List<Permission>();

			foreach (PermissionDTO item in permissions)
			{
				Permissions.Add(Mapper.Map<Permission>(item));
			}

			if (Permissions.Count > 0)
			{
				RoleService.AddRolePermissions(roleId, Permissions.ToArray());
			}
		}

		public void RemoveRolePermissions(int roleId, params PermissionDTO[] permissions)
		{
			List<Permission> Permissions = new List<Permission>();

			foreach (PermissionDTO item in permissions)
			{
				Permissions.Add(Mapper.Map<Permission>(item));
			}

			if (Permissions.Count > 0)
			{
				RoleService.RemoveRolePermissions(roleId, Permissions.ToArray());
			}
		}

		#endregion Permissions Members

		#endregion IRoleFacadeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (Mapper != null)
			{
				Mapper = null;
			}

			if (RoleService != null)
			{
				RoleService.Dispose();
				RoleService = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}