using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IUserRepository : IRepositoryAsync<int, User>
	{
	}

	public class UserRepository : BaseRepositoryAsync<int, User>, IUserRepository
	{
		public UserRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}