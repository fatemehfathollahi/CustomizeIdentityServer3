define(['jquery', 'styler', 'bootstrapValidator'], function ($, styler, bootstrapValidator) {
    styler.load('/Content/BootStrapValidator/bootstrapValidator.min.css');
    function ClearForm(formId) {
        try {
            //hides all error elements and feedback icons. All the fields are marked as not validated yet.
            $("#" + formId).data('bootstrapValidator').resetForm();
        } catch (e) {
        }
    }
    function GetValidatorObject(formId) {
        return $("#" + formId).data('bootstrapValidator');
    }
    //return Validate Object
    function GetValidate(SetFileValidator, params) {
        SetFileValidator(params);
        ClearForm(params);
        objValidator = GetValidatorObject(params.formId);
        return objValidator.validate();
    }
    //return True Or False
    function Validate(params) {
        return GetValidate(params.SetFileValidator, params).isValid();
    }
    function SetInValidateFieald(validator, message, event, field) {
        validator.updateStatus(field, 'INVALID', event)
                                             .updateMessage(field, event, message);
    }
    function SetValidateFieald(validator, event, field) {
        validator.updateStatus(field, 'VALID', event);
    }
    function SetValidatingFieald(validator, event, field) {
        validator.updateStatus(field, 'VALIDATING', event)
                           .updateMessage(field, event, 'در حال پردازش');
    }
    return {
        ClearForm: ClearForm,
        Validate: Validate,
        SetInValidateFieald: SetInValidateFieald,
        SetValidateFieald: SetValidateFieald,
        SetValidatingFieald: SetValidatingFieald
    };
});