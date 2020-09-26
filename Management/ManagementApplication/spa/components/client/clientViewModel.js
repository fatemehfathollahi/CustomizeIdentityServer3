define(['jquery', 'ko', 'commonMethod'], function ($, ko, commonMethod) {
	var ClientViewModel = function (mealParams) {
		var self = this;
		//agar legParams ra nadad vase inke payin field ha error nadahad
		if (!mealParams)
			mealParams = {};
		self.selected = ko.observable(mealParams.selected || false);
		self.Id = ko.observable(mealParams.Id || 0);
		self.Enabled = ko.observable(mealParams.Enabled === false ? false : true);
		self.EnableDescription = ko.observable(mealParams.EnableDescription || "");
		self.ClientId = ko.observable(mealParams.ClientId || "");
		self.ClientName = ko.observable(mealParams.ClientName || "");
		self.ClientUri = ko.observable(mealParams.ClientUri || "");
		self.LogoUri = ko.observable(mealParams.LogoUri || "");
		self.LogoutUri = ko.observable(mealParams.LogoutUri || "");
		self.AllowAccessToAllScopes = ko.observable(mealParams.AllowAccessToAllScopes === true ? true : false);
		self.IdentityTokenLifetime = ko.observable(mealParams.IdentityTokenLifetime || 300);
		self.AccessTokenLifetime = ko.observable(mealParams.IdentityTokenLifetime || 3600);
		self.AuthorizationCodeLifetime = ko.observable(mealParams.AuthorizationCodeLifetime || 300);
		self.AbsoluteRefreshTokenLifetime = ko.observable(mealParams.AbsoluteRefreshTokenLifetime || 2592000);
		self.ClientCorsOrigins = ko.observableArray();
		self.ClientPostLogoutRedirectUris = ko.observableArray();
		self.ClientRedirectUris = ko.observableArray();
		self.ClientScopes = ko.observableArray();
		self.ClientSecrets = ko.observableArray();
		self.Permissions = ko.observableArray();

        self.PhotoChanged = ko.observable(false);
		self.LogoUri = ko.observableArray(mealParams.LogoUri || []);
		 self.viewModel = {};
        self.viewModel.visibleError = ko.observable(false);
        self.viewModel.fileData = ko.observable({
            dataURL: ko.observable(),
            base64String: ko.observable(),
        });
        self.viewModel.fileData.subscribe(function () {
            self.viewModel.visibleError(false);
            self.PhotoChanged(true);
        });
        self.viewModel.onClear = function (fileData) {
            self.viewModel.fileData({
                file: ko.observable(), // will be filled with a File object
                // Read the files (all are optional, e.g: if you're certain that it is a text file, use only text:
                binaryString: ko.observable(), // FileReader.readAsBinaryString(Blob|File) - The result property will contain the file/blob's data as a binary string. Every byte is represented by an integer in the range [0..255].
                text: ko.observable(), // FileReader.readAsText(Blob|File, opt_encoding) - The result property will contain the file/blob's data as a text string. By default the string is decoded as 'UTF-8'. Use the optional encoding parameter can specify a different format.
                dataURL: ko.observable(), // FileReader.readAsDataURL(Blob|File) - The result property will contain the file/blob's data encoded as a data URL.
                arrayBuffer: ko.observable(), // FileReader.readAsArrayBuffer(Blob|File) - The result property will contain the file/blob's data as an ArrayBuffer object.
                // a special observable (optional)
                base64String: ko.observable(), // just the base64 string, without mime type or anything else
                // you can have observable arrays for each of the properties above, useful in multiple file upload selection (see Multiple file Uploads section below)
                // in the format of xxxArray: ko.observableArray(),
                /* e.g: */
                fileArray: ko.observableArray(),
                base64StringArray: ko.observableArray(),
            });
            fileData.clear && fileData.clear();
            self.viewModel.fileData().dataURL([]);
            self.viewModel.visibleError(false);
            self.PhotoChanged(true);
        };
	}
	return {
		ClientViewModel: ClientViewModel
	}
});