using SecurityService.SSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityService.SSO.Controllers
{
    public class MvcCaptchaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string CaptchaUserInput)
        {
            var resultStr = string.Empty;
            var captchaStr = this.HttpContext.Session["Captcha"]; 
            CaptchaUserInput = CaptchaUserInput.ToLower();
            if (String.Equals(captchaStr, CaptchaUserInput))
            {
                resultStr = "Captcha is entered correctly!!";
            }
            else
            {
                resultStr = "Captcha re-enter captcha!";
            }

            return Json(new { Data = resultStr, captchaStr = captchaStr, CaptchaUserInput = CaptchaUserInput }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateCaptcha()
        {
            Captcha captcha = new Captcha(6, 200, 100, HttpContext);
            return File(captcha.create_captcha(), "image/jpeg");
        }
    }
}