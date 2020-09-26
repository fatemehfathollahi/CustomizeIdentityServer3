define(['jquery'], function ($) {
    function getMoneyFromMaskData(value) {
        if (value == null || value == undefined)
            return false;
        result = value.toString().replace(new RegExp(',', 'g'), '').replace(new RegExp(' ', 'g'), '')
        if (isNumeric(result)) {
            return result;
        }
        else {
            return undefined;
        }
    }
    function isValidMoney(value) {
        if (value == undefined || value == '') {
            return false;
        };
        objValue = getMoneyFromMaskData(value);
        if (isNumeric(objValue)) {
            return true;
        }
        else {
            return false;
        }
    }
    function isNumeric(data) {
        try {
            if (data == null || data == undefined)
                return false;

            return parseFloat(data) == data && isFinite(data);
        } catch (e) {
            return false;
        }
    }
    return {
        isValidMoney: isValidMoney,
        isNumeric: isNumeric,
        getMoneyFromMaskData: getMoneyFromMaskData
    };
});