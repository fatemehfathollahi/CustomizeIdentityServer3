define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var scopeSecretViewModel = function (pParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!pParams)
			pParams = {};
		self.selected = ko.observable(pParams.selected || false);
		self.Id = ko.observable(pParams.Id || 0);
		self.Type = ko.observable(pParams.Type || "");
		self.Description = ko.observable(pParams.Description || "");
		self.Value = ko.observable(pParams.Value || "");
	}
	return {
		scopeSecretViewModel: scopeSecretViewModel
	}
});