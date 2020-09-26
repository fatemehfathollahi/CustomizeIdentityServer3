﻿(function () {
    requirejs.onError = function (err) {
        console.log(err.requireType);
        if (err.requireType === 'timeout') {
            console.log('modules: ' + err.requireModules);
        }
        //    else {
        //    throw err;
        //}
        //window.location.href = "#/home";
        console.log(err);
        //alert('خطا در لود داده های سیستم، لطفا صفحه مرورگر خود را باز نگه داشته و با مدیر سیستم تماس بگیرید', 'خطا');
    };

    require.config({
        urlArgs: { 'bust': (new Date()).getTime() },
        waitSeconds: 0,
        paths: {
        	'text': '/spa/engine/lib/text.min',
        	'domReady': '/spa/engine/lib/domReady.min',
            'jquery': '/Scripts/jquery/jquery-2.2.4.min',
            'bootstrap': '/Scripts/bootstrap/bootstrap.min',
            'jRespond': '/Scripts/jRespond/jRespond.min',
            'jquery.sparkline': '/Scripts/sparkline/jquery.sparkline.min',
            'jquery.slimscroll': '/Scripts/slimscroll/jquery.slimscroll.min',
            'jquery.animsition': '/Scripts/animsition/js/jquery.animsition.min',
            'ko': '/spa/engine/lib/knockout-3.4.0',
            'crossroads': '/spa/engine/lib/Crossroads',
            'styler': '/spa/engine/infrastructure/styler/styler.min',
            'router': '/spa/engine/infrastructure/router/router.min',
            'modernizr': '/Scripts/modernizr-2.8.3-respond-1.4.2.min',
            'main': '/Scripts/main',
            'screenfull': '/assets/js/vendor/screenfull/screenfull.min',
            'parsley': '/assets/js/vendor/parsley/parsley.min',
            'jquery.bootstrap.wizard': '/assets/js/vendor/form-wizard/jquery.bootstrap.wizard.min',
            'bootstrap-dialog': '/Scripts/bootstrapDialog/bootstrap-dialog',
            'dialog': '/spa/engine/widget/dialog/dialog.min',
            'toastr': '/Scripts/toastr/toastr.min',
            'logger': '/spa/engine/widget/logger/logger.min',
            'bootstrapDatepicker': '/Scripts/bootstrapDatePicker/bootstrap-datepicker.min',
            'bootstrapDatepickerFa': '/Scripts/bootstrapDatePicker/bootstrap-datepicker.fa.min',
            'datepicker': '/spa/engine/widget/datepicker/datepicker.min',
            'jquerymask': '/Scripts/jquerymask/jquery.mask.min',
            'commonMask': '/spa/engine/infrastructure/mask/commonMask.min',
            'bootstrapValidator': '/Scripts/BootStrapValidator/bootstrapValidator.min',
            'validator': '/spa/engine/infrastructure/validator/validator.min',
            'numeric': '/spa/engine/infrastructure/common/numeric.min',
            'persiandate': '/Scripts/date/persian-date-0.1.8.min',
            'date': '/spa/engine/infrastructure/common/date.min',
            'time': '/spa/engine/infrastructure/common/time.min',
            'raphael': '/Scripts/raphael/raphael-min',
            'morris': '/Scripts/morris/morris.min',
            'knockout-morris': '/spa/engine/widget/morris/knockout-morris.min',
            'select2.full': '/Scripts/select/select2.min',
            'knockout-select2': '/spa/engine/widget/select2/knockout-select2.min',
            'commonMethod': '/spa/components/common/commonMethod.min',
            'jqueryDataTables': '/Scripts/jquery.dataTable/jquery.dataTables',
            'dataTablesBootstrap': '/Scripts/jquery.dataTable/dataTables.bootstrap',
            'dataTable': '/spa/engine/widget/dataTable/datatable.min',
            'common': '/spa/components/common/common.min',
            'komapping': '/spa/engine/lib/knockout.mapping',
            'linq': '/Scripts/linq/linq.min',
            'fileBindings': '/spa/engine/widget/fileUploader/knockout-file-upload.min',
            'foodViewModel': '/spa/components/food/foodViewModel.min',
            'foodValidator': '/spa/components/food/foodValidator.min',
            'foodTypeViewModel': '/spa/components/FoodTypes/foodTypeViewModel.min',
            'foodTypeValidator': '/spa/components/foodTypes/foodTypeValidator.min',
            'mealViewModel': '/spa/components/meal/MealViewModel.min',
            'mealValidator': '/spa/components/meal/mealValidator.min',
            'personelViewModel': '/spa/components/personel/personelViewModel.min',
            'personelValidator': '/spa/components/personel/personelValidator.min',
            'personelrestaurantViewModel': '/spa/components/personelrestaurant/personelrestaurantViewModel.min',
            'persianDateViewModel': '/spa/components/PersianDate/PersianDateViewModel.min',
            'restaurantViewModel': '/spa/components/restaurant/restaurantViewModel.min',
            'mealRestaurantViewModel': '/spa/components/mealRestaurant/mealRestaurantViewModel.min',
            'restaurantFoodPackageViewModel': '/spa/components/restaurantFoodPackage/restaurantFoodPackageViewModel.min',
            'personelFoodViewModel': '/spa/components/personelFood/personelFoodViewModel.min',
            'restaurantValidator': '/spa/components/restaurant/restaurantValidator.min',
            'copyFoodValidator': '/spa/components/restaurantFoodPackage/copyFoodValidator.min',
            'copyFoodPackageViewModel': '/spa/components/restaurantFoodPackage/copyFoodPackageViewModel.min',
            'rimsSettingViewModel': '/spa/components/rimsSetting/rimsSettingViewModel.min',
            'rimsSettingValidator': '/spa/components/rimsSetting/rimsSettingValidator.min',
            'deviceViewModel': '/spa/components/device/deviceViewModel.min',
            'deviceValidator': '/spa/components/device/deviceValidator.min',
            'jsoneditor': '/Scripts/jsonEditor/jsoneditor',
            'auditViewModel': '/spa/components/auditEvent/auditViewModel.min'
        },
        shim: {
            'komapping': {
                deps: ['ko']
            },
            'bootstrap': {
                deps: ['jquery']
            },
            'jRespond': {
                deps: ['bootstrap']
            },
            'jquery.sparkline': {
                deps: ['jRespond']
            },
            'bootstrap-dialog': {
                deps: ['jquery', 'bootstrap']
            },
            'logger': {
                deps: ['toastr']
            },
            'bootstrapDatepicker': {
                deps: ['bootstrap']
            },
            'bootstrapDatepickerFa': {
                deps: ['bootstrapDatepicker']
            },
            'datepicker': {
                deps: ['bootstrapDatepickerFa']
            },
            'raphael': {
                deps: ['jquery']
            },
            'morris': {
                deps: ['raphael']
            },
            'knockout-morris': {
                deps: ['morris']
            },
            'select2.full': {
                deps: ['jquery']
            },
            'knockout-select2': {
                deps: ['select2.full']
            },
            'dataTablesBootstrap': {
                deps: ['jqueryDataTables']
            },
        }
    });

    require(["styler"], function (styler) {
        styler.load("/Content/Style/bootstrap/bootstrap.min.css");
        styler.load("/Content/Style/animate.min.css");
        styler.load("/Content/Style/font-awesome/font-awesome.min.css");
        styler.load("/Scripts/animsition/css/animsition.min.css");
        styler.load("/Content/Style/main.min.css");
        styler.load("/Content/ExtendedTheme.min.css");
        require(["modernizr"], function (modernizr) {
            require(["bootstrap", "jRespond", "jquery.sparkline"],
                   function () {
                       require(["jquery.slimscroll"],
                      function (slimscroll) {
                          require(["jquery.animsition", "Components/pcomponents.min"],
                          function () {
                          	require(["toastr", "screenfull", "parsley", "jquery.bootstrap.wizard", "jsoneditor"],
                             function () {
                             });
                          });
                      });
                   });
        });
    });
})();

//require(["modernizr"], function (modernizr) {
//    require(["bootstrap", "jRespond"],
//        function () {
//            require(["jquery.sparkline"],
//           function (sparkline) {
//               require(["jquery.slimscroll"],
//              function (slimscroll) {
//                  require(["jquery.animsition", "Components/components"],
//                  function () {
//                      require(["toastr"],
//                     function () {
//                     });
//                  });
//              });
//           });
//        });
//});