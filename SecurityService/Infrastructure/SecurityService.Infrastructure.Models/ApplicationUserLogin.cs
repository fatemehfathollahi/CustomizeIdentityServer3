using Microsoft.AspNet.Identity.EntityFramework;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationUserLogin : IdentityUserLogin<int>
	{
		public ApplicationUserLogin()
		{
		}
	}
}