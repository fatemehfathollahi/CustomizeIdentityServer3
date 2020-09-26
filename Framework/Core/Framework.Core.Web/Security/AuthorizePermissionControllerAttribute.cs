using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace Framework.Core.Web.Security
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class AuthorizePermissionControllerAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			ClaimsPrincipal currentUser = (ClaimsPrincipal)filterContext.HttpContext.User;

			if (currentUser.Identity.IsAuthenticated)
			{
				if (!currentUser.HasClaim("role", GetRoleName(filterContext)))
				{
					filterContext.Result = new HttpUnauthorizedResult();
				}
			}
			else
			{
				HandleUnauthorizedRequest(filterContext);
			}
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				// 403 we know who you are, but you haven't been granted access
				filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
			}
			else
			{
				// 401 who are you? go login and then try again
				filterContext.Result = new HttpUnauthorizedResult();
			}
		}

		private string GetRoleName(AuthorizationContext actionContext)
		{
			string controllerName = actionContext.Controller.GetType().Name;
			string actionName = actionContext.ActionDescriptor.ActionName;

			return controllerName + "." + actionName;
		}
	}
}