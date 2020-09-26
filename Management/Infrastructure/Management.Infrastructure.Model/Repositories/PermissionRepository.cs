using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IPermissionRepository : IRepositoryAsync<int, Permission>
	{
	}

	public class PermissionRepository : BaseRepositoryAsync<int, Permission>, IPermissionRepository
	{
		public PermissionRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}