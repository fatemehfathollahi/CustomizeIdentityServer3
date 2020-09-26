using Framework.Core.Contracts.Services;
using Management.Infrastructure.Facade.DTOModel;
using System.Collections.Generic;

namespace Management.Infrastructure.Facade.FacadeServices.Contracts
{
	public interface IUserClientPermissionFacadeService : IFacadeService
	{
		IEnumerable<UserClientPermissionDTO> Get();

		IEnumerable<UserClientPermissionDTO> Get(UserClientPermissionDTO dto);

		UserClientPermissionDTO Get(int id);

		void Add(UserClientPermissionDTO client);

		void Update(UserClientPermissionDTO client);

		void Delete(UserClientPermissionDTO client);
	}
}