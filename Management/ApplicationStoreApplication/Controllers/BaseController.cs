using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ApplicationStoreApplication.Controllers
{
    public class BaseController : Controller
    {
        protected ClaimsPrincipal CurrentUser { get { return User as ClaimsPrincipal; } }
        protected string UserName { get { return CurrentUser.Claims.First(o => o.Type == "name").Value; } }
    }
}