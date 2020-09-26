define(["ko", "text!/Permision/PermisionComponent", "permisionViewModel", 'permisionValidator'
                , 'commonMethod', "common", "domReady", "logger","dialog"],
    function (ko, template, permisionViewModel, permisionValidator, commonMethod, common, domReady, logger, dialog) {
    	function corsViewModel(pparam) {
    		var self = this;
    		self.loadingVisible = ko.observable(true);
    		self.contentVisible = ko.observable(false);
    		self.enabled = ko.observable(true);
    		self.enableButton = ko.observable(true);
    		self.saveButtonVisible = ko.observable(true);
    		self.objDialog = null;
    		self.callParentCancel = function () {
    			pparam.parentcontext.$data.dialogButtonClick();
    		}
    		self.permisionRow = new permisionViewModel.permisionViewModel();
    		self.showContent = function () {
    			self.loadingVisible(false);
    			self.contentVisible(true);
    		};
    		var formId = 'PermisionForm';
    		switch (pparam.permisionsExtraParams.act) {
    			case new common.action().create:
    				break;
    			case new common.action().update:
    				meParams = {};
    				meParams.Id = pparam.permisionsExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetPermissionWithId(function (output, error) {
    					self.permisionRow.Id(output.Id);
    					self.permisionRow.Name(output.Name);
    					self.showContent();
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.callParentCancel();
    				}, meParams);
    				break;
    			case new common.action().read:
    				self.enabled(false);
    				self.saveButtonVisible(false);
    				meParams = {};
    				meParams.Id = pparam.permisionsExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetPermissionWithId(function (output, error) {
    					self.permisionRow.Id(output.Id);
    					self.permisionRow.Name(output.Name);
    					self.showContent();
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.callParentCancel();
    				}, meParams);
    				break;
    			case new common.action().delete:
    				self.enabled(false);
    				meParams = {};
    				meParams.Id = pparam.permisionsExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetPermissionWithId(function (output, error) {
    					self.permisionRow.Id(output.Id);
    					self.permisionRow.Name(output.Name);
    					self.showContent();
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.callParentCancel();
    				}, meParams);
    				break;
    		}
    		self.dialogOkClick = function (vm, jqEvt) {
    			switch (pparam.permisionsExtraParams.act) {
    				case new common.action().create: {
    					if (!self.ValidateForm()) {
    						return false;
    					}

    					self.objDialog = dialog.getWaitDialog();
    					var prParam = {};
    					prParam.permisionRow = self.permisionRow;
    					commonMethod.AddPermission(function (output, error) {
    						self.objDialog.dialog.close();
    						logger.success("Operation Complete");

    						var result = {};
    						result = pparam;
    						result.okClick = true;
    						pparam.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    					}, function (request, status) {
    						self.objDialog.dialog.close();
    						logger.error("Error in save data.");
    					}, prParam);
    					break;
    				}
    				case new common.action().update: {
    					if (!self.ValidateForm()) {
    						return;
    					}
    					self.objDialog = dialog.getWaitDialog();
    					var prParam = {};
    					prParam.permisionRow = self.permisionRow;
    					commonMethod.UpdatePermission(function (output, error) {
    						self.objDialog.dialog.close();
    						logger.success("Operation Complete");
    						var result = {};
    						result = pparam;
    						result.okClick = true;
    						pparam.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    					}, function (request, status) {
    						self.objDialog.dialog.close();
    						logger.error("Error in save data.");
    					}, prParam);
    					break;
    				}
    				case new common.action().delete: {
    					self.objDialog = dialog.getWaitDialog();
    					var prParam = {};
    					prParam.permisionRow = self.permisionRow;
    					commonMethod.DeletePermission(function (output, error) {
    						self.objDialog.dialog.close();
    						logger.success("Operation Complete");
    						var result = {};
    						result = pparam;
    						result.okClick = true;
    						pparam.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    					}, function (request, status) {
    						self.objDialog.dialog.close();
    						logger.error("Error in save data.");
    					}, prParam);
    					break;
    				}
    			}
    		};

    		self.ValidateForm = function () {
    			return permisionValidator.Validate(formId, self.permisionRow);
    		};
    		self.dialogCancelClick = function () {
    			self.callParentCancel();
    		};
    		domReady(function () {
    			if (pparam.permisionsExtraParams.act == new common.action().create) {
    				self.showContent();
    			}
    		});
    	}
    	return {
    		viewModel: corsViewModel,
    		template: template
    	}
    });