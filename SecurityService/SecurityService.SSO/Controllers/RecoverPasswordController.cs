
using manage = Management.Infrastructure.Service.Contracts;
using SecurityService.SSO.Services;
using System;
using System.Text;
using System.Web.Mvc;
using SecurityService.SSO.Models;
using SecurityService.SSO.Infrastructure.Configuration;

namespace SecurityService.SSO.Controllers
{
    public class RecoverPasswordController : Controller
    { 
        #region Fields
        private SmsService _smsService;
        private readonly UserService _userService = new UserService();
        #endregion

        #region Ctor

        public RecoverPasswordController()
        {
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

    }
}