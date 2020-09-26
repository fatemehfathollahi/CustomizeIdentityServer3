using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace Framework.Core.Web.Security
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class AuthorizePermissionControllerWithRoleNameAttribute : AuthorizeAttribute
	{
		public List<string> RoleNames { get; set; }

		public AuthorizePermissionControllerWithRoleNameAttribute(params string[] roleNames)
		{
			RoleNames = roleNames.ToList();
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			ClaimsPrincipal currentUser = (ClaimsPrincipal)filterContext.HttpContext.User;

			if (currentUser.Identity.IsAuthenticated)
			{
				List<string> RoleClaims = currentUser.Claims.Where(o => o.Type.ToLower() == "role").Select(o => o.Value).ToList();

				bool hasMatch = RoleClaims.Any(x => RoleNames.Any(y => y == x));

				if (!hasMatch)
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
	}
}