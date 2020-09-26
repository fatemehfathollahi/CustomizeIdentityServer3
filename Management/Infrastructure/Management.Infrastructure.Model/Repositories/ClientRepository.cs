using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientRepository : IRepositoryAsync<int, Client>
	{
	}

	public class ClientRepository : BaseRepositoryAsync<int, Client>, IClientRepository
	{
		public ClientRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}