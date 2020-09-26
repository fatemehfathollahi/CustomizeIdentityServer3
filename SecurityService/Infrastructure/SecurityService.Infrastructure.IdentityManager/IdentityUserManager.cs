using Microsoft.AspNet.Identity;
using SecurityService.Infrastructure.Models;

namespace SecurityService.Infrastructure.IdentityManager
{
	public class IdentityUserManager : UserManager<ApplicationUser, int>
	{
		public IdentityUserManager(IdentityUserStore userStore)
			: base(userStore)
		{
		}
	}
}