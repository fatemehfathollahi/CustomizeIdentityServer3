using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using Management.Infrastructure.Models;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Management.Infrastructure.Facade.FacadeServices
{
	public class PermissionFacadeService : IPermissionFacadeService
	{
		#region Internal Fields

		internal IMapper Mapper;
		internal IPermissionService PermissionService;

		#endregion Internal Fields

		#region Ctor

		public PermissionFacadeService(IMapper mapper, IPermissionService permissionService)
		{
			Mapper = mapper;
			PermissionService = permissionService;
		}

		#endregion Ctor

		#region IPermissionFacadeService Members

		public IEnumerable<PermissionListDTO> GetPermissions()
		{
			IEnumerable<Permission> permissions = PermissionService.GetPermissions();

			return Mapper.Map<List<PermissionListDTO>>(permissions);
		}

		public PermissionDTO FindPermissionByID(int id)
		{
			Permission permissions = PermissionService.FindPermissionByID(id);

			return Mapper.Map<PermissionDTO>(permissions);
		}

		public PermissionDTO FindPermissionByName(string name)
		{
			Permission permissions = PermissionService.FindPermissionByName(name);

			return Mapper.Map<PermissionDTO>(permissions);
		}

		public bool ExistsPermissionName(int id, string Name)
		{
			return PermissionService.ExistsPermissionName(id, Name);
		}

		public void CreatePermission(PermissionDTO permission)
		{
			var model = Mapper.Map<Permission>(permission);

			PermissionService.CreatePermission(model);
		}

		public void UpdatePermission(PermissionDTO permission)
		{
			var model = Mapper.Map<Permission>(permission);

			PermissionService.UpdatePermission(model);
		}

		public void DeletePermission(PermissionDTO permission)
		{
			var model = Mapper.Map<Permission>(permission);

			PermissionService.DeletePermission(model);
		}

		#endregion IPermissionFacadeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (Mapper != null)
			{
				Mapper = null;
			}

			if (PermissionService != null)
			{
				PermissionService.Dispose();
				PermissionService = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}