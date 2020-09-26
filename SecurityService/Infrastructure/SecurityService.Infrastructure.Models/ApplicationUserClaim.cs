using Microsoft.AspNet.Identity.EntityFramework;

namespace SecurityService.Infrastructure.Models
{
	public class ApplicationUserClaim : IdentityUserClaim<int>
	{
		public ApplicationUserClaim()
		{
		}
	}
}