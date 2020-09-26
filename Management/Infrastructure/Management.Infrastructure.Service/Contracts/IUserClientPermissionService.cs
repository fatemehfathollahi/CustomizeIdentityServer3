using Framework.Core.Contracts.Services;
using Management.Infrastructure.Models;
using System.Collections.Generic;

namespace Management.Infrastructure.Service.Contracts
{
	public interface IUserClientPermissionService : IService
	{
		IEnumerable<UserClientPermission> Get();

		IEnumerable<UserClientPermission> Get(UserClientPermission model);

		UserClientPermission Get(int id);

		void Add(UserClientPermission client);

		void Update(UserClientPermission client);

		void Delete(UserClientPermission client);
	}
}