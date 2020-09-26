using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ClientController : BaseController
	{
		// GET: Client
		public PartialViewResult IndexComponent()
		{
			return PartialView();
		}

		public PartialViewResult ClientComponent()
		{
			return PartialView();
		}
	}
}