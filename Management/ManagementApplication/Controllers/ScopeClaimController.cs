using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ScopeClaimController : BaseController
	{
		// GET: ScopeClaim
		public PartialViewResult ScopeClaimComponent()
		{
			return PartialView();
		}
	}
}