define(['jquery', 'persiandate', 'numeric'], function ($, persiandate, objNumber) {
    function IsValidTime(value) {
        try {
            return (/^([0]?[0-9]|1[0-9]|2[0-3]):([0-5]\d)\s?$/i).test(value)
        } catch (e) {
            return false
        }
    }

    return {
        IsValidTime: IsValidTime
    };
});