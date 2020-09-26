using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IScopeRepository : IRepositoryAsync<int, Scope>
	{
	}

	public class ScopeRepository : BaseRepositoryAsync<int, Scope>, IScopeRepository
	{
		public ScopeRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}