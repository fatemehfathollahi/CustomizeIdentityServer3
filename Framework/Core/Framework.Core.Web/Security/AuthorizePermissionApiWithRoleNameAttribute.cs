using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
	public class AuthorizePermissionApiWithRoleNameAttribute : AuthorizationFilterAttribute
	{
		public List<string> RoleNames { get; set; }

		public AuthorizePermissionApiWithRoleNameAttribute(params string[] roleNames)
		{
			RoleNames = roleNames.ToList();
			//WriteFile("AuthorizePermissionApiWithRoleNameAttribute Constructor");
			//WriteFile(RoleNames);
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			//WriteFile("OnAuthorization Method");
			ClaimsPrincipal currentUser = (ClaimsPrincipal)actionContext.RequestContext.Principal;
			//WriteFile("currentUser.Identity.IsAuthenticated :");
			//WriteFile("-- :" + currentUser.Identity.IsAuthenticated);
			if (currentUser.Identity.IsAuthenticated)
			{
				List<string> RoleClaims = currentUser.Claims.Where(o => o.Type.ToLower() == "role").Select(o => o.Value).ToList();
				//WriteFile("RoleClaims :");
				//WriteFile("-- :" + RoleClaims);

				bool hasMatch = RoleClaims.Any(x => RoleNames.Any(y => y == x));
				//WriteFile("hasMatch :");
				//WriteFile("-- :" + hasMatch);

				if (!hasMatch)
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
			//WriteFile("OnAuthorizationAsync Method");
			ClaimsPrincipal currentUser = actionContext.RequestContext.Principal as ClaimsPrincipal;
			//WriteFile("currentUser.Identity.IsAuthenticated :");
			//WriteFile("-- :" + currentUser.Identity.IsAuthenticated);

			if (currentUser.Identity.IsAuthenticated)
			{
				List<string> RoleClaims = currentUser.Claims.Where(o => o.Type.ToLower() == "role").Select(o => o.Value).ToList();
				//WriteFile("RoleClaims :");
				//WriteFile("-- :" + RoleClaims);

				bool hasMatch = RoleClaims.Any(x => RoleNames.Any(y => y == x));
				//WriteFile("hasMatch :");
				//WriteFile("-- :" + hasMatch);

				if (!hasMatch)
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

		public void WriteFile(string text)
		{
			using (StreamWriter outputFile = new StreamWriter(@"c:\WriteLines.txt", true))
			{
				outputFile.WriteLine(text);
				outputFile.Flush();
			}
		}

		public void WriteFile(List<string> text)
		{
			using (StreamWriter outputFile = new StreamWriter(@"c:\WriteLines.txt", true))
			{
				outputFile.WriteLine("List");
				foreach (string item in text)
				{
					outputFile.WriteLine(item);
				}
				outputFile.Flush();
			}
		}
	}
}