define(["ko", "text!/home/headerComponent", "domReady", "commonMethod","logger"], function (ko, template, domReady, commonMethod,logger) {
    function headerViewModel() {
    	var self = this;
    	//self.UserName = ko.observable(' یعقوب جلالی ');
    	self.UserName = ko.observable('...');
    	self.GoToPersonelInfo = function () {
    		//commonMethod.GetCurrentUser(function (output) {
    		//	if (output) {
    		//		window.location.href = "#/personel/viewPersonel/" + output.Id;
    		//	}
    		//}, function (error) {
    		//	logger.error(error.responseText.replace('{"Message":"', '').replace('"}', ''));
    		//});
    	};
    	domReady(function () {
    		//commonMethod.GetCurrentUserName(function (output) {
    		//	self.UserName(output);
    		//}, function (error) {
    		//	logger.error(error.responseText.replace('{"Message":"', '').replace('"}', ''));
    		//});
    	});
    }
    return {
        viewModel: headerViewModel,
        template: template
    }
});