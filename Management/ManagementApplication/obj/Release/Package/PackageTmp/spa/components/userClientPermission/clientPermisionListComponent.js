define(["ko", "text!/UserClientPermission/UserClientPermissionListComponent", "userClientPermissionViewModel"
                , 'commonMethod', "common", "domReady", "logger", "linq"],
    function (ko, template, userClientPermissionViewModel, commonMethod, common, domReady, logger, linq) {
        function PermissionListViewModel(permissionListparams) {
            var self = this;
            self.PermissionList = ko.observableArray([]);
            self.loadingVisible = ko.observable(true);
            self.contentVisible = ko.observable(false);
            self.enabled = ko.observable(true);
            self.enableButton = ko.observable(true);
            self.saveButtonVisible = ko.observable(true);
            self.tableHeight = ko.observable('100%');
            self.permissionList = ko.observableArray();
            self.SetContentWidth = function () {
            	try {
            		if (screen.height && $("#PermissionListGrid").height()) {
            			var scrHeight = screen.height * 40 / 100;
            			var tblHeight = $("#PermissionListGrid").height();
            			if (tblHeight > scrHeight)
            				self.tableHeight(scrHeight + "px");
            		}
            	}
            	catch (err) {
            	}
            };

            self.callParentCancel = function () {
            	permissionListparams.permissionExtraParams.dialogButtonClick();
            }

            commonMethod.GetClientPermissionList(function (output) {
                if (output) {
                	output.forEach(function (pl) {
                		var t = new userClientPermissionViewModel.userClientPermissionViewModel(ko.toJS(pl));
                    	self.permissionList.push(t);
                    });
                	if (permissionListparams.permissionExtraParams.permissionList && permissionListparams.permissionExtraParams.permissionList().length > 0) {
                		permissionListparams.permissionExtraParams.permissionList().forEach(function (pl) {
                    		var msRow = Enumerable.From(self.permissionList())
									.Where(function (x) { return x.Client_Id() == pl.Client_Id() && x.Permission_Id() == pl.Permission_Id() })
									.ToArray();			 
									if(msRow.length>0)
                    					msRow[0].selected(true);
                		});
                		
                	}
                	self.SetContentWidth();
                	self.changeSelectGridRowColor();
                }
            }, function (request) {
                logger.error('Error in reading data.');
                self.callParentCancel();
            });
            self.dialogOkClick = function (vm, jqEvt) {
                var result = {};
                result = permissionListparams;
                result.okClick = true;
                result.permissionList = self.getSelectedList();
                permissionListparams.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
            };
            self.dialogCancelClick = function () {
                self.callParentCancel();
            };
            self.permissionListGridClick = function () {
                var checkStatus = $(this).find('input:checkbox').prop('checked');
                $(this).find('input:checkbox').prop('checked', !checkStatus);
                $(this).toggleClass("selected");
                var rowId = $(this).closest('tr').attr('RowID');
                if (rowId) {
                	for (var i = 0; i < self.permissionList().length; i++) {
                		if (self.permissionList()[i].Id() == rowId) {
                			self.permissionList()[i].selected(!checkStatus);
                            break;
                        }
                    }
                }
            };
            self.getSelectedList = function (selectedList) {
                var selectedMealList = ko.observableArray([]);
                self.permissionList().forEach(function (lg) {
                    if (lg.selected() == true) {
                        selectedMealList.push(lg);
                    }
                });
                return selectedMealList;
            };
            self.changeSelectGridRowColor = function () {
            	$('#PermissionListGrid tbody tr').each(function () {
            		var rowId = $(this).attr('RowID');
            		var msRow = Enumerable.From(self.permissionList())
						.Where(function (x) { return x.Id() == rowId && x.selected() })
						.ToArray();
            		if (msRow.length > 0) {
            			$(this).toggleClass("selected");
            		}
            	});
            };
            self.setPermissionListRowClickInterval = setInterval(function () {
            	if (document.getElementById('PermissionListGrid')) {
            		self.SetContentWidth();
                    clearInterval(self.setPermissionListRowClickInterval);
                    self.loadingVisible(false);
                    self.contentVisible(true);
                    $('#PermissionListGrid tbody').on('click', 'tr', self.permissionListGridClick);

                    var row = $('#PermissionListGrid tbody tr');
                    self.changeSelectGridRowColor();
                }
            }, 500);
        }
        return {
        	viewModel: PermissionListViewModel,
            template: template
        }
    });