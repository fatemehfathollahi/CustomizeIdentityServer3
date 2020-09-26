define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var scopeClaimViewModel = function (pParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!pParams)
			pParams = {};
		self.selected = ko.observable(pParams.selected || false);
		self.Id = ko.observable(pParams.Id || 0);
		self.Name = ko.observable(pParams.Name || "");
		self.Description = ko.observable(pParams.Description || "");
		self.AlwaysIncludeInIdToken = ko.observable(pParams.AlwaysIncludeInIdToken === false ? false : true);
	}
	return {
		scopeClaimViewModel: scopeClaimViewModel
	}
});