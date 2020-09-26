define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var userClientPermissionViewModel = function (mParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!mParams)
			mParams = {};
		self.selected = ko.observable(mParams.selected || false);
		self.Id = ko.observable(mParams.Id || 0);
		self.User_Id = ko.observable(mParams.User_Id || 0);
		self.Client_Id = ko.observable(mParams.Client_Id || 0);
		self.Permission_Id = ko.observable(mParams.Permission_Id || 0);

		self.User_Name = ko.observable(mParams.User_Name || "");
		self.Client_Name = ko.observable(mParams.Client_Name || "");
		self.Permission_Name = ko.observable(mParams.Permission_Name || "");

	}									   
	return {
		userClientPermissionViewModel: userClientPermissionViewModel
	}
}); 