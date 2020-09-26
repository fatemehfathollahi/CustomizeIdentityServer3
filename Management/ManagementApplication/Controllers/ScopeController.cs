using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ScopeController : BaseController
	{
		// GET: Client
		public PartialViewResult IndexComponent()
		{
			return PartialView();
		}

		public PartialViewResult ScopeComponent()
		{
			return PartialView();
		}
	}
}