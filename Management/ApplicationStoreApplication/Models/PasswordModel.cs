﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationStoreApplication.Models
{
	public class PasswordModel
	{
		public string FirstlastName { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmNewPassword { get; set; }
		public string Message { get; set; }
	}
}