using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IUserLoginRepository : IRepositoryAsync<int, UserLogin>
	{
	}

	public class UserLoginRepository : BaseRepositoryAsync<int, UserLogin>, IUserLoginRepository
	{
		public UserLoginRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}