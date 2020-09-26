define(["ko", "text!/Role/RoleComponent", "roleViewModel", 'roleValidator'
                , 'commonMethod', "common", "domReady", "logger", "dialog", "linq", "permisionViewModel"
				],
    function (ko, template, RoleViewModel, roleValidator, commonMethod, common,
                domReady, logger, dialog, linq, permisionViewModel) {
    	function RestaurantViewModel(roleparams) {
    		var self = this;
    		var formId = 'roleForm';
    		self.loadingVisible = ko.observable(true);
    		self.contentVisible = ko.observable(false);
    		self.enabled = ko.observable(true);
    		self.enableButton = ko.observable(true);
    		self.saveButtonVisible = ko.observable(true);
    		self.currentRowId = ko.observable();
    		self.currentName = ko.observable();
    		self.objDialog = null;
    		self.headerText = ko.observable();
    		self.loadAjaxContent = ko.observable(false);
    		self.showContent = function () {
    			self.loadingVisible(false);
    			self.contentVisible(true);
    		};
    		self.callParentCancel = function () {
    			roleparams.parentcontext.$data.dialogButtonClick();
    		};
    		self.callParentOkClick = function (vm, jqEvt) {
    			var result = {};
    			result = roleparams;
    			result.okClick = true;
    			roleparams.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
    		};
    		self.roleRow = new RoleViewModel.RoleViewModel(ko.toJS(roleparams.roleExtraParams.roleRow));

    		switch (roleparams.roleExtraParams.act) {
    			case new common.action().create:
    				self.headerText('Add Role');
    				break;
    			case new common.action().update:
    			    self.headerText('Update Role');
    				meParams = {};
    				meParams.Id = roleparams.roleExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetRoleWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.callParentOkClick();
    				}, meParams);
    				break;
    			case new common.action().read:
    				self.headerText('Show Role');
    				self.enabled(false);
    				self.saveButtonVisible(false);
    				meParams = {};
    				meParams.Id = roleparams.roleExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetRoleWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.callParentOkClick();
    				}, meParams);
    				break;
    			case new common.action().delete:
    				self.headerText('Delete Role');
    				self.enabled(false);
    				meParams = {};
    				meParams.Id = roleparams.roleExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetRoleWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.callParentOkClick();
    				}, meParams);
    				break;
    		}
    		self.fillModel = function (output) {
    			for (prop in output) {
    			    if (self.roleRow[prop] && prop != 'Users' && prop != 'Permissions')
    					self.roleRow[prop](output[prop]);
    			}

    			for (var i = 0; i < output.Permissions.length; i++) {
    				self.roleRow.Permissions.push(
						   new permisionViewModel.permisionViewModel(output.Permissions[i])
					   );
    			}
    			self.SortPermisionArray();
    			self.showContent();
    		};
    		self.dialogOkClick = function (vm, jqEvt) {
    			//if (!self.ValidateForm())
    			//	return false;

    			switch (roleparams.roleExtraParams.act) {
    				case new common.action().create: {
    					if (!self.ValidateForm()) {
    						return false;
    					}

    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.saveRole();
    						}, 400);
    					} else {
    						self.saveRole();
    					}
    					break;
    				}
    				case new common.action().update: {
    					if (!self.ValidateForm()) {
    						return;
    					}
    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.updateRole();
    						}, 400);
    					} else {
    						self.updateRole();
    					}
    					break;
    				}
    				case new common.action().delete: {
    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.deleteRole();
    						}, 400);
    					} else {
    						self.deleteRole();
    					}
    					break;
    				}
    			}
    		};
    		self.dialogCancelClick = function () {
    			self.callParentCancel();
    		};
    		self.saveRole = function () {
    			var prParam = {};
    			self.roleRow.ClientId(roleparams.roleExtraParams.ClientId);
    			prParam.roleRow = self.roleRow;
				commonMethod.AddRole(function (output, error) {
    							self.objDialog.dialog.close();
    							logger.success("Operation Complete");
    							self.callParentOkClick();
    						}, function (request, status) {
    							self.objDialog.dialog.close();
    							logger.error("Error in save data.");
    						}, prParam);
    		};
    		self.updateRole = function () {
    			var prParam = {};
    			self.roleRow.ClientId(roleparams.roleExtraParams.ClientId);
    			prParam.roleRow = self.roleRow;
    			commonMethod.UpdateRole(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.callParentOkClick();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.deleteRole = function () {
    			var prParam = {};
    			self.roleRow.ClientId(roleparams.roleExtraParams.ClientId);
    			prParam.roleRow = self.roleRow;
    			commonMethod.DeleteRole(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.callParentOkClick();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.ValidateForm = function () {
    			return roleValidator.Validate(formId);
    		};
    		self.dialogButtonClick = function (vm, jqEvt, result) {
    			self.objDialog.dialog.close();
    			if (result && result.okClick) {
    				if (result.permisionsExtraParams && result.permisionsExtraParams.extraAction == new common.extraAction().addPermision) {
    					var corRow = new permisionViewModel.permisionViewModel(ko.toJS(result.permisionRow));
    					corRow.Id(self.GetNewPermisionId());
    					self.roleRow.Permissions.push(corRow);
    					self.SortPermisionArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deletePermision) {
    					self.roleRow.Permissions.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortPermisionArray();
    				}
    			}
    		};
    		self.addIcon = ko.observable("fa fa-plus bigger-150");
    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
    		self.showIcon = ko.observable("fa fa-info bigger-150");
    		/////////////
    		self.addPermissions = function () {
    			permisionsExtraParams = {};
    			permisionsExtraParams.dialogButtonClick = self.dialogButtonClick;
    			permisionsExtraParams.extraAction = new common.extraAction().addPermision;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Permision',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<permisioncomponent  params = {parentcontext:$context,parentdata:$data,permisionsExtraParams:permisionsExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deletePermissions = function () {
    			if (!self.checkSelectedPermision()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deletePermision;
    			confirmExtraParams.message = "Do you want to delete Permision? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Permision',
    				dialogtype: 'danger',
    				dialogcontenttype: 'component',
    				bodyContent: '<confirmcomponent  params = {parentcontext:$context,parentdata:$data,confirmExtraParams:confirmExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'large'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    			self.deleteIcon("fa fa-remove bigger-150");
    		};
    		self.checkSelectedPermision = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.roleRow.Permissions())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortPermisionArray = function () {
    			self.currentRowId(null);
    			self.roleRow.Permissions.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.roleRow.Permissions().length; i++) {
    				self.roleRow.Permissions()[i].selected(false);
    			}
    			self.removeSelectedFromPermisionGrid();
    		};
    		self.GetNewPermisionId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.roleRow.Permissions().length; i++) {
    				if (tempId == null) {
    					tempId = self.roleRow.Permissions()[i].Id();
    				}
    				else if (tempId > self.roleRow.Permissions()[i].Id())
    					tempId = self.roleRow.Permissions()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.PermisionGridClick = function () {
    			$('#permisionGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#permisionGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.roleRow.Permissions().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromPermisionGrid = function () {
    			$('#permisionGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setPermisionRowClickInterval = setInterval(function () {
    			if (document.getElementById('permisionGrid')) {
    				clearInterval(self.setPermisionRowClickInterval);
    				if (roleparams.roleExtraParams && roleparams.roleExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#permisionGrid tbody').on('click', 'tr', self.PermisionGridClick);
    			}
    		}, 500);
    		////////////
    		domReady(function () {
    			if (roleparams.roleExtraParams.act == new common.action().create)
    				self.showContent();
    		});
    	}
    	return {
    		viewModel: RestaurantViewModel,
    		template: template
    	}
    });