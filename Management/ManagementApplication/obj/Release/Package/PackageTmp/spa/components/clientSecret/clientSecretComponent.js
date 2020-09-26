define(["ko", "text!/ClientSecret/ClientSecretComponent", "clientSecretViewModel", 'clientScopsValidator'
                , 'commonMethod', "common", "domReady", "logger"],
    function (ko, template, clientSecretViewModel, clientSecretValidator, commonMethod, common, domReady, logger) {
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
    		self.clientRow = new clientSecretViewModel.clientSecretViewModel();

    		var formId = 'ClientSecretsForm';
    		self.dialogOkClick = function (vm, jqEvt) {
    			if (!self.ValidateForm()) {
    				return;
    			}

    			var meParams = {};
    			meParams.value = self.clientRow.Value();
    			meParams.async = false;
    			commonMethod.GetHashCode(function (output, error) {
					 self.clientRow.Value(output);
    				 var result = {};
    				result = pparam;
    				result.okClick = true;
    				result.clientRow = self.clientRow;
    				pparam.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    			}, function (request, status) {
    				logger.error('Error In Updating Data');
    			}, meParams);
    		};
    		self.ValidateForm = function () {
    			return clientSecretValidator.Validate(formId, self.FoodRow);
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