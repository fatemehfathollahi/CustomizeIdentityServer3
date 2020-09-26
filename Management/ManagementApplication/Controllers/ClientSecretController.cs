using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class ClientSecretController : BaseController
	{
		// GET: ClientSecret
		public PartialViewResult ClientSecretComponent()
		{
			return PartialView();
		}
	}
}