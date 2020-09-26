using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientCorsOriginRepository : IRepositoryAsync<int, ClientCorsOrigin>
	{
	}

	public class ClientCorsOriginRepository : BaseRepositoryAsync<int, ClientCorsOrigin>, IClientCorsOriginRepository
	{
		public ClientCorsOriginRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}