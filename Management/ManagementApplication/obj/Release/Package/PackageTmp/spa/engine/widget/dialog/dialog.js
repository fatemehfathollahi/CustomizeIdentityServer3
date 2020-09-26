define(['ko', 'jquery', 'bootstrap-dialog', 'styler'], function (ko, $, BootstrapDialog, styler) {
    styler.load('/Content/bootstrapDialog/bootstrap-dialog.css');
    styler.load('/Content/bootstrapDialog/bootstrap-dialogExtended.css');

    function customDialog(params) {
        dialogType = BootstrapDialog.TYPE_DEFAULT;
        if (params.dialogtype) {
            if (params.dialogtype == 'primary') {
                dialogType = BootstrapDialog.TYPE_PRIMARY;
            }
            if (params.dialogtype == 'info') {
                dialogType = BootstrapDialog.TYPE_INFO;
            }
            if (params.dialogtype == 'success') {
                dialogType = BootstrapDialog.TYPE_SUCCESS;
            }
            if (params.dialogtype == 'warning') {
                dialogType = BootstrapDialog.TYPE_WARNING;
            }
            if (params.dialogtype == 'danger') {
                dialogType = BootstrapDialog.TYPE_DANGER;
            }
        }

        dialogSize = BootstrapDialog.SIZE_NORMAL;
        CssClass = '';
        if (params.size) {
            if (params.size == 'small') {
                dialogSize = BootstrapDialog.SIZE_SMALL;
            } else if (params.size == 'wide') {
                dialogSize = BootstrapDialog.SIZE_WIDE;
            } else if (params.size == 'large') {
                dialogSize = BootstrapDialog.SIZE_LARGE;
            } else if (params.size == 'extra_wide') {
                CssClass = 'extra_wide';
            }
        }
        messages = '';
        if (params.dialogcontenttype == 'message') {
            messages = params.bodyContent.toString();
        }
        closable = (params.closable == false ? false : true);
        closeByBackdrop = (params.closeByBackdrop == false ? false : true);
        closeByKeyboard = (params.closeByKeyboard == false ? false : true);
        draggable = (params.draggable == true ? true : false);
        autodestroy = (params.autodestroy == false ? false : true);
        animate = (params.animate == false ? false : true);

        dialog = new BootstrapDialog({
            id: params.dialogid,
            type: dialogType,
            title: params.title,
            message: messages,
            size: dialogSize,
            closable: closable,
            closeByBackdrop: closeByBackdrop,
            closeByKeyboard: closeByKeyboard,
            draggable: draggable,
            autodestroy: autodestroy,
            cssClass: CssClass,
            animate: animate,
            onshow: function (dialogRef) {
                if (params.dialogcontenttype == 'component') {
                    $componentName = $(params.bodyContent);
                    dialogRef.getModalBody().html($componentName);
                    ko.applyBindings(params.dialogparent, $componentName[0]);
                } else if (params.dialogcontenttype == 'html') {
                    dialogRef.getModalBody().html(params.bodyContent);
                }

                if (params.onshowfunction && typeof (params.onshowfunction) == 'function') {
                    params.onshowfunction();
                }
            },
            onshown: function (dialogRef) {
                if (params.onshownfunction && typeof (params.onshownfunction) == 'function') {
                    params.onshownfunction();
                }
            },
            onhide: function (dialogRef) {
                if (params.onhidefunction && typeof (params.onhidefunction) == 'function') {
                    params.onhidefunction();
                }
            },
            onhidden: function (dialogRef) {
                if (params.onhiddenfunction && typeof (params.onhiddenfunction) == 'function') {
                    params.onhiddenfunction();
                }
            }
        });

        showdialog = (params.showdialog == false ? false : true);
        if (showdialog) {
            dialog.open();
        }

        var result = {};
        result.dialog = dialog;
        result.params = params;
        return result;
    };
    function getWaitDialog(params) {
        if (!params)
            params = {};
        var message = (params.message ? params.message : "Operation in progress, Please wait...");
        var dialogType = (params.dialogtype ? params.dialogtype : 'info');
        var title = (params.title ? params.title : 'پیام');
        var waitParameters = {
            dialogid: 'waitdialog',
            title: title,
            dialogtype: dialogType,
            dialogcontenttype: 'html',
            bodyContent: '<br/><b><i class="ace-icon fa fa-spinner fa-spin blue fa-3x"></i> '+message+' </b><br/><br/>',
            dialogparent: self,
            closable: false,
            draggable: false,
            size: 'large'
        }
        return customDialog(waitParameters);
    }

    return {
        getDialog: customDialog,
        getWaitDialog: getWaitDialog
    };
});

//create parameter and show dialog sample
//var parameters = {
//    dialogid: 'createdialog',
//    title: new app.enumerations.action().toString(params.act),
//    dialogtype: 'primary',
//    dialogcontenttype: 'component',
//    //bodyContent: data,
//    //bodyContent: message,
//    //bodyContent: 'hi',
//    bodyContent: '<personcreate  params = {dialogparent:$context,parentdata:$data}>',
//    dialogparent: self,
//    act: new app.enumerations.action().create,
//    closable: true,
//    showdialog: true,
//    size: 'wide',
//    closeByBackdrop: true,
//    closeByKeyboard: true,
//    draggable: true,
//    animate:false,
//    autodestroy:false,
//    onshowfunction: function () {
//        //alert('onshow');
//    },
//    onshownfunction: function () {
//        //alert('onshown');
//    },
//    onhidefunction: function () {
//        //alert('onhidefunction');
//    },
//    onhiddenfunction: function () {
//        //alert('onhiddenfunction');
//        mainID = 0;
//    }

//}

//objdilog = dialog.customDialog(parameters);