using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IUserClientPermissionRepository : IRepositoryAsync<int, UserClientPermission>
	{
	}

	public class UserClientPermissionRepository : BaseRepositoryAsync<int, UserClientPermission>, IUserClientPermissionRepository
	{
		public UserClientPermissionRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}