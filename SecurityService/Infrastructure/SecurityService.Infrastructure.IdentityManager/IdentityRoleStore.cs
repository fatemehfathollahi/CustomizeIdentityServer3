using Microsoft.AspNet.Identity.EntityFramework;
using SecurityService.Infrastructure.Data;
using SecurityService.Infrastructure.Models;

namespace SecurityService.Infrastructure.IdentityManager
{
	public class IdentityRoleStore : RoleStore<ApplicationPermission, int, ApplicationUserPermission>
	{
		public IdentityRoleStore(IdentityContext context)
			: base(context)
		{
		}
	}
}