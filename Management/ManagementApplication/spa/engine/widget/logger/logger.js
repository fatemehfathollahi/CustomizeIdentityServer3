define(['toastr', 'styler'], function (toastr, styler) {
    styler.load('/Content/toastr/toastr.min.css');
    styler.load('/Content/toastr/toastr-rtl.min.css');

    function init(option) {
        if (option != undefined && option != null) {
            toastr.options = {
                "closeButton": (option.closeButton?option.closeButton:false),
                "debug": (option.debug?option.debug:true),
                "newestOnTop": (option.newestOnTop?option.newestOnTop:false),
                "progressBar": (option.progressBar==false?option.progressBar:true),
                "positionClass": (option.positionClass ? option.positionClass : "toast-bottom-full-width"),
                "preventDuplicates": (option.preventDuplicates?option.preventDuplicates:false),
                "onclick": (option.onclick?option.onclick:null),
                "showDuration": (option.showDuration?option.showDuration:"300"),
                "hideDuration": (option.hideDuration?option.hideDuration:"1000"),
                "timeOut": (option.timeOut?option.timeOut:"5000"),
                "extendedTimeOut": (option.extendedTimeOut?option.extendedTimeOut:"1000"),
                "showEasing": (option.showEasing?option.showEasing:"swing"),
                "hideEasing": (option.hideEasing ? option.hideEasing : "linear"),
                "showMethod": (option.showMethod ? option.showMethod : "fadeIn"),
                "hideMethod": (option.hideMethod ? option.hideMethod : "fadeOut")
            };
        }
        else {
            toastr.options = {
                "closeButton": false,
                "debug": true,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-bottom-full-width",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };
        }
    }

    function info(message, title, option) {
        if (option != undefined, option != null) {
            init(option);
        }
        toastr.info(message, title);
    }

    function error(message, title, option) {
        if (option != undefined, option != null) {
            init(option);
        }
        toastr.error(message, title);
    }

    function success(message, title, option) {
        if (option != undefined, option != null) {
            init(option);
        }
        toastr.success(message, title);
    }

    function clear() {
        toastr.clear();
    }

    function UnknownError(err) {
        if (err.status != undefined && err.statusText != undefined) {
            msg = err.status + " " + err.statusText + " ";
        }
        else if (err.err != undefined) {
            msg = err.err;
        }
        else if (err.text != undefined) {
            msg = err.text;
        }
        else {
            msg = err;
        }

        error(msg, "خطا");

        console.log("UnknownError Method");
        console.log(msg);
    }

    function LogError(err,showToast) {
        if (err.status != undefined && err.statusText != undefined) {
            console.log("Log Error");
            console.log(err.status);
            console.log(err.statusText);
            console.log(err);
            if (showToast) {
                error(err.statusText, "خطا");
            }
        }
        else if (err.err != undefined) {
            console.log("Log Error");
            console.log(err.err);
            console.log(err);
            if (showToast) {
                error(err.err, "خطا");
            }
        }
        else if (err.text != undefined) {
            console.log("Log Error");
            console.log(err.text);
            console.log(err);
            if (showToast) {
                error(err.text, "خطا");
            }
        }
        else {
            console.log(err);
            if (showToast) {
                error(err, "خطا");
            }
        }
    }

    function bindModelStateError(formID, validatorEventName, error) {
        ////////////////////////////////
        var response = null;
        var errors = [];
        var errorsString = "";
        //WE IGNORE MODEL STATE EXTRACTION IF THERE WAS SOME OTHER UNEXPECTED ERROR (I.E. NOT A VALIDATION ERROR)
        if (error.status == 400) {//SINCE WE ARE RETURNING BAD REQUEST STATUS FROM OUR WEB API, SO WE CHECK IF STATUS CODE IS 400
            try {
                //hengame parse agar error bedahad kolan kharej mishavad
                response = JSON.parse(error.responseText);

                var allErrorString = "";
                if (response != null) {
                    var modelState = response.ModelState;
                    //THE CODE BLOCK below IS IMPORTANT WHEN EXTRACTING MODEL STATE IN JQUERY/JAVASCRIPT
                    try {
                        for (var key in modelState) {
                            if (modelState.hasOwnProperty(key)) {
                                errorsString = (errorsString == "" ? "" : errorsString + "<br/>") + modelState[key];
                                allErrorString += errorsString;
                                errors.push(modelState[key]);//list of error messages in an array

                                $("#" + formID).data('bootstrapValidator').updateMessage(key, validatorEventName, errorsString);
                                $("#" + formID).data('bootstrapValidator').updateStatus(key, 'INVALID', validatorEventName);
                                //throw ExceptionInformation();
                            }
                        }
                    } catch (e) {
                    }

                    var intErrorCount = 0;
                    try {
                        intErrorCount = $("#" + formID).data('bootstrapValidator').getInvalidFields().length;
                    } catch (ee) {
                    }

                    if (intErrorCount == 0) {
                        //$("#errorRegion").html(allErrorString);
                        app.logger.alert.error(allErrorString, "خطا");
                    }

                    //DISPLAY THE LIST OF ERROR MESSAGES
                    if (errorsString != "") {
                        return;
                    }
                }
            }
            catch (err) { //this is not sending us a ModelState, else we would get a good responseText JSON to parse)
                console.log(err);
            }
        }
            //if error is not 400 and not for Validating
        else {
            //log error detail
            app.logger.alert.UnknownError(error);
        }
    }

    init();

    return {
        info: info,
        error: error,
        success: success,
        clear: clear,
        UnknownError: UnknownError,
        bindModelStateError: bindModelStateError,
        LogError: LogError
    };
});