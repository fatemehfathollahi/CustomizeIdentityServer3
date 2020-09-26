define(['ko', 'jquery', 'styler']
            , function (ko, $, styler) {
                styler.load("/Content/bootstrapDatePicker/bootstrap-datepicker.min.css");
                ko.bindingHandlers.datepicker = {
                    init: function (element, valueAccessor, allBinding, viewModel, bindingContext) {
                        $(element).datepicker(
                          {
                              isRTL: true,
                              dateFormat: "yy/mm/dd",
                              selectOtherMonths: true,
                              selectOtherYear: true,
                              showButtonPanel: true
                          });
                    }
                };
            });