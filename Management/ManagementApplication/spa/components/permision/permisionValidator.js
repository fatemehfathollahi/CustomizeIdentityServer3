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
    				Name: {
    					validators: {
    						notEmpty: {
    							message: 'Name is required'
    						}, callback: {
                                message: '',
                                callback: function (value, validator, $field) {
                                    if (value == "" || value == undefined)
                                        return true;

                                    var outputValue = true;
                                    var outputMessage = '';

                                    var meParams = {};
                                    meParams.Id = params.model.Id();
                                    meParams.Name = value;
                                    meParams.async = false;
                                    commonMethod.ExistsPermissionName(function (output, error) {
                                        if (output) {
                                            outputValue = false;
                                            outputMessage = 'Name is duplicate';
                                        }
                                        return {
                                            valid: outputValue,
                                            message: outputMessage
                                        }
                                    }, function (request, status) {
                                        outputValue = false;
                                        outputMessage = 'Error in reading data';
                                    }, meParams);
                                    return {
                                        valid: outputValue,
                                        message: outputMessage
                                    }
                                }
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