define(['jquery', 'jquerymask'], function ($, jquerymask) {
    function SetDateMask() {
        $('.dateMask').mask('0000/00/00', { 'translation': { 0: { pattern: /[0-9*]/ } } });
    }

    function SetMoneyMask() {
        $('.moneyMask').mask("#,##0", {reverse:true,maxlength:false});
    }

    function SetNumericMask() {
        $('.numericMask').mask("0#");
    }
    function SetNumericTwoMask() {
        $('.twoNumericMask').mask("00", { 'translation': { 0: { pattern: /[0-9*]/ } } });
    }
    function SetNumericFiveMask() {
        $('.fiveNumericMask').mask("00000", { 'translation': { 0: { pattern: /[0-9*]/ } } });
    }
    function SetNumericFifteenMask() {
    	$('.fifteenNumericMask').mask("000000000000000", { 'translation': { 0: { pattern: /[0-9*]/ } } });
    }
    function SetNumericTenMask() {
    	$('.tenNumericMask').mask("0000000000", { 'translation': { 0: { pattern: /[0-9*]/ } } });
    }

    function RemoveMoneyMask() {
        $('.moneyMask').data('mask').remove();
    }
    function SetMealMask() {
        $('.codeMask').mask("00000", { 'translation': { 0: { pattern: /[0-9*]/ } } });

        //$('.codeMask').mask('ABCDE', {
        //    'translation': {
        //            A: { pattern: /[1-6]/ },
        //            B: { pattern: /[0-9]/ },
        //            C: { pattern: /[0-9]/ },
        //            D: { pattern: /[0-9]/ },
        //            E: { pattern: /[0-9]/ }
        //    }
        //});
    }
    function SetTimeMask() {
        $('.timeMask').mask("AB:CD", {
            'translation':
                {
                    A: { pattern: /[0-2*]/ },
                    B: { pattern: /[0-9*]/ },
                    C: { pattern: /[0-5*]/ },
                    D: { pattern: /[0-9*]/ }
                },
            placeholder: "__:__"
        });
    };

    $(function () {
        SetDateMask();
        SetMoneyMask();
        SetNumericMask();
        SetTimeMask();
        SetNumericTwoMask();
        SetMealMask();
        SetNumericFiveMask();
        SetNumericFifteenMask();
        SetNumericTenMask();
    });
    return {
        SetDateMask: SetDateMask,
        SetMoneyMask: SetMoneyMask,
        SetNumericMask: SetNumericMask,
        SetNumericTwoMask:SetNumericTwoMask,
        RemoveMoneyMask: RemoveMoneyMask,
        SetTimeMask: SetTimeMask,
        SetMealMask: SetMealMask,
        SetNumericFiveMask: SetNumericFiveMask,
        SetNumericFifteenMask: SetNumericFifteenMask,
        SetNumericTenMask: SetNumericTenMask
    };
});