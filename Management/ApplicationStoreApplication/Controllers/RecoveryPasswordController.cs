using ApplicationStoreApplication.Models;
using ApplicationStoreApplication.Utility;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ApplicationStoreApplication.Controllers
{
    
    public class RecoveryPasswordController : Controller
    {
        #region Fields

        private IUserFacadeService _userFacadeService;
        private SmsService _smsService;
        #endregion

        #region Ctor

        public RecoveryPasswordController(IUserFacadeService userFacadeService)
        {
            _userFacadeService = userFacadeService;
            _smsService = new SmsService();
        }

        #endregion
        [AllowAnonymous]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RecoverPassword(RecoveryPassModel model)
        {
            var _users = _userFacadeService.GetUsers();
            var _hasUser = _users.Where(u => u.PhoneNumber == model.Mobile).FirstOrDefault();
            if(_hasUser == null)
            {
                ModelState.AddModelError("Mobile", "there is not mobile number");
                return View(model);
            }
           
            var pass = CreatePassword();
            SmsService.ClsSend smsSingle = new SmsService.ClsSend();
            smsSingle.SendSMS_Single(new SmsModel { PhoneNumber = model.Mobile, Message = pass }, SmsSetting.GetConfig());
           _userFacadeService.UpdatePassword(_hasUser.UserProfile.Id, pass);
            return RedirectToAction("Success");

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