using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class CommonController : BaseController
	{
		// GET: Common
		public PartialViewResult ConfirmDialogComponent()
		{
			return PartialView();
		}
	}
}