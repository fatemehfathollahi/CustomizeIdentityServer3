using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ScopeSecretController : BaseController
	{
		// GET: ScopeSecret
		public PartialViewResult ScopeSecretComponent()
		{
			return PartialView();
		}
	}
}