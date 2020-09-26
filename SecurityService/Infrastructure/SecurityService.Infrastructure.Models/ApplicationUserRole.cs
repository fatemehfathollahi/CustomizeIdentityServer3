using Microsoft.AspNet.Identity.EntityFramework;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationUserPermission : IdentityUserRole<int>
	{
		public ApplicationUserPermission()
		{
		}
	}
}