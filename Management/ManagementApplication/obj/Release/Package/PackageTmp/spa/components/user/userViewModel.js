define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var userViewModel = function (mParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!mParams)
			mParams = {};
		self.selected = ko.observable(mParams.selected || false);
		self.Id = ko.observable(mParams.Id || 0);
		self.UserName = ko.observable(mParams.UserName || "");
		self.Email = ko.observable(mParams.Email || "");
		self.PasswordHash = ko.observable("");
		self.PhoneNumber = ko.observable(mParams.PhoneNumber || "");
		self.LockoutEnabled = ko.observable(mParams.LockoutEnabled === true ? true : false);
		self.AccessFailedCount = ko.observable(mParams.AccessFailedCount || 0);
		self.UserPermissions = ko.observableArray();
		//user profile
		self.FirstName = ko.observable(mParams.LastName || "");
		self.LastName = ko.observable(mParams.LastName || "");
		self.Mobile = ko.observable(mParams.Mobile || "");
		self.InternalPhone = ko.observable(mParams.InternalPhone || "");
		self.InternalPhoneCode = ko.observable(mParams.InternalPhoneCode || "");
		self.EmployeeNumber = ko.observable(mParams.EmployeeNumber || "");
		self.NationalCode = ko.observable(mParams.NationalCode || "");
		self.PersonCode = ko.observable(mParams.PersonCode || "");
	}									   
	return {
		userViewModel: userViewModel
	}
});