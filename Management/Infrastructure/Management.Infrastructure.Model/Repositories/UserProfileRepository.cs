using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IUserProfileRepository : IRepositoryAsync<int, UserProfile>
	{
	}

	public class UserProfileRepository : BaseRepositoryAsync<int, UserProfile>, IUserProfileRepository
	{
		public UserProfileRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}