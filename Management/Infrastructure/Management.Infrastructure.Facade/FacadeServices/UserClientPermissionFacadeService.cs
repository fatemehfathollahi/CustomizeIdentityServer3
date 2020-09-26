using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeServices.Contracts;
using Management.Infrastructure.Models;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Management.Infrastructure.Facade.FacadeServices
{
	public class UserClientPermissionFacadeService : IUserClientPermissionFacadeService
	{
		#region Internal Fields

		internal IMapper Mapper;
		internal IUserClientPermissionService userClientPermissionService;

		#endregion Internal Fields

		#region Constructor

		public UserClientPermissionFacadeService(IMapper mapper, IUserClientPermissionService userClientPermissionService)
		{
			Mapper = mapper;
			this.userClientPermissionService = userClientPermissionService;
		}

		#endregion Constructor

		#region IClientFacadeService Members

		public IEnumerable<UserClientPermissionDTO> Get()
		{
			IEnumerable<UserClientPermission> tempList = userClientPermissionService.Get();
			var list = Mapper.Map<IEnumerable<UserClientPermissionDTO>>(tempList);
			return list;
		}

		public IEnumerable<UserClientPermissionDTO> Get(UserClientPermissionDTO dto)
		{
			var temp = Mapper.Map<UserClientPermission>(dto);
			var tempList = userClientPermissionService.Get(temp);
			var list = Mapper.Map<IEnumerable<UserClientPermissionDTO>>(tempList);
			return list;
		}

		public UserClientPermissionDTO Get(int id)
		{
			UserClientPermission tempList = userClientPermissionService.Get(id);
			var list = Mapper.Map<UserClientPermissionDTO>(tempList);
			return list;
		}

		public void Add(UserClientPermissionDTO client)
		{
			var temp = Mapper.Map<UserClientPermission>(client);
			userClientPermissionService.Add(temp);
		}

		public void Update(UserClientPermissionDTO client)
		{
			var temp = Mapper.Map<UserClientPermission>(client);
			userClientPermissionService.Update(temp);
		}

		public void Delete(UserClientPermissionDTO client)
		{
			var temp = Mapper.Map<UserClientPermission>(client);
			userClientPermissionService.Delete(temp);
		}

		#endregion IClientFacadeService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (Mapper != null)
			{
				Mapper = null;
			}

			if (userClientPermissionService != null)
			{
				userClientPermissionService.Dispose();
				userClientPermissionService = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}