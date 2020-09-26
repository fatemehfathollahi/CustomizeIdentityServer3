define(['jquery', 'bootstrapValidator', 'validator', 'numeric', 'time', 'commonMethod'],
    function ($, bootstrapValidator, basevalidator, numeric, time, commonMethod) {
    	function ClearForm(formId) {
    		basevalidator.ClearForm(formId);
    	}

    	function Validate(formId, model) {
    		params = {
    			formId: formId,
    			model: model,
    			SetFileValidator: SetFileValidator
    		};
    		return basevalidator.Validate(params);
    	}

    	function SetFileValidator(params) {
    		var formId = params.formId;
    		$("#" + formId).bootstrapValidator({
    			message: 'Field is not vaid',
    			exclude: [':disabled', ':hidden', ':not(:visible)'],
    			feedbackIcons: {
    				valid: 'fa fa-check',
    				invalid: 'fa fa-times',
    				validating: 'fa fa-refresh'
    			},
    			fields: {
    				Origin: {
    					validators: {
    						notEmpty: {
    							message: 'Origin is required'
    						}
    					}
    				}
    			}
    		});
    	}
    	return {
    		ClearForm: ClearForm,
    		Validate: Validate
    	}
    });