define(["ko", "text!/ClientScops/ClientScopsComponent", "clientScopsViewModel", 'clientScopsValidator'
                , 'commonMethod', "common", "domReady", "logger"],
    function (ko, template, clientScopsViewModel, clientScopsValidator, commonMethod, common, domReady, logger) {
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
    		self.clientRow = new clientScopsViewModel.clientScopsViewModel();

    		var formId = 'ClientScopForm';
    		self.dialogOkClick = function (vm, jqEvt) {
    			if (!self.ValidateForm()) {
    				return;
    			}
    			var result = {};
    			result = pparam;
    			result.okClick = true;
    			result.clientRow = self.clientRow;
    			pparam.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    		};
    		self.ValidateForm = function () {
    			return clientScopsValidator.Validate(formId, self.FoodRow);
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