using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ClientScopsController : BaseController
	{
		// GET: ClientScops
		public PartialViewResult ClientScopsComponent()
		{
			return PartialView();
		}
	}
}