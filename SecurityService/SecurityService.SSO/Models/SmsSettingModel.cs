using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityService.SSO.Models
{
    public class SmsSettingModel
    {
        public string Number { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Ip { get; set; }
        public bool IsFlash { get; set; }
        public bool IsFullAct { get; set; }
    }
}