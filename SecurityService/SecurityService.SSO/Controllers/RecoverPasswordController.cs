
using manage = Management.Infrastructure.Service.Contracts;
using SecurityService.SSO.Services;
using System;
using System.Text;
using System.Web.Mvc;
<<<<<<< HEAD
using SecurityService.SSO.Models;
using SecurityService.SSO.Infrastructure.Configuration;
=======
using Fluentx.Mvc;
using IdentityServer3.Core.ViewModels;
using SecurityService.SSO.IdentityService;
using SecurityService.SSO.Infrastructure;
>>>>>>> 0d20e5961a45f9fbdc3d8f4865af2742226075e2

namespace SecurityService.SSO.Controllers
{
    public class RecoverPasswordController : Controller
<<<<<<< HEAD
    { 
        #region Fields
        private SmsService _smsService;
        private readonly UserService _userService = new UserService();
        #endregion

        #region Ctor
=======
    {
        public RecoverPasswordController()
        {
          
        }
>>>>>>> 0d20e5961a45f9fbdc3d8f4865af2742226075e2

        public RecoverPasswordController()
        {
<<<<<<< HEAD
            _smsService = new SmsService();
        }
        #endregion
      
        public ActionResult RecoverCode(string userName)
        {
            if(userName == "" || userName == null)
            {
                return Json(new { result = "نام کاربری را وارد نمایید" });
            }
            if (userName.ToLower() == "admin")
            {
                return Json(new { result = "امکان بازیابی پسورد برای این کاربر وجود ندارد" });
            }
            var _user = _userService.FindByName(userName);
            if (_user == null)
            {
                //ModelState.AddModelError("userName", "there is not userName");
                return Json(new { result = "کاربری با این نام وجود ندارد"});
            }
            var pass = CreatePassword();
            SmsService.ClsSend smsSingle = new SmsService.ClsSend();
            smsSingle.SendSMS_Single(new SmsModel { PhoneNumber = _user.PhoneNumber, Message = pass }, SmsSetting.GetConfig());
            _userService.UpdatePassword(_user, pass);
            return Json(new { result = "password recovery" });
        }
      
        public string CreatePassword()
        {
            int length = 8;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
=======
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

     
>>>>>>> 0d20e5961a45f9fbdc3d8f4865af2742226075e2

    }
}