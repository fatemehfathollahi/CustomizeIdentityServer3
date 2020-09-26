define(["jquery", "ko", "domReady", "text!/home/manager", "router", "main", "common", "commonMethod", "logger", "commonMask"],
            function ($, ko, domReady, template, router, main, common, commonMethod, logger) {
            	function managerViewModel() {
            		var self = this;
            		self.loadingVisible = ko.observable(true);
            		domReady(function () {
            			MINOVATE.initialize();
            		});
            		//var self = this;
            		self.deactiveMenu = function () {
            			$("#homeLink").removeClass("active");
            			$("#BasicOperationMenu").removeClass("active");
            			$("#MainOperationMenu").removeClass("active");
            			$("#client").removeClass("active");
            			$("#scope").removeClass("active");
            			$("#user").removeClass("active");
            			$("#permission").removeClass("active");
            		};
            		self.showLoading = function () {
            			self.loadingVisible(true);
            		}
            		self.hideLoading = function () {
            			self.loadingVisible(false);
            		}
            		self.autoHideLoading = function () {
            			setTimeout(function () {
            				self.loadingVisible(false);
            			}, 3000);
            		}
            		self.currentComponent = ko.observable({ name: "homecomponent", params: { parent: self } })
            		router.addRoute('{page}', function (page) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (page) {
            				case 'home':
            					$("#homeLink").addClass("active");
            					self.currentComponent({ name: "homecomponent", params: { parent: self } });
            					break;
            				case 'client':
            					$("#BasicOperationMenu").addClass("active");
            					$("#client").addClass("active");
            					var clientExtraParams = {};
            					clientExtraParams.parent = self;
            					self.currentComponent({ name: "clinetindexcomponent", params: { clientExtraParams: clientExtraParams } });
            					break;
            				case 'scope':
            					$("#BasicOperationMenu").addClass("active");
            					$("#scope").addClass("active");
            					var scopeExtraParams = {};
            					scopeExtraParams.parent = self;
            					self.currentComponent({ name: "scopeindexcomponent", params: { scopeExtraParams: scopeExtraParams } });
            					break;
							case 'user':
            					$("#BasicOperationMenu").addClass("active");
            					$("#user").addClass("active");
            					var userExtraParams = {};
            					userExtraParams.parent = self;
            					self.currentComponent({ name: "userindexcomponent", params: { userExtraParams: userExtraParams } });
            					break;
							case 'permission':
            					$("#BasicOperationMenu").addClass("active");
            					$("#permission").addClass("active");
            					var permisionExtraParams = {};
            					permisionExtraParams.parent = self;
            					self.currentComponent({ name: "permissionindexcomponent", params: { permisionExtraParams: permisionExtraParams } });
            					break;
            			}
            		});
            		router.addRoute('client/{action}', function (action) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (action) {
            				case 'add':
            					$("#BasicOperationMenu").addClass("active");
            					$("#client").addClass("active");
            					var clientExtraParams = {};
            					clientExtraParams.parent = self;
            					clientExtraParams.act = new common.action().create;
            					clientExtraParams.Id = 0;
            					self.currentComponent({ name: "clinetcomponent", params: { clientExtraParams: clientExtraParams } });
            					break;
            			}
            		});
            		router.addRoute('scope/{action}', function (action) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (action) {
            				case 'add':
            					$("#BasicOperationMenu").addClass("active");
            					$("#scope").addClass("active");
            					var scopeExtraParams = {};
            					scopeExtraParams.parent = self;
            					scopeExtraParams.act = new common.action().create;
            					scopeExtraParams.Id = 0;
            					self.currentComponent({ name: "scopecomponent", params: { scopeExtraParams: scopeExtraParams } });
            					break;
            			}
            		});
            		router.addRoute('user/{action}', function (action) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (action) {
            				case 'add':
            					$("#BasicOperationMenu").addClass("active");
            					$("#user").addClass("active");
            					var userExtraParams = {};
            					userExtraParams.parent = self;
            					userExtraParams.act = new common.action().create;
            					userExtraParams.Id = 0;
            					self.currentComponent({ name: "usercomponent", params: { userExtraParams: userExtraParams } });
            					break;
            			}
            		});
            		router.addRoute('role/{id}', function (id) {
            		    self.showLoading();
            		    self.deactiveMenu();
            		    $("#BasicOperationMenu").addClass("active");
            		    $("#client").addClass("active");
            		    var roleExtraParams = {};
            		    roleExtraParams.parent = self;
            		    roleExtraParams.act = new common.action().create;
            		    roleExtraParams.ClientId = id;
            		    self.currentComponent({ name: "roleindexcomponent", params: { roleExtraParams: roleExtraParams } });
            		    
            		});
            		router.addRoute('client/{action}/{id}', function (action, id) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (action) {
            				case 'edit':
            					$("#BasicOperationMenu").addClass("active");
            					$("#client").addClass("active");
            					var clientExtraParams = {};
            					clientExtraParams.parent = self;
            					clientExtraParams.act = new common.action().update;
            					clientExtraParams.Id = id;
            					self.currentComponent({ name: "clinetcomponent", params: { clientExtraParams: clientExtraParams } });
            					break;
            				case 'view':
            					$("#BasicOperationMenu").addClass("active");
            					$("#client").addClass("active");
            					var clientExtraParams = {};
            					clientExtraParams.parent = self;
            					clientExtraParams.act = new common.action().read;
            					clientExtraParams.Id = id;
            					self.currentComponent({ name: "clinetcomponent", params: { clientExtraParams: clientExtraParams } });
            					break;
            				case 'delete':
            					$("#BasicOperationMenu").addClass("active");
            					$("#client").addClass("active");
            					var clientExtraParams = {};
            					clientExtraParams.parent = self;
            					clientExtraParams.act = new common.action().delete;
            					clientExtraParams.Id = id;
            					self.currentComponent({ name: "clinetcomponent", params: { clientExtraParams: clientExtraParams } });
            					break;
            			}
            		});
            		router.addRoute('scope/{action}/{id}', function (action, id) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (action) {
            				case 'edit':
            					$("#BasicOperationMenu").addClass("active");
            					$("#scope").addClass("active");
            					var scopeExtraParams = {};
            					scopeExtraParams.parent = self;
            					scopeExtraParams.act = new common.action().update;
            					scopeExtraParams.Id = id;
            					self.currentComponent({ name: "scopecomponent", params: { scopeExtraParams: scopeExtraParams } });
            					break;
            				case 'view':
            					$("#BasicOperationMenu").addClass("active");
            					$("#scope").addClass("active");
            					var scopeExtraParams = {};
            					scopeExtraParams.parent = self;
            					scopeExtraParams.act = new common.action().read;
            					scopeExtraParams.Id = id;
            					self.currentComponent({ name: "scopecomponent", params: { scopeExtraParams: scopeExtraParams } });
            					break;
            				case 'delete':
            					$("#BasicOperationMenu").addClass("active");
            					$("#scope").addClass("active");
            					var scopeExtraParams = {};
            					scopeExtraParams.parent = self;
            					scopeExtraParams.act = new common.action().delete;
            					scopeExtraParams.Id = id;
            					self.currentComponent({ name: "scopecomponent", params: { scopeExtraParams: scopeExtraParams } });
            					break;
            			}
            		});
            		router.addRoute('user/{action}/{id}', function (action, id) {
            			self.showLoading();
            			self.deactiveMenu();
            			switch (action) {
            				case 'edit':
            					$("#BasicOperationMenu").addClass("active");
            					$("#user").addClass("active");
            					var userExtraParams = {};
            					userExtraParams.parent = self;
            					userExtraParams.act = new common.action().update;
            					userExtraParams.Id = id;
            					self.currentComponent({ name: "usercomponent", params: { userExtraParams: userExtraParams } });
            					break;
            				case 'view':
            					$("#BasicOperationMenu").addClass("active");
            					$("#user").addClass("active");
            					var userExtraParams = {};
            					userExtraParams.parent = self;
            					userExtraParams.act = new common.action().read;
            					userExtraParams.Id = id;
            					self.currentComponent({ name: "usercomponent", params: { userExtraParams: userExtraParams } });
            					break;
            				case 'delete':
            					$("#BasicOperationMenu").addClass("active");
            					$("#user").addClass("active");
            					var userExtraParams = {};
            					userExtraParams.parent = self;
            					userExtraParams.act = new common.action().delete;
            					userExtraParams.Id = id;
            					self.currentComponent({ name: "usercomponent", params: { userExtraParams: userExtraParams } });
            					break;
            			}
            		});
            		router.start();
            	}

            	return {
            		viewModel: managerViewModel
					, template: template
            	}
            });