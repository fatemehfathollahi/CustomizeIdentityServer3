define(["ko", "text!/Permision/IndexComponent", "dialog", "logger", "common", "domReady", "commonMethod", "dataTable"]
                    , function (ko, template, dialog, logger, common, domReady, commonMethod) {
                    	function PersonelViewModel(permisionParentParams) {
                    		var self = this;
                    		self.contentVisible = ko.observable(false);
                    		self.objDialog = null;
							self.Id = ko.observable();
                    		//Filter
                    		self.filterText = ko.observable("Hide Filter");
                    		self.visibleFilter = ko.observable(true);
                    		self.toggleFilter = function () {
                    			if (self.visibleFilter())
                    				self.filterText('Show Filter');
                    			else
                    				self.filterText('Hide Filter');

                    			self.visibleFilter(!self.visibleFilter());
                    		};
                    		self.clearFilter = function () {
                    			self.CardNumber('');
                    			self.EmployeeNumber('');
                    			self.FirstName('');
                    			self.FamilyName('');
                    			self.refreshMethod();
                    		};
                    		//DataTable Parameter
                    		self.mainUrl = ko.observable('/api/PermisionApi');
                    		//get current selected row id, if no row select return undefined
                    		self.currentRowId = ko.observable();
                    		//get collection of selected rows,
                    		self.selectedRows = ko.observableArray();
                    		//Parameter to refresh dataTable, when call refreshMethod, dataTable will be refresh
                    		self.refreshDataTablePage = ko.observable(false);
                    		self.refreshMethod = function () {
                    			self.refreshDataTablePage(true);
                    		};
                    		//
                    		self.dialogButtonClick = function (vm, jqEvt, result) {
                    			self.objDialog.dialog.close();
                    			if (result && result.okClick) {
                    				self.refreshMethod();
                    			}
                    		};
                            //
                    		self.addIcon = ko.observable("fa fa-plus bigger-150");
                    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
                    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
                    		self.showIcon = ko.observable("fa fa-info bigger-150");
                    		self.backToClient = function () {
                    		    self.goToParentForm();
                    		};
                    		self.addPermision = function () {
                    			self.addIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			permisionsExtraParams = {};
                    			permisionsExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			permisionsExtraParams.act = new common.action().create;
                    			var parameters = {
                    				dialogid: 'createPermissiondialog',
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
                    			self.addIcon("fa fa-plus bigger-150");
                    		};
                    		self.updatePermision = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.editIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			permisionsExtraParams = {};
                    			permisionsExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			permisionsExtraParams.act = new common.action().update;
                    			permisionsExtraParams.Id = self.currentRowId();
                    			var parameters = {
                    				dialogid: 'updatePermissiondialog',
                    				title: 'Update Permision',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<permisioncomponent  params = {parentcontext:$context,parentdata:$data,permisionsExtraParams:permisionsExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.editIcon("fa fa-pencil bigger-150");
                    		};
                    		self.deletePermision = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			permisionsExtraParams = {};
                    			permisionsExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			permisionsExtraParams.act = new common.action().delete;
                    			permisionsExtraParams.Id = self.currentRowId();
                    			var parameters = {
                    				dialogid: 'updatePermissiondialog',
                    				title: 'Delete Permision',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<permisioncomponent  params = {parentcontext:$context,parentdata:$data,permisionsExtraParams:permisionsExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.deleteIcon("fa fa-remove bigger-150");
                    		};
                    		self.showPermision = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.showIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			permisionsExtraParams = {};
                    			permisionsExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			permisionsExtraParams.act = new common.action().read;
                    			permisionsExtraParams.Id = self.currentRowId();
                    			var parameters = {
                    				dialogid: 'updatePermissiondialog',
                    				title: 'Show Permision',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<permisioncomponent  params = {parentcontext:$context,parentdata:$data,permisionsExtraParams:permisionsExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.showIcon("fa fa-info bigger-150");
                    		};
                    		domReady(function () {
                    		    permisionParentParams.permisionExtraParams.parent.hideLoading();
                    			self.contentVisible(true);
                    		});
                    	}
                    	return {
                    		viewModel: PersonelViewModel,
                    		template: template
                    	}
                    });