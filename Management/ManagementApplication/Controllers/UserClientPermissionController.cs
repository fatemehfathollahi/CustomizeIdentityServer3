using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class UserClientPermissionController : Controller
	{
		// GET: UserClientPermission
		public PartialViewResult UserClientPermissionListComponent()
		{
			return PartialView();
		}
	}
}