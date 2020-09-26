using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientScopeRepository : IRepositoryAsync<int, ClientScope>
	{
	}

	public class ClientScopeRepository : BaseRepositoryAsync<int, ClientScope>, IClientScopeRepository
	{
		public ClientScopeRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}