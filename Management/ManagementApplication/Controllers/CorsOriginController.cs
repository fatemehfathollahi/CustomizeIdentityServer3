using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class CorsOriginController : BaseController
	{
		// GET: CorsOrigin
		public PartialViewResult CorsOriginComponent()
		{
			return PartialView();
		}
	}
}