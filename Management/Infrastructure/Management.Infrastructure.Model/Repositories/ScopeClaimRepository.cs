using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IScopeClaimRepository : IRepositoryAsync<int, ScopeClaim>
	{
	}

	public class ScopeClaimRepository : BaseRepositoryAsync<int, ScopeClaim>, IScopeClaimRepository
	{
		public ScopeClaimRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}