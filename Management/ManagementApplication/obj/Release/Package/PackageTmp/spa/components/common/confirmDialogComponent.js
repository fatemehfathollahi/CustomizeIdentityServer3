define(["ko", "text!/Common/ConfirmDialogComponent", "domReady"],
    function (ko, template, domReady) {
        function MealViewModel(confirmparams) {
            var self = this;
            self.contentVisible = ko.observable(false);
            self.enabled = ko.observable(true);
            self.enableButton = ko.observable(true);
            self.saveButtonVisible = ko.observable(true);
            self.message = ko.observable(confirmparams.confirmExtraParams.message);
            self.callParentCancel = function () {
                confirmparams.parentcontext.$data.dialogButtonClick();
            }

            self.dialogOkClick = function (vm, jqEvt) {
                result = {};
                result = confirmparams;
                result.okClick = true;
                confirmparams.parentcontext.$data.dialogButtonClick(vm, jqEvt, result);
            };
            self.dialogCancelClick = function () {
                self.callParentCancel();
            };
            domReady(function () {
                    self.contentVisible(true);
            });
        }
        return {
            viewModel: MealViewModel,
            template: template
        }
    });