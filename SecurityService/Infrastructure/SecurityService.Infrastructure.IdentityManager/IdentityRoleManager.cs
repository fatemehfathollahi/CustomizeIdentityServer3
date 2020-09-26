using Microsoft.AspNet.Identity;
using SecurityService.Infrastructure.Models;

namespace SecurityService.Infrastructure.IdentityManager
{
	public class IdentityRoleManager : RoleManager<ApplicationPermission, int>
	{
		public IdentityRoleManager(IdentityRoleStore roleStore)
			: base(roleStore)
		{
		}
	}
}