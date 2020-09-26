using Framework.Core.Web.Security;
using System.Web.Http;

namespace ManagementApplication.Api
{
	[AuthorizePermissionApiWithRoleName("ADMAdmin")]
	public class BaseApiController : ApiController
	{
	}
}