define(["ko", "text!/ScopeSecret/ScopeSecretComponent", "scopeSecretViewModel", 'scopeSecretValidator'
                , 'commonMethod', "common", "domReady", "logger"],
    function (ko, template, scopeSecretViewModel, scopeSecretValidator, commonMethod, common, domReady, logger) {
    	function scopeSecretsViewModel(pparam) {
    		var self = this;
    		self.loadingVisible = ko.observable(true);
    		self.contentVisible = ko.observable(false);
    		self.enabled = ko.observable(true);
    		self.enableButton = ko.observable(true);
    		self.saveButtonVisible = ko.observable(true);

    		self.callParentCancel = function () {
    			pparam.parentcontext.$data.dialogButtonClick();
    		}
    		self.clientRow = new scopeSecretViewModel.scopeSecretViewModel();

    		var formId = 'ScopeSecretForm';
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
    			return scopeSecretValidator.Validate(formId, self.FoodRow);
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
    		viewModel: scopeSecretsViewModel,
    		template: template
    	}
    });