define(['jquery', 'persiandate', 'numeric'], function ($, persiandate, objNumber) {
    function IsValidPersianDate(value) {
        blnResult = true;
        try {
            if (value == undefined
                || value == '') {
                return false;
            };
            if (value.length != 10) {
                return false;
            };
            if (value.split("/").length != 3) {
                return false;
            }
            if (value[4] !== '/') {
                return false;
            }
            if (value[7] !== '/') {
                return false;
            }
            objValue = value.toString().replace('/', '').replace('/', '').replace('-', '').replace('-', '');
            if (objValue.length != 8) {
                return false;
            };
            try {
                if (!objNumber.isNumeric(objValue.substring(0, 4))
                    || !objNumber.isNumeric(objValue.substring(4, 6))
                    || !objNumber.isNumeric(objValue.substring(6, 8))) {
                    return false;
                }
                objYear = Number(objValue.substring(0, 4));
                objMonth = Number(objValue.substring(4, 6));
                objDay = Number(objValue.substring(6, 8));
            } catch (e) {
                return false;
            }
            if (!objYear) {
                return false;
            }
            if (!(objMonth && objMonth >= 1 && objMonth <= 12)) {
                return false;
            }
            if (!(objDay && objDay >= 1 && objDay <= 31)) {
                return false;
            }
            switch (objMonth) {
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    if (!(objDay >= 1 && objDay <= 30)) {
                        return false;
                    }
                    break;
                case 12:
                    if (persianDate([objYear]).isLeapYear()) {
                        if (!(objDay >= 1 && objDay <= 30)) {
                            return false;
                        }
                    }
                    else {
                        if (!(objDay >= 1 && objDay <= 29)) {
                            return false;
                        }
                    }
                    break;
            }
        } catch (e) {
            return false
        }
        return blnResult;
    }
    function GetCurrentPersianDateWithSlash() {
        result = persianDate(new Date()).format('YYYY/MM/DD');
        return result;
    }
    function GetCurrentPersianDate() {
        result = persianDate(new Date()).format('YYYYMMDD');
        return result;
    }

    function CompareTwoPersianDate(firstDate, secondDate) {
        return (firstDate).localeCompare(secondDate);
    }
    function LessThanEqualPersianDate(firstDate, secondDate) {
        objCompare = (firstDate).localeCompare(secondDate);
        if (objCompare == -1 || objCompare == 0) {
            return true;
        } else {
            return false;
        }
    }
    return {
        IsValidPersianDate: IsValidPersianDate,
        GetCurrentPersianDate: GetCurrentPersianDate,
        GetCurrentPersianDateWithSlash: GetCurrentPersianDateWithSlash,
        CompareTwoPersianDate: CompareTwoPersianDate,
        LessThanEqualPersianDate: LessThanEqualPersianDate
    };
});