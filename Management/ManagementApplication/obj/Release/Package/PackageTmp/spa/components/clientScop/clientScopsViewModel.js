define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var clientScopsViewModel = function (pParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!pParams)
			pParams = {};
		self.selected = ko.observable(pParams.selected || false);
		self.Id = ko.observable(pParams.Id || 0);
		self.Scope = ko.observable(pParams.Scope || "");
	}
	return {
		clientScopsViewModel: clientScopsViewModel
	}
});