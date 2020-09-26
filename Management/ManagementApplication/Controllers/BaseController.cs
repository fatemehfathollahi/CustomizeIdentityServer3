using Framework.Core.Web.Security;
using System.Web.Mvc;

namespace ManagementApplication.Controllers
{
	[AuthorizePermissionControllerWithRoleName("ADMAdmin")]
	public class BaseController : Controller
	{
	}
}