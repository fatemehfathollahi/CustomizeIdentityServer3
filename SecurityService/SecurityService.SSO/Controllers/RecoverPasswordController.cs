using SecurityService.SSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Fluentx.Mvc;
using IdentityServer3.Core.ViewModels;
using SecurityService.SSO.IdentityService;
using SecurityService.SSO.Infrastructure;

namespace SecurityService.SSO.Controllers
{
    public class RecoverPasswordController : Controller
    {
        public RecoverPasswordController()
        {
          
        }

        public ActionResult Index()
        {
            // return  Redirect("http://localhost:16161/RecoveryPassword/RecoverPassword");
              return View();
        }

        //[HttpPost]
        public async Task<ActionResult> FoundUser(RecoverPassModel model)
        {
            model.Mobile = model.Mobile;
          
            //var exists = await _studentService.GetForgetPassword(model.NationalCode, model.Username);

            //if (exists.Email.IsNullOrEmpty() && exists.Mobile.IsNullOrEmpty())
            //{
            //    model.Message = "اطلاعات بازیابی رمز عبور در سیستم ثبت نشده است. لطفا به مدیر سیستم مراجعه کنید.";
            //    model.MessageColor = "callout-danger";
            //}
            //else
            //{
            //    if (!exists.Mobile.IsNullOrEmpty())
            //    {
            //        model.Mobile = exists.Mobile;
            //        model.IsActiveSms = true;
            //    }
            //    model.Email = exists.Email;
            //    model.Step = 1;
            //}

            return View("Index", model);
        }

      
        public async Task<ActionResult> RecoverCode(string userName)
        {
            if(userName == null)
            {
                ModelState.AddModelError("UserName", "UserName is Require");
            }
           
            return View("Index", userName);
        }

      //  [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> RecoverCode(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Dictionary<string, object> postData = new Dictionary<string, object>();
        //        postData.Add("UserName", model.Username);
        //        return this.RedirectAndPost("http://localhost:16161/UserProfile/RecoveryPassword", postData);
        //        // return JavaScript(script: "alert('اطلاعات مورد نیاز را وارد کنید');");
        //        // return RedirectToAction("Index");
        //    }


        //    //Dictionary<string, object> postData = new Dictionary<string, object>();
        //    //postData.Add("mobile", model.Mobile);
        //    //return this.RedirectAndPost("http://localhost:16161/UserProfile/RecoveryPassword", postData);

        //    //  var smsService = new SmsService();
        //    //  var response = smsService.SendSms(input);

        //    //  return JavaScript(script: "alert('" + response.message + "');");
        //    //model.FullEmail = model.FullEmail;
        //    //model.FullMobile = model.FullMobile;
        //    //model.Username = model.Username;
        //    //var result = "";
        //    //if (model.SendEmail && model.FullEmail.Length < 5)
        //    //{
        //    //    result = "ایمیل خود را به طور کامل وارد نمایید.";
        //    //}
        //    //else if (model.SendSms && model.FullMobile.Length != 11)
        //    //{
        //    //    result = "شماره همراه خود را به طور کامل وارد نمایید.";
        //    //}
        //    //else
        //    //{
        //    //   // result = await _service.GetSendEmailOrSms(model.SendEmail ? model.FullEmail : "", model.SendSms ? model.FullMobile : "", model.NationalCode, model.Username, Request.UserHostAddress);
        //    //}
        //    //model.Message = result;
        //    //model.MessageColor = "callout-danger";

        //    //if (result != "")
        //    //{
        //    //    model.FullEmail = "";
        //    //    model.FullMobile = "";
        //    //    model.Step = 1;
        //    //    return View("Index", model);
        //    //}

        //    //model.Message = "کد بازیابی رمز عبور برای شما ارسال شد.";
        //    //model.MessageColor = "callout-success";
        //    //model.Step = 2;
        //    return View("Index", model);
        //}

        //[HttpPost]
        public async Task<ActionResult> Verification(RecoverPassModel model)
        {
            //model.Username = model.Username;
            //model.RecoverCode = model.RecoverCode;
            ////var result = await _service.VerifyRecoveCode(model.Username, model.RecoverCode);
            //model.Message = result;
            //model.MessageColor = "callout-danger";

            //if (result != "")
            //{
            //    model.Step = 2;
            //    return View("Index", model);
            //}

            //model.Message = "کد بازیابی رمز عبور پذیرفته شد.";
            //model.MessageColor = "callout-success";
            //model.Step = 3;
            return View("Index", model);
        }

       // [HttpPost]
        public async Task<ActionResult> SetNewPassword(RecoverPassModel model)
        {
            //model.Username = model.Username;
            //model.Password = model.Password;
            //model.RePassword = model.RePassword;
            //var result = await _service.SetNewPassword(model.Username, model.Password, model.RePassword);
            //model.Message = result;
            //model.MessageColor = "callout-danger";

            //if (result != "")
            //{
            //    model.Step = 3;
            //    return View("Index", model);
            //}

            //model.Message = "کلمه عبور جدید با موفقیت ثبت شد.";
            //model.MessageColor = "callout-success";
            //model.Step = 4;
            return View("Index", model);
        }

     

    }
}