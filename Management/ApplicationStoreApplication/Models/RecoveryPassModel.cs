using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApplicationStoreApplication.Models
{
    public class RecoveryPassModel
    {
        [Required(ErrorMessage = "Mobile Number is Required")]
        public string Mobile { get; set; }
    }
}