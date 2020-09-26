define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var clientSecretViewModel = function (pParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!pParams)
			pParams = {};
		self.selected = ko.observable(pParams.selected || false);
		self.Id = ko.observable(pParams.Id || 0);
		self.Value = ko.observable(pParams.Value || "");
		self.Type = ko.observable("SharedSecret");
		self.Description = ko.observable(pParams.Description || "");
	}
	return {
		clientSecretViewModel: clientSecretViewModel
	}
});