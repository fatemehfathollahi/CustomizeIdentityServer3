﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Facade.DTOModel
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }    
        public string Password { get; set; }       
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}