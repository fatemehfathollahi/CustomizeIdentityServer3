define(["ko", "text!/home/homeComponent", "domReady", "commonMethod", "logger"],
            function (ko, template, domReady, commonMethod, logger) {
            	function homeViewModel(homeParentParams) {
            		var self = this;
            		homeParentParams.parent.hideLoading();
            		self.loadingVisible = ko.observable(true);
            		self.contentVisible = ko.observable(false);
            		//self.settingLoad = ko.observable(false);
            		//commonMethod.AddSetting(function (output) {
            		//	self.loadingVisible(false);
            		//	self.contentVisible(true);
            		//	self.settingLoad(true);
            		//	//homeParentParams.parent.hideLoading();
            		//}, function (request) {
            		//	self.loadingVisible(false);
            		//	self.contentVisible(true);
            		//	logger.error('خطا در فراخوانی تنظیمات پایه ای');
            		//});
            		domReady(function () {
            			//homeParentParams.parent.hideLoading();
            			//if (!self.settingLoad())
            			self.loadingVisible(false);
            			self.contentVisible(true);
            		});
            	}
            	return {
            		viewModel: homeViewModel,
            		template: template
            	}
            });