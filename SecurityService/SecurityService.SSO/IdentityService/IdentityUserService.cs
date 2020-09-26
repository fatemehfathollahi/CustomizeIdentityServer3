using IdentityServer3.Core.Models;
using SecurityService.Infrastructure.IdentityManager;
using SecurityService.Infrastructure.Models;
using SecurityService.SSO.Infrastructure;

namespace SecurityService.SSO.IdentityService
{
	public class IdentityUserService : AspNetIdentityUserService<ApplicationUser, int>
	{
		public IdentityUserService(IdentityUserManager userManager)
			: base(userManager)
		{
		}
	}
}