define(["ko", "text!/User/UserComponent", "userViewModel", 'userValidator'
                , 'commonMethod', "common", "domReady", "logger", "dialog", "linq", "userClientPermissionViewModel"],
    function (ko, template, userViewModel, userValidator, commonMethod, common,
                domReady, logger, dialog, linq, userClientPermissionViewModel) {
    	function UserViewModel(userparams) {
    		var self = this;
    		var formId = 'UserForm';
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
    		self.goToParentForm = function () {
    			window.location.href = "#/user";
    		}
    		self.userRow = new userViewModel.userViewModel(ko.toJS(userparams.userExtraParams.userRow));

    		switch (userparams.userExtraParams.act) {
    			case new common.action().create:
    				self.headerText('Add User');
    				break;
    			case new common.action().update:
    				self.headerText('Update User');
    				meParams = {};
    				meParams.Id = userparams.userExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetUserWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    			case new common.action().read:
    				self.headerText('Show User');
    				self.enabled(false);
    				self.saveButtonVisible(false);
    				meParams = {};
    				meParams.Id = userparams.userExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetUserWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    			case new common.action().delete:
    				self.headerText('Delete User');
    				self.enabled(false);
    				meParams = {};
    				meParams.Id = userparams.userExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetUserWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    		}
    		self.fillModel = function (output) {
    			for (prop in output) {
    				if (self.userRow[prop] && prop != 'UserPermissions')
    					self.userRow[prop](output[prop]);
    			}

    			for (var i = 0; i < output.UserPermissions.length; i++) {
    				self.userRow.UserPermissions.push(
						   new userClientPermissionViewModel.userClientPermissionViewModel(output.UserPermissions[i])
					   );
    			}
    			self.SortUserPermissionArray();
    			self.showContent();
    		};
    		self.dialogOkClick = function (vm, jqEvt) {
    			//if (!self.ValidateForm())
    			//	return false;

    			switch (userparams.userExtraParams.act) {
    				case new common.action().create: {
    					if (!self.ValidateForm()) {
    						return false;
    					}

    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.saveUser();
    						}, 400);
    					} else {
    						self.saveUser();
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
    							self.updateUser();
    						}, 400);
    					} else {
    						self.updateUser();
    					}
    					break;
    				}
    				case new common.action().delete: {
    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.deleteUser();
    						}, 400);
    					} else {
    						self.deleteUser();
    					}
    					break;
    				}
    			}
    		};
    		self.saveUser = function () {
    			var prParam = {};
    			prParam.userRow = self.userRow;
				commonMethod.AddUser(function (output, error) {
    							self.objDialog.dialog.close();
    							logger.success("Operation Complete");
    							self.goToParentForm();
    						}, function (request, status) {
    							self.objDialog.dialog.close();
    							logger.error("Error in save data.");
    						}, prParam);
    		};
    		self.updateUser = function () {
    			var prParam = {};
    			prParam.userRow = self.userRow;
    			commonMethod.UpdateUser(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.goToParentForm();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.deleteUser = function () {
    			var prParam = {};
    			prParam.userRow = self.userRow;
    			commonMethod.DeleteUser(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.goToParentForm();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.ValidateForm = function () {
    			return userValidator.Validate(formId,self.userRow);
    		};
    		self.cancelClick = function () {
    			self.goToParentForm();
    		};
    		self.dialogButtonClick = function (vm, jqEvt, result) {
    			self.objDialog.dialog.close();
    			if (result && result.okClick) {
    				if (result.permissionExtraParams && result.permissionExtraParams.extraAction == new common.extraAction().addPermission) {
    					self.userRow.UserPermissions.removeAll();
    					result.permissionList().forEach(function (pl) {
    						self.userRow.UserPermissions.push(pl);
    					});
    					self.SortUserPermissionArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deletePermission) {
    					self.userRow.UserPermissions.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortUserPermissionArray();
    				}
    			}
    		};
    		self.addIcon = ko.observable("fa fa-plus bigger-150");
    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
    		self.showIcon = ko.observable("fa fa-info bigger-150");
    		/////////////
    		self.addPermission = function () {
    			permissionExtraParams = {};
    			permissionExtraParams = {};
    		    permissionExtraParams.dialogButtonClick = self.dialogButtonClick;
    		    permissionExtraParams.extraAction = new common.extraAction().addPermission;
    		    permissionExtraParams.permissionList = self.userRow.UserPermissions;
    		    var parameters = {
    		        dialogid: 'addpermissiondialog',
    		        title: 'Add Permission',
    		        dialogtype: 'info',
    		        dialogcontenttype: 'component',
    		        bodyContent: '<clientpermissionlistcomponent  params = {parentcontext:$context,parentdata:$data,permissionExtraParams:permissionExtraParams}>',
    		        dialogparent: self,
    		        closable: true,
    		        draggable: true,
    		        size: 'wide'
    		    }
    		    self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deletePermission = function () {
    			if (!self.checkSelectedPermission()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deletePermission;
    			confirmExtraParams.message = "Do you want to delete Scope Claim? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Scope Claim',
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
    		self.checkSelectedPermission = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.userRow.UserPermissions())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortUserPermissionArray = function () {
    			self.currentRowId(null);
    			self.userRow.UserPermissions.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.userRow.UserPermissions().length; i++) {
    				self.userRow.UserPermissions()[i].selected(false);
    			}
    			self.removeSelectedFromPermissionGrid();
    		};
    		self.GetNewPermissionId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.userRow.UserPermissions().length; i++) {
    				if (tempId == null) {
    					tempId = self.userRow.UserPermissions()[i].Id();
    				}
    				else if (tempId > self.userRow.UserPermissions()[i].Id())
    					tempId = self.userRow.UserPermissions()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.PermissionGridClick = function () {
    			$('#UserClientPermissionGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#UserClientPermissionGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.userRow.UserPermissions().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromPermissionGrid = function () {
    			$('#UserClientPermissionGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setPermissionRowClickInterval = setInterval(function () {
    			if (document.getElementById('UserClientPermissionGrid')) {
    				clearInterval(self.setPermissionRowClickInterval);
    				if (userparams.userExtraParams && userparams.userExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#UserClientPermissionGrid tbody').on('click', 'tr', self.PermissionGridClick);
    			}
    		}, 500);
    		/////////////
    		domReady(function () {
    			userparams.userExtraParams.parent.hideLoading();
    			if (userparams.userExtraParams.act == new common.action().create)
    				self.showContent();
    		});
    	}
    	return {
    		viewModel: UserViewModel,
    		template: template
    	}
    });