define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var RoleViewModel = function (mealParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!mealParams)
			mealParams = {};
		self.selected = ko.observable(mealParams.selected || false);
		self.Id = ko.observable(mealParams.Id || 0);
		self.Name = ko.observable(mealParams.Name || "");
		self.Description = ko.observable(mealParams.Description || "");
		self.ClientId = ko.observable(mealParams.ClientId || 0);
		self.Permissions = ko.observableArray();
		self.Users = ko.observableArray();
	}
	return {
		RoleViewModel: RoleViewModel
	}
});