define(['ko', 'jquery', 'styler']
            , function (ko, $, styler) {
                styler.load("/Content/morris/morris.css");

                var __hasProp = {}.hasOwnProperty;
                ko.bindingHandlers.morris = {
                    init: function (element, valueAccessor) {
                        var key, options, value, _ref;
                        options = {
                            element: element
                        };
                        _ref = ko.unwrap(valueAccessor());
                        for (key in _ref) {
                            if (!__hasProp.call(_ref, key)) continue;
                            value = _ref[key];
                            options[key] = ko.unwrap(value);
                        }
                        return element.morris = new Morris[options.type](options);
                    },
                    update: function (element, valueAccessor) {
                        var _base;
                        return typeof (_base = element.morris).setData === "function" ? _base.setData(ko.unwrap((ko.unwrap(valueAccessor())).data)) : void 0;
                    }
                };
            });