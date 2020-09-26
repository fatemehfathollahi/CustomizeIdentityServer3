using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IRoleRepository : IRepositoryAsync<int, Role>
	{
	}

	public class RoleRepository : BaseRepositoryAsync<int, Role>, IRoleRepository
	{
		public RoleRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}