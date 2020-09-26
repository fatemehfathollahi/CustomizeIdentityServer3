define(["ko", "text!/PostLogoutRedirectURI/PostLogoutRedirectURIComponent", "postLogoutViewModel", 'postLogoutValidator'
                , 'commonMethod', "common", "domReady", "logger"],
    function (ko, template, postLogoutViewModel, postLogoutValidator, commonMethod, common, domReady, logger) {
    	function corsViewModel(pparam) {
    		var self = this;
    		self.loadingVisible = ko.observable(true);
    		self.contentVisible = ko.observable(false);
    		self.enabled = ko.observable(true);
    		self.enableButton = ko.observable(true);
    		self.saveButtonVisible = ko.observable(true);

    		self.callParentCancel = function () {
    			pparam.parentcontext.$data.dialogButtonClick();
    		}
    		self.postLogoutRow = new postLogoutViewModel.postLogoutViewModel();

    		var formId = 'PostLogoutForm';
    		self.dialogOkClick = function (vm, jqEvt) {
    			if (!self.ValidateForm()) {
    				return;
    			}
    			var result = {};
    			result = pparam;
    			result.okClick = true;
    			result.postLogoutRow = self.postLogoutRow;
    			pparam.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    		};
    		self.ValidateForm = function () {
    			return postLogoutValidator.Validate(formId, self.FoodRow);
    		};
    		self.dialogCancelClick = function () {
    			self.callParentCancel();
    		};
    		domReady(function () {
    				self.loadingVisible(false);
    				self.contentVisible(true);
    		});
    	}
    	return {
    		viewModel: corsViewModel,
    		template: template
    	}
    });