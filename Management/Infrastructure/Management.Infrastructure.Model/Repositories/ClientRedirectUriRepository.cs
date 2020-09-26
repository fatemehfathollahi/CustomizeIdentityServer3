using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientRedirectUriRepository : IRepositoryAsync<int, ClientRedirectUri>
	{
	}

	public class ClientRedirectUriRepository : BaseRepositoryAsync<int, ClientRedirectUri>, IClientRedirectUriRepository
	{
		public ClientRedirectUriRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}