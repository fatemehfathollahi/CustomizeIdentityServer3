using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class PermisionController : Controller
	{
		// GET: Permision
		public PartialViewResult IndexComponent()
		{
			return PartialView();
		}

		public PartialViewResult PermisionComponent()
		{
			return PartialView();
		}

		public PartialViewResult PermisionListComponent()
		{
			return PartialView();
		}
	}
}