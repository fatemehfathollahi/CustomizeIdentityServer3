using Microsoft.AspNet.Identity.EntityFramework;
using SecurityService.Infrastructure.Data;
using SecurityService.Infrastructure.Models;

namespace SecurityService.Infrastructure.IdentityManager
{
	public class IdentityUserStore : UserStore<ApplicationUser, ApplicationPermission, int, ApplicationUserLogin, ApplicationUserPermission, ApplicationUserClaim>
	{
		public IdentityUserStore(IdentityContext context)
			: base(context)
		{
		}
	}
}