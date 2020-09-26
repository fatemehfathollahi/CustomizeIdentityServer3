define(["ko", "text!/Scope/IndexComponent", "dialog", "logger", "common", "domReady", "commonMethod", "dataTable"]
                    , function (ko, template, dialog, logger, common, domReady, commonMethod) {
                    	function PersonelViewModel(ScopeParentParams) {
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
                    		self.mainUrl = ko.observable('/api/ScopeApi');
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
                    		self.addIcon = ko.observable("fa fa-plus bigger-150");
                    		self.editIcon = ko.observable("fa fa-pencil bigger-150");
                    		self.deleteIcon = ko.observable("fa fa-remove bigger-150");
                    		self.showIcon = ko.observable("fa fa-info bigger-150");

                    		self.addScope = function () {
                    			self.addIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			window.location.href = "#/scope/add/";
                    		};
                    		self.updateScope = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.editIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			window.location.href = "#/scope/edit/" + self.currentRowId();
                    		};
                    		self.deleteScope = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.deleteIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			window.location.href = "#/scope/delete/" + self.currentRowId();
                    		};
                    		self.showScope = function () {
                    			if (!self.currentRowId()) {
                    				logger.error("Please Select Row");
                    				throw new Error("");
                    			}
                    			self.showIcon("ace-icon fa fa-spinner fa-spin bigger-125");
                    			window.location.href = "#/scope/view/" + self.currentRowId();
                    		};
                    		domReady(function () {
                    			ScopeParentParams.scopeExtraParams.parent.hideLoading();
                    			self.contentVisible(true);
                    		});
                    	}
                    	return {
                    		viewModel: PersonelViewModel,
                    		template: template
                    	}
                    });