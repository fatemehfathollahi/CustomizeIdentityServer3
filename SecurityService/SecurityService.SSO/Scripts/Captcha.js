(function () {

    var captchaImageID = $('#captchaImageID');
    var imgRefreshId = $('#imgRefreshId');
    var CaptchaUserInput = $('#CaptchaUserInput');

    imgRefreshId.click(function () {
        refreshCaptchaFunc();
    });

    function refreshCaptchaFunc() {
        d = new Date();
        captchaImageID.attr('src', '/MvcCaptcha/GenerateCaptcha/' + d.getTime());

        CaptchaUserInput.val('');
    }

    $('#form0').ajaxForm({
        beforeSerialize: function () {

        },
        success: function (d) {

        },
        complete: function (xhr) {

            var obj = jQuery.parseJSON(xhr.responseText);

            alert(obj.Data);
            alert("captcha string was = " + obj.captchaStr);
            alert("you entered = " + obj.CaptchaUserInput);

            refreshCaptchaFunc();
        }
    });


    $('#submit_link').click(function () {
        var userName = $('#username').val();
            $.ajax({
                url: '/RecoverPassword/RecoverCode',
                data: { 'userName': userName },
                type: "post",
                cache: false,
                success: function (rs) {
                    alert(rs.result);
                    //  $("#hdnOrigComments").val($('#txtComments').val());
                    // $('#lblCommentsNotification').text(savingStatus);
                },
                error: function (xhr, textStatus, exceptionThrown) {
                    alert(JSON.parse(xhr.responseText));
                    //$('#error').text(result.responseText);
                }
            });
     
    });

})();