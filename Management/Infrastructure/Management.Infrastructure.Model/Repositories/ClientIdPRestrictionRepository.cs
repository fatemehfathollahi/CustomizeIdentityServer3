using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientIdPRestrictionRepository : IRepositoryAsync<int, ClientIdPRestriction>
	{
	}

	public class ClientIdPRestrictionRepository : BaseRepositoryAsync<int, ClientIdPRestriction>, IClientIdPRestrictionRepository
	{
		public ClientIdPRestrictionRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}