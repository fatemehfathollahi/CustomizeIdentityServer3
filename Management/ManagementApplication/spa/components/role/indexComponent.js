define(["ko", "text!/Role/IndexComponent", "dialog", "logger", "common", "domReady", "commonMethod", "dataTable"]
                    , function (ko, template, dialog, logger, common, domReady, commonMethod) {
                    	function PersonelViewModel(RoleParentParams) {
                    		var self = this;
                    		self.contentVisible = ko.observable(false);
                    		self.objDialog = null;
                    		self.ClientId = ko.observable(RoleParentParams.roleExtraParams.ClientId);
                    		self.clientIdText = ko.observable();
                    		self.clientName = ko.observable();
                    	    //
                    		self.goToParentForm = function () {
                    		    window.location.href = "#/client";
                    		}
                    		if (!RoleParentParams.roleExtraParams.ClientId || RoleParentParams.roleExtraParams.ClientId == 0)
                    		{
                    		    logger.error("Error in loading Client information");
                    		    self.goToParentForm();
                    		}
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
                    		self.mainUrl = ko.observable('/api/RoleApi');
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

                    		meParams = {};
                    		meParams.Id = self.ClientId();
                    		meParams.async = true;
                    		commonMethod.GetClientWithId(function (output, error) {
                    			self.clientIdText(output.ClientId);
                    		    self.clientName(output.ClientName);
                    		}, function (request, status) {
                    		    logger.error('Error In Loading Data');
                    		    self.goToParentForm();
                    		}, meParams);
                            //
                    		self.addIcon = ko.observable("fa fa-plus bigger-150");
                    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
                    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
                    		self.showIcon = ko.observable("fa fa-info bigger-150");
                    		self.backIcon = ko.observable("fa fa-step-backward bigger-150");
                    		self.backToClient = function () {
                    		    self.goToParentForm();
                    		};
                    		self.addRole = function () {
                    			self.addIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			roleExtraParams = {};
                    			roleExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			roleExtraParams.act = new common.action().create;
                    			roleExtraParams.ClientId = self.ClientId();
                    			var parameters = {
                    				dialogid: 'createRoledialog',
                    				title: 'Add Role',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<rolecomponent  params = {parentcontext:$context,parentdata:$data,roleExtraParams:roleExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.addIcon("fa fa-plus bigger-150");
                    		};
                    		self.updateRole = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.editIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			roleExtraParams = {};
                    			roleExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			roleExtraParams.act = new common.action().update;
                    			roleExtraParams.Id = self.currentRowId();
                    			roleExtraParams.ClientId = self.ClientId();
                    			var parameters = {
                    				dialogid: 'createRoledialog',
                    				title: 'Update Role',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<rolecomponent  params = {parentcontext:$context,parentdata:$data,roleExtraParams:roleExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.editIcon("fa fa-pencil bigger-150");
                    		};
                    		self.deleteRole = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			roleExtraParams = {};
                    			roleExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			roleExtraParams.act = new common.action().delete;
                    			roleExtraParams.Id = self.currentRowId();
                    			roleExtraParams.ClientId = self.ClientId();
                    			var parameters = {
                    				dialogid: 'createRoledialog',
                    				title: 'Delete Role',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<rolecomponent  params = {parentcontext:$context,parentdata:$data,roleExtraParams:roleExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.deleteIcon("fa fa-remove bigger-150");
                    		};
                    		self.showRole = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.showIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			roleExtraParams = {};
                    			roleExtraParams.dialogButtonClick = self.dialogButtonClick;
                    			roleExtraParams.act = new common.action().read;
                    			roleExtraParams.Id = self.currentRowId();
                    			roleExtraParams.ClientId = self.ClientId();
                    			var parameters = {
                    				dialogid: 'createRoledialog',
                    				title: 'View Role',
                    				dialogtype: 'info',
                    				dialogcontenttype: 'component',
                    				bodyContent: '<rolecomponent  params = {parentcontext:$context,parentdata:$data,roleExtraParams:roleExtraParams}>',
                    				dialogparent: self,
                    				closable: true,
                    				draggable: true,
                    				size: 'wide'
                    			}
                    			self.objDialog = dialog.getDialog(parameters);
                    			self.showIcon("fa fa-remove bigger-150");
                    		};
                    		domReady(function () {
                    		    RoleParentParams.roleExtraParams.parent.hideLoading();
                    			self.contentVisible(true);
                    		});
                    	}
                    	return {
                    		viewModel: PersonelViewModel,
                    		template: template
                    	}
                    });