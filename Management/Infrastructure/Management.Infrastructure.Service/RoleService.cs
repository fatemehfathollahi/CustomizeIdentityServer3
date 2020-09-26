using Framework.Core.Common.Attributes;
using Management.Infrastructure.Models;
using Management.Infrastructure.Models.Repositories;
using Management.Infrastructure.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Management.Infrastructure.Service
{
	public class RoleService : IRoleService
	{
		#region Fields

		private IRoleRepository _roleRepository;
		private IPermissionRepository _permissionRepository;
		private IClientRepository _clientRepository;

		#endregion Fields

		#region Ctor

		public RoleService(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IClientRepository clientRepository)
		{
			_roleRepository = roleRepository;
			_permissionRepository = permissionRepository;
			_clientRepository = clientRepository;
		}

		#endregion Ctor

		#region IRoleService Members

		[NonTransactional]
		public IEnumerable<Role> GetRoles()
		{
			return _roleRepository.GetAllAsQuery()
				.ToList();
		}

		[NonTransactional]
		public IEnumerable<Role> GetRolesByClient(int clientId)
		{
			return _roleRepository.GetAllAsQuery()
				.Where(o => o.Clients.Any(c => c.Id == clientId)).ToList();
		}

		[NonTransactional]
		public Role FindRoleById(int id)
		{
			return _roleRepository.Get(id);
		}

		[NonTransactional]
		public Role FindRoleByName(string name)
		{
			List<Role> role = _roleRepository.Get(o => o.Name == name);

			return role.FirstOrDefault();
		}

		public void CreateRole(Role role)
		{
			_roleRepository.Insert(role);
		}

		public void CreateRole(int clientId, Role role)
		{
			Client cRow = _clientRepository.Get(o => o.Id == clientId).FirstOrDefault();
			role.Clients.Add(cRow);
			_roleRepository.Insert(role);
		}

		public void DeleteRole(Role role)
		{
			Role currentRole = _roleRepository.Get(role.Id);
			if (currentRole != null)
			{
				List<int> tempDeleteListId = new List<int>();
				foreach (Permission item in currentRole.Permissions)
				{
					tempDeleteListId.Add(item.Id);
				}
				currentRole.Permissions.Clear();
				foreach (int item in tempDeleteListId)
				{
					//var tRow = currentRole.Permissions.Where(o => o.Id == item).FirstOrDefault();
					//if (tRow != null)
					//	currentRole.Permissions.Remove(tRow);

					_permissionRepository.Delete(item);
				}
				currentRole.Clients.Clear();
				_roleRepository.Delete(currentRole);
			}
		}

		public void UpdateRole(Role role)
		{
			Role currentRole = _roleRepository.Get(role.Id);

			if (currentRole != null)
			{
				currentRole.Name = role.Name;
				currentRole.Description = role.Description;
				_roleRepository.Update(currentRole);

				List<int> tempDeleteListId = new List<int>();
				foreach (Permission item in currentRole.Permissions)
				{
					Permission tRow = role.Permissions.Where(o => o.Id == item.Id && item.Roles.Any(r => r.Id == currentRole.Id)).FirstOrDefault();
					if (tRow == null)
					{
						tempDeleteListId.Add(item.Id);
					}
				}
				foreach (Permission item in role.Permissions)
				{
					if (item.Id < 0)
					{
						_permissionRepository.Insert(new Permission
						{
							Name = item.Name,
							Roles = new List<Role> { currentRole }
						});
					}
				}
				foreach (int item in tempDeleteListId)
				{
					Permission tRow = currentRole.Permissions.Where(o => o.Id == item).FirstOrDefault();
					if (tRow != null)
					{
						currentRole.Permissions.Remove(tRow);
					}

					_permissionRepository.Delete(item);
				}
			}
		}

		[NonTransactional]
		public IEnumerable<Permission> GetRolePermissions(int roleId)
		{
			return _permissionRepository.Get(o => o.Roles.Any(r => r.Id == roleId));
		}

		public void AddRolePermissions(int roleId, params Permission[] permissions)
		{
			Role role = _roleRepository.Get(roleId);

			if (role != null)
			{
				foreach (Permission item in permissions)
				{
					item.Roles.Add(role);

					_permissionRepository.Insert(item);
				}
			}
		}

		public void RemoveRolePermissions(int roleId, params Permission[] permissions)
		{
			Role role = _roleRepository.Get(roleId);

			if (role != null)
			{
				foreach (Permission item in permissions)
				{
					item.Roles.Remove(role);

					_permissionRepository.Insert(item);
				}
			}
		}

		#endregion IRoleService Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_roleRepository != null)
			{
				_roleRepository.Dispose();
				_roleRepository = null;
			}

			if (_permissionRepository != null)
			{
				_permissionRepository.Dispose();
				_permissionRepository = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}