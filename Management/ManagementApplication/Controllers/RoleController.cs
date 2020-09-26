using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class RoleController : BaseController
	{
		// GET: Role
		public PartialViewResult IndexComponent()
		{
			return PartialView();
		}

		public PartialViewResult RoleComponent()
		{
			return PartialView();
		}
	}
}