using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.AspNet.Identity;
using SecurityService.Infrastructure.Models;
using SecurityService.SSO.IdentityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SecurityService.SSO.Infrastructure
{
    public class RecoveryPassService //: IdentityUserService
    {
        public static  string GetPhoneNumber(string userName)
        {

            //var _user =  IdentityUserService.userManager.FindByName(userName);
            // return _user.PhoneNumber;
            return null;
        }
      
        //public override Task<ApplicationUser> FindUserAsync(string username)
        //{
        //    return base.FindUserAsync(username);
        //}
    }
}