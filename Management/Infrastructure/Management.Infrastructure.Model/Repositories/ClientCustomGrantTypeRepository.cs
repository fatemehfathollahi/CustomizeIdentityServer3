using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IClientCustomGrantTypeRepository : IRepositoryAsync<int, ClientCustomGrantType>
	{
	}

	public class ClientCustomGrantTypeRepository : BaseRepositoryAsync<int, ClientCustomGrantType>, IClientCustomGrantTypeRepository
	{
		public ClientCustomGrantTypeRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}