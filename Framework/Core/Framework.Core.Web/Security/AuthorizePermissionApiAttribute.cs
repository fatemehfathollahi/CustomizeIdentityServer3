using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Framework.Core.Web.Security
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizePermissionApiAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			ClaimsPrincipal currentUser = (ClaimsPrincipal)actionContext.RequestContext.Principal;

			if (currentUser.Identity.IsAuthenticated)
			{
				if (!currentUser.HasClaim("role", GetRoleName(actionContext)))
				{
					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "شما دسترسی ندارید.");
				}
			}
			else
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "شما دسترسی ندارید.");
			}
		}

		public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			ClaimsPrincipal currentUser = actionContext.RequestContext.Principal as ClaimsPrincipal;

			if (currentUser.Identity.IsAuthenticated)
			{
				if (!currentUser.HasClaim("role", GetRoleName(actionContext)))
				{
					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "شما دسترسی ندارید.");
				}
			}
			else
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "شما دسترسی ندارید.");
			}

			return Task.FromResult<object>(null);
		}

		private string GetRoleName(HttpActionContext actionContext)
		{
			string controllerName = actionContext.ControllerContext.Controller.GetType().Name;
			string actionName = actionContext.ActionDescriptor.ActionName;

			return controllerName + "." + actionName;
		}
	}
}