define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var corsOriginsViewModel = function (corsParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!corsParams)
			corsParams = {};
		self.selected = ko.observable(corsParams.selected || false);
		self.Id = ko.observable(corsParams.Id || 0);
		self.Origin = ko.observable(corsParams.Origin || "");
	}
	return {
		corsOriginsViewModel: corsOriginsViewModel
	}
});