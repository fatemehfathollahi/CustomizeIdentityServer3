using System.Web.Mvc;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.ViewModels;
using System.IO;
using System.Drawing;
using System;
using System.Text;
using SecurityService.SSO.Models;
using SecurityService.SSO.Services;
using IdentityServer3.Core.Services.Default;
using System.Threading.Tasks;
using SecurityService.SSO.IdentityService;
using SecurityService.SSO.Infrastructure;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;

namespace SecurityService.SSO.Controllers
{
    public class LogonWorkflowController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogonWorkflowController"/> class.
        /// </summary>
        public LogonWorkflowController()
        {
           
        }

        #region Login

        /// <summary>
        /// Loads the HTML for the login page.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="message">
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login(LoginViewModel model, SignInMessage message)
        {
            return this.View(model);
        }

        #endregion

        #region Logout

        /// <summary>
        /// Loads the HTML for the logout prompt page.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Logout(LogoutViewModel model)
        {
            return this.View(model);
        }

        #endregion

        #region LoggedOut

        /// <summary>
        /// Loads the HTML for the logged out page informing the user that they have successfully logged out.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult LoggedOut(LoggedOutViewModel model)
        {
            return this.View(model);
        }

        #endregion

        #region Consent

        /// <summary>
        /// Loads the HTML for the user consent page.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Consent(ConsentViewModel model)
        {
            return this.View(model);
        }

        #endregion

        #region Permissions

        /// <summary>
        /// Loads the HTML for the client permissions page.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Permissions(ClientPermissionsViewModel model)
        {
            return this.View(model);
        }

        #endregion

        #region Error

        /// <summary>
        /// Loads the HTML for the error page.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public virtual ActionResult Error(ErrorViewModel model)
        {
            return this.View(model);
        }

        #endregion

        #region RecoverPass


        public ActionResult ResetPassword(LoginViewModel model)
        {
            return this.View(model);
        }


        public  ActionResult RecoverPass(string Username)
        {
            //var userName = ((ClaimsIdentity)User.Identity).FindFirst("UserName");
            var  selectedTraining = Request["Username"];
            if (Username == null)
            {
                ModelState.AddModelError("UserName", "UserName is Require");
                return  Redirect("http://localhost:16161/");
            }
           var _name =  RecoveryPassService.GetPhoneNumber(Username);
            return View("Index", Username);
        }
        #endregion

       
    }
}