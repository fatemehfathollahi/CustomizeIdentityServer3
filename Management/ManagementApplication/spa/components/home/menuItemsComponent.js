define(["ko", "text!/home/menuItemsComponent", "domReady", "commonMethod", "logger"],
            function (ko, template, domReady, commonMethod, logger) {
            	function menuItemsViewModel(homeParentParams) {
        var self = this;
        //self.loadingVisible = ko.observable(false);
        //self.contentVisible = ko.observable(false);
        //commonMethod.AddSetting(function (output) {
        //    self.loadingVisible(false);
        //    self.contentVisible(true);
        //}, function (request) {
        //    self.loadingVisible(false);
        //    self.contentVisible(true);
        //    logger.error('خطا در فراخوانی تنظیمات پایه ای');
        //});
        domReady(function () {
            //homeParentParams.parent.hideLoading();
            //self.loadingVisible(true);
            //self.contentVisible(true);
        });
    }
    return {
    	viewModel: menuItemsViewModel,
        template: template
    }
});