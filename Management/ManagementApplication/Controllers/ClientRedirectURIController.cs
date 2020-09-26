using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ClientRedirectURIController : BaseController
	{
		// GET: ClientRedirectURI
		public PartialViewResult ClientRedirectURIComponent()
		{
			return PartialView();
		}
	}
}