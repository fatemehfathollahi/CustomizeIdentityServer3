using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using Framework.Persistences.EF.Model;

namespace Management.Infrastructure.Models.Repositories
{
	public interface IUserClaimRepository : IRepositoryAsync<int, UserClaim>
	{
	}

	public class UserClaimRepository : BaseRepositoryAsync<int, UserClaim>, IUserClaimRepository
	{
		public UserClaimRepository(DataContext dataContext) : base(dataContext)
		{
		}
	}
}