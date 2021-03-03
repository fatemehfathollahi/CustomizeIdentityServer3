using ApplicationStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationStoreApplication.Utility
{
    public static class SmsSetting
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