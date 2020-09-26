using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class UserController : Controller
	{
		// GET: User
		public PartialViewResult IndexComponent()
		{
			return PartialView();
		}

		public PartialViewResult UserComponent()
		{
			return PartialView();
		}
	}
}