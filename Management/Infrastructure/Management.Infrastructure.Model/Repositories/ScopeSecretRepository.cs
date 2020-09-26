using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IScopeSecretRepository : IRepositoryAsync<int, ScopeSecret>
	{
	}

	public class ScopeSecretRepository : BaseRepositoryAsync<int, ScopeSecret>, IScopeSecretRepository
	{
		public ScopeSecretRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}