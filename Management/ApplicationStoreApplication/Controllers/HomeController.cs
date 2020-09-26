using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ApplicationStoreApplication.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields

        private IClientFacadeService _clientFacadeService;

        #endregion

        #region Ctor

        public HomeController(IClientFacadeService clientFacadeService)
        {
            _clientFacadeService = clientFacadeService;
        }

        #endregion

        // GET: Home
        public ActionResult Index()
        {
            //List<ClientDTO> temp = _clientFacadeService.GetAllClientsByUserName("Admin").ToList();
            List<ClientDTO> temp = _clientFacadeService.GetAllClientsByUserName(UserName).ToList();
            ViewBag.UserName = this.UserName;
            return View(temp);
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
            var cp = (ClaimsPrincipal)User;
            var sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut("Cookies");
            }
        }
    }
}