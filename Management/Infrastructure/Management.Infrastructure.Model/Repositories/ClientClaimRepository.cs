using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientClaimRepository : IRepositoryAsync<int, ClientClaim>
	{
	}

	public class ClientClaimRepository : BaseRepositoryAsync<int, ClientClaim>, IClientClaimRepository
	{
		public ClientClaimRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}