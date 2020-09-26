using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	public class HomeController : BaseController
	{
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public PartialViewResult HomeComponent()
		{
			return PartialView();
		}

		public PartialViewResult Manager()
		{
			return PartialView();
		}

		public PartialViewResult headerComponent()
		{
			return PartialView();
		}

		public PartialViewResult brandingComponent()
		{
			return PartialView();
		}

		public PartialViewResult controlsComponent()
		{
			return PartialView();
		}

		public PartialViewResult menuItemsComponent()
		{
			return PartialView();
		}

		[AllowAnonymous]
		public ActionResult Signout()
		{
			Request.GetOwinContext().Authentication.SignOut();
			return Redirect("/");
		}

		[AllowAnonymous]
		public void SignoutCleanup(string sid)
		{
			ClaimsPrincipal cp = (ClaimsPrincipal)User;
			Claim sidClaim = cp.FindFirst("sid");
			if (sidClaim != null && sidClaim.Value == sid)
			{
				Request.GetOwinContext().Authentication.SignOut("Cookies");
			}
		}
	}
}