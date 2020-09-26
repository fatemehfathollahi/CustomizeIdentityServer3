using ApplicationStoreApplication.Models;
using Framework.Core.Contracts.Security;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationStoreApplication.Controllers
{
    public class UserProfileController : BaseController
    {
        #region Fields

        private IUserFacadeService _userFacadeService;
		#endregion

		#region Ctor

		public UserProfileController(IUserFacadeService userFacadeService)
        {
            _userFacadeService = userFacadeService;
		}

        #endregion
        // GET: UserProfile
        public ActionResult Index()
        {
            ViewBag.UserName = this.UserName;
            var user = _userFacadeService.FindUserByName(this.UserName);
            return View(user);
        }
		public ActionResult ChangePassword()
		{
			ViewBag.UserName = this.UserName;
			var user = _userFacadeService.FindUserByName(this.UserName);
			//user.FirstName = "ali";
			PasswordModel passwordDto = new Models.PasswordModel { FirstlastName = user.FirstName + ' ' + user.LastName,Message= "تغییر رمز عبور" };
			return View(passwordDto);

		}
		[HttpPost]
		public ActionResult ChangePassword(PasswordModel dto)
		{
			ViewBag.UserName = this.UserName;
			var user = _userFacadeService.FindUserByName(this.UserName);
			PasswordModel passwordDto = new Models.PasswordModel { FirstlastName = user.FirstName + ' ' + user.LastName};
			if(!dto.ConfirmNewPassword.Equals(dto.NewPassword))
			{
				passwordDto.Message = "تکرار رمز عبور اشتباه است";
				return View(passwordDto);
			}
			if (!_userFacadeService.IsSamePassword(user.Id,dto.OldPassword))
			{
				passwordDto.Message = "کلمه عبور اشتباه است";
				return View(passwordDto);
			}
			_userFacadeService.UpdatePassword(user.Id, dto.NewPassword);
			passwordDto.Message = "کلمه عبور با موفقیت به روز رسانی شد";
			return View(passwordDto);

		}
	}
}