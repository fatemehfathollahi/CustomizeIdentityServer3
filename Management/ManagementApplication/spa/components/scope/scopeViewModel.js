define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var scopeViewModel = function (mParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!mParams)
			mParams = {};
		self.selected = ko.observable(mParams.selected || false);
		self.Id = ko.observable(mParams.Id || 0);
		self.Enabled = ko.observable(mParams.Enabled === false ? false : true);
		self.Name = ko.observable(mParams.Name || "");
		self.DisplayName = ko.observable(mParams.DisplayName || "");
		self.Description = ko.observable(mParams.Description || "");
		self.Required = ko.observable(mParams.Required === false ? false : true);
		self.Emphasize = ko.observable(mParams.Emphasize ===  false ? false : true);
		self.Type = ko.observable(mParams.Type || 0);
		self.IncludeAllClaimsForUser = ko.observable(mParams.IncludeAllClaimsForUser === false ? false : true);
		self.ClaimsRule = ko.observable(mParams.ClaimsRule || "");
		self.ShowInDiscoveryDocument = ko.observable(mParams.ShowInDiscoveryDocument ===  false ? false : true);
		self.AllowUnrestrictedIntrospection = ko.observable(mParams.AllowUnrestrictedIntrospection ===  false ? false : true);
		self.ScopeClaims = ko.observableArray();
		self.ScopeSecrets = ko.observableArray();
	}
	return {
		scopeViewModel: scopeViewModel
	}
});