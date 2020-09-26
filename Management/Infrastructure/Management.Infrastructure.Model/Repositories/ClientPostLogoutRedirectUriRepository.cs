using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientPostLogoutRedirectUriRepository : IRepositoryAsync<int, ClientPostLogoutRedirectUri>
	{
	}

	public class ClientPostLogoutRedirectUriRepository : BaseRepositoryAsync<int, ClientPostLogoutRedirectUri>, IClientPostLogoutRedirectUriRepository
	{
		public ClientPostLogoutRedirectUriRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}