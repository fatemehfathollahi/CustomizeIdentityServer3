using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class PostLogoutRedirectURIController : BaseController
	{
		// GET: PostLogoutRedirectURI
		public PartialViewResult PostLogoutRedirectURIComponent()
		{
			return PartialView();
		}
	}
}