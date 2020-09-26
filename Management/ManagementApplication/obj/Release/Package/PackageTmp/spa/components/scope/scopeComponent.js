define(["ko", "text!/Scope/ScopeComponent", "scopeViewModel", 'scopeValidator'
                , 'commonMethod', "common", "domReady", "logger", "dialog", "linq", "scopeClaimViewModel", "scopeSecretViewModel"],
    function (ko, template, scopeViewModel, scopeValidator, commonMethod, common,
                domReady, logger, dialog, linq, scopeClaimViewModel, scopeSecretViewModel) {
    	function ScopeViewModel(scopeparams) {
    		var self = this;
    		var formId = 'scopeForm';
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
    			window.location.href = "#/scope";
    		}
    		self.scopeRow = new scopeViewModel.scopeViewModel(ko.toJS(scopeparams.scopeExtraParams.scopeRow));

    		switch (scopeparams.scopeExtraParams.act) {
    			case new common.action().create:
    				self.headerText('Add Scope Claim');
    				break;
    			case new common.action().update:
    				self.headerText('Update Scope Claim');
    				meParams = {};
    				meParams.Id = scopeparams.scopeExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetScopeWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    			case new common.action().read:
    				self.headerText('Show Scope Claim');
    				self.enabled(false);
    				self.saveButtonVisible(false);
    				meParams = {};
    				meParams.Id = scopeparams.scopeExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetScopeWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    			case new common.action().delete:
    				self.headerText('Delete Scope Claim');
    				self.enabled(false);
    				meParams = {};
    				meParams.Id = scopeparams.scopeExtraParams.Id;
    				meParams.async = true;
    				commonMethod.GetScopeWithId(function (output, error) {
    					self.fillModel(output);
    				}, function (request, status) {
    					logger.error('Error In Loading Data');
    					self.goToParentForm();
    				}, meParams);
    				break;
    		}
    		self.fillModel = function (output) {
    			for (prop in output) {
    				if (self.scopeRow[prop] && prop != 'ScopeClaims' && prop != 'ScopeSecrets')
    					self.scopeRow[prop](output[prop]);
    			}

    			for (var i = 0; i < output.ScopeClaims.length; i++) {
    				self.scopeRow.ScopeClaims.push(
						   new scopeClaimViewModel.scopeClaimViewModel(output.ScopeClaims[i])
					   );
    			}
    			self.SortScopeClaimArray();
				for (var i = 0; i < output.ScopeSecrets.length; i++) {
    				self.scopeRow.ScopeSecrets.push(
						   new scopeSecretViewModel.scopeSecretViewModel(output.ScopeSecrets[i])
					   );
    			}
				self.SortScopeSecretsArray();
    			self.showContent();
    		};
    		self.dialogOkClick = function (vm, jqEvt) {
    			//if (!self.ValidateForm())
    			//	return false;

    			switch (scopeparams.scopeExtraParams.act) {
    				case new common.action().create: {
    					if (!self.ValidateForm()) {
    						return false;
    					}

    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.saveScope();
    						}, 400);
    					} else {
    						self.saveScope();
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
    							self.updateScope();
    						}, 400);
    					} else {
    						self.updateScope();
    					}
    					break;
    				}
    				case new common.action().delete: {
    					self.objDialog = dialog.getWaitDialog();
    					if (setTimeout) {
    						setTimeout(function () {
    							self.deleteScope();
    						}, 400);
    					} else {
    						self.deleteScope();
    					}
    					break;
    				}
    			}
    		};
    		self.saveScope = function () {
    			var prParam = {};
    			prParam.scopeRow = self.scopeRow;
				commonMethod.AddScope(function (output, error) {
    							self.objDialog.dialog.close();
    							logger.success("Operation Complete");
    							self.goToParentForm();
    						}, function (request, status) {
    							self.objDialog.dialog.close();
    							logger.error("Error in save data.");
    						}, prParam);
    		};
    		self.updateScope = function () {
    			var prParam = {};
    			prParam.scopeRow = self.scopeRow;
    			commonMethod.UpdateScope(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.goToParentForm();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.deleteScope = function () {
    			var prParam = {};
    			prParam.scopeRow = self.scopeRow;
    			commonMethod.DeleteScope(function (output, error) {
    				self.objDialog.dialog.close();
    				logger.success("Operation Complete");
    				self.goToParentForm();
    			}, function (request, status) {
    				self.objDialog.dialog.close();
    				logger.error("Error in save data.");
    			}, prParam);
    		};
    		self.ValidateForm = function () {
    			return scopeValidator.Validate(formId,self.scopeRow);
    		};
    		self.cancelClick = function () {
    			self.goToParentForm();
    		};
    		self.dialogButtonClick = function (vm, jqEvt, result) {
    			self.objDialog.dialog.close();
    			if (result && result.okClick) {
    				if (result.scopeClaimExtraParams && result.scopeClaimExtraParams.extraAction == new common.extraAction().addScopeClaim) {
    					var corRow = new scopeClaimViewModel.scopeClaimViewModel(ko.toJS(result.clientRow));
    					corRow.Id(self.GetNewScopeClaimId());
    					self.scopeRow.ScopeClaims.push(corRow);
    					self.SortScopeClaimArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteScopeClaim) {
    					self.scopeRow.ScopeClaims.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortScopeClaimArray();
    				}
    				else if (result.scopeSecretsExtraParams && result.scopeSecretsExtraParams.extraAction == new common.extraAction().addScopeSecret) {
    					var corRow = new scopeSecretViewModel.scopeSecretViewModel(ko.toJS(result.clientRow));
    					corRow.Id(self.GetNewScopeSecretsId());
    					self.scopeRow.ScopeSecrets.push(corRow);
    					self.SortScopeSecretsArray();
    				}
    				else if (result.confirmExtraParams && result.confirmExtraParams.extraAction == new common.extraAction().deleteScopeSecret) {
    					self.scopeRow.ScopeSecrets.remove(function (item) {
    						return item.Id() == self.currentRowId();
    					});
    					self.SortScopeSecretsArray();
    				}
    			}
    		};
    		self.addIcon = ko.observable("fa fa-plus bigger-150");
    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
    		self.showIcon = ko.observable("fa fa-info bigger-150");
    		/////////////
    		self.addScopeClaim = function () {
    			scopeClaimExtraParams = {};
    			scopeClaimExtraParams.dialogButtonClick = self.dialogButtonClick;
    			scopeClaimExtraParams.extraAction = new common.extraAction().addScopeClaim;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Scope Claim',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<scopeclaimcomponent  params = {parentcontext:$context,parentdata:$data,scopeClaimExtraParams:scopeClaimExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteScopeClaim = function () {
    			if (!self.checkSelectedScopeClaim()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteScopeClaim;
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
    		self.checkSelectedScopeClaim = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.scopeRow.ScopeClaims())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortScopeClaimArray = function () {
    			self.currentRowId(null);
    			self.scopeRow.ScopeClaims.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.scopeRow.ScopeClaims().length; i++) {
    				self.scopeRow.ScopeClaims()[i].selected(false);
    			}
    			self.removeSelectedFromScopeClaimGrid();
    		};
    		self.GetNewScopeClaimId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.scopeRow.ScopeClaims().length; i++) {
    				if (tempId == null) {
    					tempId = self.scopeRow.ScopeClaims()[i].Id();
    				}
    				else if (tempId > self.scopeRow.ScopeClaims()[i].Id())
    					tempId = self.scopeRow.ScopeClaims()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.ScopeClaimGridClick = function () {
    			$('#scopeClaimGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#scopeClaimGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.scopeRow.ScopeClaims().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromScopeClaimGrid = function () {
    			$('#scopeClaimGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setScopeClaimRowClickInterval = setInterval(function () {
    			if (document.getElementById('scopeClaimGrid')) {
    				clearInterval(self.setScopeClaimRowClickInterval);
    				if (scopeparams.scopeExtraParams && scopeparams.scopeExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#scopeClaimGrid tbody').on('click', 'tr', self.ScopeClaimGridClick);
    			}
    		}, 500);
    		/////////////
    		self.addScopeSecrets = function () {
    			scopeSecretsExtraParams = {};
    			scopeSecretsExtraParams.dialogButtonClick = self.dialogButtonClick;
    			scopeSecretsExtraParams.extraAction = new common.extraAction().addScopeSecret;
    			var parameters = {
    				dialogid: 'createmealListdialog',
    				title: 'Add Scope Secret',
    				dialogtype: 'info',
    				dialogcontenttype: 'component',
    				bodyContent: '<scopesecretcomponent  params = {parentcontext:$context,parentdata:$data,scopeSecretsExtraParams:scopeSecretsExtraParams}>',
    				dialogparent: self,
    				closable: true,
    				draggable: true,
    				size: 'wide'
    			}
    			self.objDialog = dialog.getDialog(parameters);
    		};
    		self.deleteScopeSecrets = function () {
    			if (!self.checkSelectedScopeSecrets()) {
    				return false;
    			}
    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
    			confirmExtraParams = {};
    			confirmExtraParams.dialogButtonClick = self.dialogButtonClick;
    			confirmExtraParams.extraAction = new common.extraAction().deleteScopeSecret;
    			confirmExtraParams.message = "Do you want to delete Scope Secret? ";
    			var parameters = {
    				dialogid: 'confirmdialog',
    				title: 'Delete Scope Secret',
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
    		self.checkSelectedScopeSecrets = function (act) {
    			self.currentRowId(null);
    			self.currentName('');
    			var msRow = Enumerable.From(self.scopeRow.ScopeSecrets())
                        .Where(function (x) { return x.selected() })
                        .ToArray();

    			if (msRow.length > 0) {
    				self.currentRowId(msRow[0].Id());
    				return true;
    			}
    			logger.error("Please Select Row");
    			return false;
    		};
    		self.SortScopeSecretsArray = function () {
    			self.currentRowId(null);
    			self.scopeRow.ScopeSecrets.sort(function (left, right) {
    				return left.Id() == right.Id() ? 0 : (left.Id() < right.Id() ? -1 : 1)
    			});
    			for (var i = 0; i < self.scopeRow.ScopeSecrets().length; i++) {
    				self.scopeRow.ScopeSecrets()[i].selected(false);
    			}
    			self.removeSelectedFromScopeSecretsGrid();
    		};
    		self.GetNewScopeSecretsId = function () {
    			var tempId = null;
    			for (var i = 0; i < self.scopeRow.ScopeSecrets().length; i++) {
    				if (tempId == null) {
    					tempId = self.scopeRow.ScopeSecrets()[i].Id();
    				}
    				else if (tempId > self.scopeRow.ScopeSecrets()[i].Id())
    					tempId = self.scopeRow.ScopeSecrets()[i].Id()
    			}
    			if (tempId == null || tempId > 0)
    				tempId = -1;
    			else
    				tempId = tempId - 1;

    			return tempId;
    		};
    		self.ScopeSecretsGridClick = function () {
    			$('#scopeSecretsGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    				$(this).find(('input:checkbox')[0]).prop('checked', false);
    			});
    			var checkStatus = $(this).find(('input:checkbox')[0]).prop('checked');
    			$(this).find(('input:checkbox')[0]).prop('checked', !checkStatus);
    			$(this).toggleClass("selected");

    			var row = $('#scopeSecretsGrid tr.selected');
    			rowId = row.closest('tr').attr('RowID');
    			if (rowId) {
    				self.scopeRow.ScopeSecrets().forEach(function (lg) {
    					if (lg.Id() == rowId) {
    						lg.selected(true);
    					} else {
    						lg.selected(false);
    					}
    				});
    			}
    		};
    		self.removeSelectedFromScopeSecretsGrid = function () {
    			$('#scopeSecretsGrid tbody tr').each(function () {
    				$(this).removeClass("selected");
    			});
    		}
    		self.setScopeSecretsRowClickInterval = setInterval(function () {
    			if (document.getElementById('scopeSecretsGrid')) {
    				clearInterval(self.setScopeSecretsRowClickInterval);
    				if (scopeparams.scopeExtraParams && scopeparams.scopeExtraParams.act == new common.action().create) {
    					self.showContent();
    				}
    				$('#scopeSecretsGrid tbody').on('click', 'tr', self.ScopeSecretsGridClick);
    			}
    		}, 500);
    		/////////////
    		domReady(function () {
    			scopeparams.scopeExtraParams.parent.hideLoading();
    			if (scopeparams.scopeExtraParams.act == new common.action().create)
    				self.showContent();
    		});
    	}
    	return {
    		viewModel: ScopeViewModel,
    		template: template
    	}
    });