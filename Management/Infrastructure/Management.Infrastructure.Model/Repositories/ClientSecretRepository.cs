using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientSecretRepository : IRepositoryAsync<int, ClientSecret>
	{
	}

	public class ClientSecretRepository : BaseRepositoryAsync<int, ClientSecret>, IClientSecretRepository
	{
		public ClientSecretRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}