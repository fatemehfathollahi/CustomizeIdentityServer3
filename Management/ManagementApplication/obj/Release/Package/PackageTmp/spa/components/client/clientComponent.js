define(["ko", "text!/Client/ClientComponent", "clientViewModel", 'clientValidator'
                , 'commonMethod', "common", "domReady", "logger", "dialog", "linq", "corsOriginViewModel", "postLogoutViewModel"
				, "clientRedirectViewModel", "clientScopsViewModel", "clientSecretViewModel", "permisionViewModel", "fileBindings"],
    function (ko, template, ClientRowViewModel, clientValidator, commonMethod, common,
                domReady, logger, dialog, linq, corsOriginViewModel, postLogoutViewModel, clientRedirectViewModel,
                clientScopsViewModel, clientSecretViewModel, permisionViewModel,fileBindings) {
    	function ClientViewModel(clientparams) {
    		var self = this;
    		var formId = 'clientForm';
    		self.loadingVisible = ko.observable(true);
    		self.contentVisible = ko.observable(false);
    		self.enabled = ko.observable(true);
    		self.enableButton = ko.observable(true);
    		self.saveButtonVisible = ko.observable(true);
    		self.currentRowId = ko.observable();
    		self.currentName = ko.observable();
    		self.enabledShowImage = ko.computed(function () {
    			return (!self.enabled());
    		});
    		self.objDialog = null;
    		self.headerText = ko.observable();
    		self.loadAjaxContent = ko.observable(false);
    		self.showContent = function () {
    			self.loadingVisible(false);
    			self.contentVisible(true);
    		};
    		self.goToParentForm = function () {
    			window.location.href = "#/client";
    		}
    		self.clientRow = new ClientRowViewModel.ClientViewModel(ko.toJS(clientparams.clientExtraParams.clientRow));

    		switch (clientparams.clientExtraParams.act) {
    			case new common.action().create:
    				self.headerText('Add Client');
    				break;
    			case new common.action().update:
    				self.headerText('Update Client');
    				meParams = {};
    				meParams.Id = clientparams.clientExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetClientWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    			case new common.action().read:
    				self.headerText('Show Client');
    				self.enabled(false);
    				self.saveButtonVisible(false);
    				meParams = {};
    				meParams.Id = clientparams.clientExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetClientWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    			case new common.action().delete:
    				self.headerText('Delete Client');
    				self.enabled(false);
    				meParams = {};
    				meParams.Id = clientparams.clientExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetClientWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    		}
    		self.fillModel = function (output) {
    			for (prop in output) {
    				if (self.clientRow[prop] && prop != 'ClientCorsOrigins' && prop != 'ClientPostLogoutRedirectUris'
							&& prop != 'ClientRedirectUris' && prop != 'ClientScopes' && prop != 'ClientSecrets' && prop != 'Permissions')
    					self.clientRow[prop](output[prop]);
    			}
    			if (output.LogoUri != "") {
    				file = {};
    				file.name = "Defualt Logo";
    				fileArray = [file];
    				self.clientRow.viewModel.fileData({
    					file: ko.observable(file),
    					binaryString: ko.observable(output.LogoUri),
    					text: ko.observable(),
    					dataURL: ko.observable("data:image/jpeg;base64," + output.LogoUri),
    					arrayBuffer: ko.observable(),
    					base64String: ko.observable(output.LogoUri),
    					fileArray: ko.observableArray(),
    					base64StringArray: ko.observableArray(),
    				});
    				self.clientRow.PhotoChanged(false);
    			}
    			for (var i = 0; i < output.ClientCorsOrigins.length; i++) {
    				self.clientRow.ClientCorsOrigins.push(
						   new corsOriginViewModel.corsOriginsViewModel(output.ClientCorsOrigins[i])
					   );
    			}
    			self.SortCorsOriginArray();
    			for (var i = 0; i < output.ClientPostLogoutRedirectUris.length; i++) {
    				self.clientRow.ClientPostLogoutRedirectUris.push(
						   new postLogoutViewModel.postLogoutViewModel(output.ClientPostLogoutRedirectUris[i])
					   );
    			}
    			self.SortURIArray();
    			for (var i = 0; i < output.ClientRedirectUris.length; i++) {
    				self.clientRow.ClientRedirectUris.push(
						   new clientRedirectViewModel.clientRedirectViewModel(output.ClientRedirectUris[i])
					   );
    			}
    			self.SortClientURIArray();
    			for (var i = 0; i < output.ClientScopes.length; i++) {
    				self.clientRow.ClientScopes.push(
						   new clientScopsViewModel.clientScopsViewModel(output.ClientScopes[i])
					   );
    			}
    			self.SortClientScopsArray();
    			for (var i = 0; i < output.ClientSecrets.length; i++) {
    				self.clientRow.ClientSecrets.push(
						   new clientSecretViewModel.clientSecretViewModel(output.ClientSecrets[i])
					   );
    			}
    			self.SortClientSecretsArray();
    			for (var i = 0; i < output.Permissions.length; i++) {
    				self.clientRow.Permissions.push(
						   new permisionViewModel.permisionViewModel(output.Permissions[i])
					   );
    			}
    			self.SortPermissionsArray();
    			self.showContent();
    		};
    		self.dialogOkClick = function (vm, jqEvt) {
    			//if (!self.ValidateForm())
    			//	return false;

    			switch (clientparams.clientExtraParams.act) {
    				case new common.action().create: {
    					if (!self.ValidateForm()) {
    						return false;
    					}

    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.saveClient();
    						}, 400);
    					} else {
    						self.saveClient();
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
    							self.updateClient();
    						}, 400);
    					} else {
    						self.updateClient();
    					}
    					break;
    				}
    				case new common.action().delete: {
    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.deleteClient();
    						}, 400);
    					} else {
    						self.deleteClient();
    					}
    					break;
    				}
    			}
    		};
    		self.saveClient = function () {
    			var prParam = {};
    			prParam.clientRow = self.clientRow;
    			commonMethod.AddClient(function (output, error) {
    							self.objDialog.dialog.close();
								self.clientRow = new ClientRowViewModel.ClientViewModel();
    							logger.success("Operation Complete");
    							self.goToParentForm();
    						}, function (request, status) {
    							self.objDialog.dialog.close();
    							logger.error("Error in save data.");
    						}, prParam);
    		};
    		self.updateClient = function () {
    			var prParam = {};
    			prParam.clientRow = self.clientRow;
    			commonMethod.UpdateClient(function (output, error) {
    				self.objDialog.dialog.close();
self.clientRow = new ClientRowViewModel.ClientViewModel();
    				logger.success("Operation Complete");
    				self.goToParentForm();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.deleteClient = function () {
    			var prParam = {};
    			prParam.clientRow = self.clientRow;
    			commonMethod.DeleteClient(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.goToParentForm();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.ValidateForm = function () {
    			return clientValidator.Validate(formId,self.clientRow);
    		};
    		self.ValidatePersonelRestaurants = function () {
    			var result = true;
    			if (!self.personelRow.AllRestaurants() && self.personelRow.PersonelRestaurantsDTO().length == 0) {
    				result = false;
    				logger.error("رستورانی برای شخص انتخاب نشده است");
    			}
    			return result;
    		};
    		self.cancelClick = function () {
    			self.goToParentForm();
    		};
    		self.dialogButtonClick = function (vm, jqEvt, result) {
    			self.objDialog.dialog.close();
    			if (result && result.okClick) {
    				if (result.corsExtraParams && result.corsExtraParams.extraAction == new common.extraAction().addCorsOrigin) {
    					var corRow = new corsOriginViewModel.corsOriginsViewModel(ko.toJS(result.corsOrigin));
    					corRow.Id(self.GetNewCorsOriginId());
    					self.clientRow.ClientCorsOrigins.push(corRow);
    					self.SortCorsOriginArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteCorsOrigin) {
    					self.clientRow.ClientCorsOrigins.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortCorsOriginArray();
    				} else if (result.uriExtraParams && result.uriExtraParams.extraAction == new common.extraAction().addURI) {
    					var pRow = new postLogoutViewModel.postLogoutViewModel(ko.toJS(result.postLogoutRow));
    					pRow.Id(self.GetNewURIId());
    					self.clientRow.ClientPostLogoutRedirectUris.push(pRow);
    					self.SortURIArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteURI) {
    					self.clientRow.ClientPostLogoutRedirectUris.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortURIArray();
    				} else if (result.uriExtraParams && result.uriExtraParams.extraAction == new common.extraAction().addClientURI) {
    					var pRow = new clientRedirectViewModel.clientRedirectViewModel(ko.toJS(result.clientRow));
    					pRow.Id(self.GetNewClientURIId());
    					self.clientRow.ClientRedirectUris.push(pRow);
    					self.SortClientURIArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteClientURI) {
    					self.clientRow.ClientRedirectUris.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortClientURIArray();
    				}
    				else if (result.uriExtraParams && result.uriExtraParams.extraAction == new common.extraAction().addClientScops) {
    					var pRow = new clientScopsViewModel.clientScopsViewModel(ko.toJS(result.clientRow));
    					pRow.Id(self.GetNewClientScopsId());
    					self.clientRow.ClientScopes.push(pRow);
    					self.SortClientScopsArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteClientScops) {
    					self.clientRow.ClientScopes.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortClientScopsArray();
    				}
    				else if (result.uriExtraParams && result.uriExtraParams.extraAction == new common.extraAction().addClientSecrets) {
    					var pRow = new clientSecretViewModel.clientSecretViewModel(ko.toJS(result.clientRow));
    					pRow.Id(self.GetNewClientSecretsId());
    					self.clientRow.ClientSecrets.push(pRow);
    					self.SortClientSecretsArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteClientSecret) {
    					self.clientRow.ClientSecrets.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortClientSecretsArray();
    				}
    				else if (result.permissionExtraParams && result.permissionExtraParams.extraAction == new common.extraAction().addPermission) {
    					self.clientRow.Permissions.removeAll();
    				    result.permissionList().forEach(function (pl) {
    				       // var pRow = new permisionViewModel.permisionViewModel(ko.toJS(pl));
    				        self.clientRow.Permissions().push(pl);
    				    });
    				    self.SortPermissionsArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deletePermission) {
    					var tRow = Enumerable.From(self.clientRow.Permissions())
									.Where(function (x) { return x.Id() == self.currentRowId() })
									.ToArray();
    					if (tRow.length > 0) {
    						self.clientRow.Permissions.remove(tRow[0]);
    					}
    					self.currentRowId(null);
    				}
    			}
    		};
    		self.addIcon = ko.observable("fa fa-plus bigger-150");
    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
    		self.showIcon = ko.observable("fa fa-info bigger-150");
    		self.addCorsOrigins = function () {
    			corsExtraParams = {};
    			corsExtraParams.dialogButtonClick = self.dialogButtonClick;
    			corsExtraParams.extraAction = new common.extraAction().addCorsOrigin;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Cors Origins',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<corsorigincomponent  params = {parentcontext:$context,parentdata:$data,corsExtraParams:corsExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteCorsOrigins = function () {
    			if (!self.checkSelectedCorsOrigin()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteCorsOrigin;
    			confirmExtraParams.message = "Do you want to delete current Cors Origin? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Cors Origin',
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
    		self.checkSelectedCorsOrigin = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.clientRow.ClientCorsOrigins())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortCorsOriginArray = function () {
    			self.currentRowId(null);
    			self.clientRow.ClientCorsOrigins.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.clientRow.ClientCorsOrigins().length; i++) {
    				self.clientRow.ClientCorsOrigins()[i].selected(false);
    			}
    			self.removeSelectedFromCorsOriginGrid();
    		};
    		self.GetNewCorsOriginId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.clientRow.ClientCorsOrigins().length; i++) {
    				if (tempId == null) {
    					tempId = self.clientRow.ClientCorsOrigins()[i].Id();
    				}
    				else if (tempId > self.clientRow.ClientCorsOrigins()[i].Id())
    					tempId = self.clientRow.ClientCorsOrigins()[i].Id()
    			}
    			if (tempId == null || tempId>0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		/////////////
    		self.addURI = function () {
    			uriExtraParams = {};
    			uriExtraParams.dialogButtonClick = self.dialogButtonClick;
    			uriExtraParams.extraAction = new common.extraAction().addURI;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add URI',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<postlogoutcomponent  params = {parentcontext:$context,parentdata:$data,uriExtraParams:uriExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteURI = function () {
    			if (!self.checkSelectedURI()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteURI;
    			confirmExtraParams.message = "Do you want to delete current URI? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Cors Origin',
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
    		self.checkSelectedURI = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.clientRow.ClientPostLogoutRedirectUris())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortURIArray = function () {
    			self.currentRowId(null);
    			self.clientRow.ClientPostLogoutRedirectUris.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.clientRow.ClientPostLogoutRedirectUris().length; i++) {
    				self.clientRow.ClientPostLogoutRedirectUris()[i].selected(false);
    			}
    			self.removeSelectedFromURIGrid();
    		};
    		self.GetNewURIId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.clientRow.ClientPostLogoutRedirectUris().length; i++) {
    				if (tempId == null) {
    					tempId = self.clientRow.ClientPostLogoutRedirectUris()[i].Id();
    				}
    				else if (tempId > self.clientRow.ClientPostLogoutRedirectUris()[i].Id())
    					tempId = self.clientRow.ClientPostLogoutRedirectUris()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		/////////////
    		self.addClientURI = function () {
    			uriExtraParams = {};
    			uriExtraParams.dialogButtonClick = self.dialogButtonClick;
    			uriExtraParams.extraAction = new common.extraAction().addClientURI;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Client Redirect URI',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<clientredirectcomponent  params = {parentcontext:$context,parentdata:$data,uriExtraParams:uriExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteClientURI = function () {
    			if (!self.checkSelectedClientRedirectURI()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteClientURI;
    			confirmExtraParams.message = "Do you want to delete Client Redirect URI? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Client Redirect URI',
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
    		self.checkSelectedClientRedirectURI = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.clientRow.ClientRedirectUris())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortClientURIArray = function () {
    			self.currentRowId(null);
    			self.clientRow.ClientRedirectUris.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.clientRow.ClientRedirectUris().length; i++) {
    				self.clientRow.ClientRedirectUris()[i].selected(false);
    			}
    			self.removeSelectedFromClientRedirectGrid();
    		};
    		self.GetNewClientURIId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.clientRow.ClientRedirectUris().length; i++) {
    				if (tempId == null) {
    					tempId = self.clientRow.ClientRedirectUris()[i].Id();
    				}
    				else if (tempId > self.clientRow.ClientRedirectUris()[i].Id())
    					tempId = self.clientRow.ClientRedirectUris()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.clientRedirectGridClick = function () {
    			$('#ClientRedirectURIGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#ClientRedirectURIGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.clientRow.ClientRedirectUris().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromClientRedirectGrid = function () {
    			$('#ClientRedirectURIGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setClientRedirectRowClickInterval = setInterval(function () {
    			if (document.getElementById('ClientRedirectURIGrid')) {
    				clearInterval(self.setClientRedirectRowClickInterval);
    				if (clientparams.clientExtraParams && clientparams.clientExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#ClientRedirectURIGrid tbody').on('click', 'tr', self.clientRedirectGridClick);
    			}
    		}, 500);
    		/////////////
    		self.addClientScops = function () {
    			uriExtraParams = {};
    			uriExtraParams.dialogButtonClick = self.dialogButtonClick;
    			uriExtraParams.extraAction = new common.extraAction().addClientScops;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Client Scope',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<clientscopscomponent  params = {parentcontext:$context,parentdata:$data,uriExtraParams:uriExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteClientScops = function () {
    			if (!self.checkSelectedClientScops()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteClientScops;
    			confirmExtraParams.message = "Do you want to delete Client Scope? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Client Redirect URI',
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
    		self.checkSelectedClientScops = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.clientRow.ClientScopes())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortClientScopsArray = function () {
    			self.currentRowId(null);
    			self.clientRow.ClientScopes.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.clientRow.ClientScopes().length; i++) {
    				self.clientRow.ClientScopes()[i].selected(false);
    			}
    			self.removeSelectedFromClientScopsGrid();
    		};
    		self.GetNewClientScopsId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.clientRow.ClientScopes().length; i++) {
    				if (tempId == null) {
    					tempId = self.clientRow.ClientScopes()[i].Id();
    				}
    				else if (tempId > self.clientRow.ClientScopes()[i].Id())
    					tempId = self.clientRow.ClientScopes()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.clientScopsGridClick = function () {
    			$('#ClientScopsGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#ClientScopsGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.clientRow.ClientScopes().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromClientScopsGrid = function () {
    			$('#ClientScopsGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setClientScopsRowClickInterval = setInterval(function () {
    			if (document.getElementById('ClientScopsGrid')) {
    				clearInterval(self.setClientScopsRowClickInterval);
    				if (clientparams.clientExtraParams && clientparams.clientExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#ClientScopsGrid tbody').on('click', 'tr', self.clientScopsGridClick);
    			}
    		}, 500);
    		/////////////
    		self.addClientSecrets = function () {
    			uriExtraParams = {};
    			uriExtraParams.dialogButtonClick = self.dialogButtonClick;
    			uriExtraParams.extraAction = new common.extraAction().addClientSecrets;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Client Secret',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<clientsecretscomponent  params = {parentcontext:$context,parentdata:$data,uriExtraParams:uriExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteClientSecrets = function () {
    			if (!self.checkSelectedClientSecrets()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteClientSecrets;
    			confirmExtraParams.message = "Do you want to delete Client Secret? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Client Secret',
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
    		self.checkSelectedClientSecrets = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.clientRow.ClientSecrets())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortClientSecretsArray = function () {
    			self.currentRowId(null);
    			self.clientRow.ClientSecrets.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.clientRow.ClientSecrets().length; i++) {
    				self.clientRow.ClientSecrets()[i].selected(false);
    			}
    			self.removeSelectedFromClientSecretsGrid();
    		};
    		self.GetNewClientSecretsId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.clientRow.ClientSecrets().length; i++) {
    				if (tempId == null) {
    					tempId = self.clientRow.ClientSecrets()[i].Id();
    				}
    				else if (tempId > self.clientRow.ClientSecrets()[i].Id())
    					tempId = self.clientRow.ClientSecrets()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.clientSecretsGridClick = function () {
    			$('#ClientSecretsGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#ClientSecretsGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.clientRow.ClientSecrets().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromClientSecretsGrid = function () {
    			$('#ClientSecretsGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setClientSecretsRowClickInterval = setInterval(function () {
    			if (document.getElementById('ClientSecretsGrid')) {
    				clearInterval(self.setClientSecretsRowClickInterval);
    				if (clientparams.clientExtraParams && clientparams.clientExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#ClientSecretsGrid tbody').on('click', 'tr', self.clientSecretsGridClick);
    			}
    		}, 500);
    	    /////////////
    		self.addPermissions = function () {
    		    permissionExtraParams = {};
    		    permissionExtraParams.dialogButtonClick = self.dialogButtonClick;
    		    permissionExtraParams.extraAction = new common.extraAction().addPermission;
    		    permissionExtraParams.permissionList = self.clientRow.Permissions;
    		    var parameters = {
    		        dialogid: 'addpermissiondialog',
    		        title: 'Add Permission',
    		        dialogtype: 'info',
    		        dialogcontenttype: 'component',
    		        bodyContent: '<permissionlistcomponent  params = {parentcontext:$context,parentdata:$data,permissionExtraParams:permissionExtraParams}>',
    		        dialogparent: self,
    		        closable: true,
    		        draggable: true,
    		        size: 'wide'
    		    }
    		    self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deletePermissions = function () {
    		    if (!self.checkSelectedPermissions()) {
    		        return false;
    		    }
    		    self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    		    confirmExtraParams = {};
    		    confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    		    confirmExtraParams.extraAction = new common.extraAction().deletePermission;
    		    confirmExtraParams.message = "Do you want to delete Permission? ";
    		    var parameters = {
    		        dialogid: 'confirmdialog',
    		        title: 'Delete Permission',
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
    		self.checkSelectedPermissions = function (act) {
    		    self.currentRowId(null);
    		    self.currentName('');
    		    var msRow = Enumerable.From(self.clientRow.Permissions())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    		    if (msRow.length > 0) {
    		        self.currentRowId(msRow[0].Id());
    		        return true;
    		    }
    		    logger.error("Please Select Row");
    		    return false;
    		};
    		self.SortPermissionsArray = function () {
    		    self.currentRowId(null);
    		    self.clientRow.Permissions.sort(function (left, right) {
    		        return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    		    });
    		    for (var i = 0; i < self.clientRow.Permissions().length; i++) {
    		        self.clientRow.Permissions()[i].selected(false);
    		    }
    		    self.removeSelectedFromPermissionsGrid();
    		};
    		self.GetNewPermissionsId = function () {
    		    var tempId = null;
    		    for (var i = 0; i < self.clientRow.Permissions().length; i++) {
    		        if (tempId == null) {
    		            tempId = self.clientRow.Permissions()[i].Id();
    		        }
    		        else if (tempId > self.clientRow.Permissions()[i].Id())
    		            tempId = self.clientRow.Permissions()[i].Id()
    		    }
    		    if (tempId == null || tempId > 0)
    		        tempId = -1;
    		    else
    		        tempId = tempId - 1;

    		    return tempId;
    		};
    		self.permissionGridClick = function () {
    		    $('#PermissionGrid tbody tr').each(function () {
    		        $(this).removeClass("selected");
    		        $(this).find(('input:checkbox')[0]).prop('checked', false);
    		    });
    		    var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    		    $(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    		    $(this).toggleClass("selected");

    		    var row = $('#PermissionGrid tr.selected');
    		    rowId = row.closest('tr').attr('RowID');
    		    if (rowId) {
    		        self.clientRow.Permissions().forEach(function (lg) {
    		            if (lg.Id() == rowId) {
    		                lg.selected(true);
    		            } else {
    		                lg.selected(false);
    		            }
    		        });
    		    }
    		};
    		self.removeSelectedFromPermissionsGrid = function () {
    		    $('#PermissionGrid tbody tr').each(function () {
    		        $(this).removeClass("selected");
    		    });
    		}
    		self.setPermissionsRowClickInterval = setInterval(function () {
    		    if (document.getElementById('PermissionGrid')) {
    		        clearInterval(self.setPermissionsRowClickInterval);
    		        if (clientparams.clientExtraParams && clientparams.clientExtraParams.act == new common.action().create) {
    		            self.showContent();
    		        }
    		        $('#PermissionGrid tbody').on('click', 'tr', self.permissionGridClick);
    		    }
    		}, 500);
    		/////////////
    		self.CorsOriginGridClick = function () {
    			$('#corsOriginGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#corsOriginGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.clientRow.ClientCorsOrigins().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromCorsOriginGrid = function () {
    			$('#corsOriginGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setCorsOriginRowClickInterval = setInterval(function () {
    			if (document.getElementById('corsOriginGrid')) {
    				clearInterval(self.setCorsOriginRowClickInterval);
    				if (clientparams.clientExtraParams && clientparams.clientExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#corsOriginGrid tbody').on('click', 'tr', self.CorsOriginGridClick);
    			}
    		}, 500);
    		/////////////
    		self.URIGridClick = function () {
    			$('#URIGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#URIGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.clientRow.ClientPostLogoutRedirectUris().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromURIGrid = function () {
    			$('#URIGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setURIClickInterval = setInterval(function () {
    			if (document.getElementById('URIGrid')) {
    				clearInterval(self.setURIClickInterval);
    				if (clientparams.clientExtraParams && clientparams.clientExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#URIGrid tbody').on('click', 'tr', self.URIGridClick);
    			}
    		}, 500);
    		////////////
    		domReady(function () {
    			clientparams.clientExtraParams.parent.hideLoading();
    			if (clientparams.clientExtraParams.act == new common.action().create)
    				self.showContent();
    		});
    	}
    	return {
    	    viewModel: ClientViewModel,
    		template: template
    	}
    });