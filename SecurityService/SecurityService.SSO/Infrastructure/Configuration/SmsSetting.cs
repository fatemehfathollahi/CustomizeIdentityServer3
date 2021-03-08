using SecurityService.SSO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityService.SSO.Infrastructure.Configuration
{
    /// <summary>
    /// config for sms service
    /// </summary>
    public class SmsSetting
    {
        public static SmsSettingModel GetConfig()
        {
            return new SmsSettingModel
            {
                Number = "100084288",
                UserName = "web_processingworld",
                Password = "fr76543709",
                Company = "PROCESSINGWORLD",
                Ip = "http://193.104.22.14:2055/CPSMSService/Access"
            };
        }
    }
}