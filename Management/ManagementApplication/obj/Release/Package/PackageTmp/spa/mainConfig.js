(function () {
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
			'text': '/spa/engine/lib/text',
			'domReady': '/spa/engine/lib/domReady',
			'jquery': '/Scripts/jquery/jquery-2.2.4.min',
			'bootstrap': '/Scripts/bootstrap/bootstrap.min',
			'jRespond': '/Scripts/jRespond/jRespond.min',
			'jquery.sparkline': '/Scripts/sparkline/jquery.sparkline.min',
			'jquery.slimscroll': '/Scripts/slimscroll/jquery.slimscroll.min',
			'jquery.animsition': '/Scripts/animsition/js/jquery.animsition.min',
			'ko': '/spa/engine/lib/knockout-3.4.0',
			'crossroads': '/spa/engine/lib/Crossroads',
			'styler': '/spa/engine/infrastructure/styler/styler',
			'router': '/spa/engine/infrastructure/router/router',
			'modernizr': '/Scripts/modernizr-2.8.3-respond-1.4.2.min',
			'main': '/Scripts/main',
			'screenfull': '/assets/js/vendor/screenfull/screenfull.min',
			'parsley': '/assets/js/vendor/parsley/parsley.min',
			'jquery.bootstrap.wizard': '/assets/js/vendor/form-wizard/jquery.bootstrap.wizard.min',
			'bootstrap-dialog': '/Scripts/bootstrapDialog/bootstrap-dialog',
			'dialog': '/spa/engine/widget/dialog/dialog',
			'toastr': '/Scripts/toastr/toastr.min',
			'logger': '/spa/engine/widget/logger/logger.min',
			'bootstrapDatepicker': '/Scripts/bootstrapDatePicker/bootstrap-datepicker.min',
			'bootstrapDatepickerFa': '/Scripts/bootstrapDatePicker/bootstrap-datepicker.fa.min',
			'datepicker': '/spa/engine/widget/datepicker/datepicker.min',
			'jquerymask': '/Scripts/jquerymask/jquery.mask.min',
			'commonMask': '/spa/engine/infrastructure/mask/commonMask',
			'bootstrapValidator': '/Scripts/BootStrapValidator/bootstrapValidator.min',
			'validator': '/spa/engine/infrastructure/validator/validator',
			'numeric': '/spa/engine/infrastructure/common/numeric',
			'persiandate': '/Scripts/date/persian-date-0.1.8.min',
			'date': '/spa/engine/infrastructure/common/date',
			'time': '/spa/engine/infrastructure/common/time',
			'raphael': '/Scripts/raphael/raphael-min',
			'morris': '/Scripts/morris/morris.min',
			'knockout-morris': '/spa/engine/widget/morris/knockout-morris.min',
			'select2.full': '/Scripts/select/select2.min',
			'knockout-select2': '/spa/engine/widget/select2/knockout-select2.min',
			'commonMethod': '/spa/components/common/commonMethod',
			'jqueryDataTables': '/Scripts/jquery.dataTable/jquery.dataTables',
			'dataTablesBootstrap': '/Scripts/jquery.dataTable/dataTables.bootstrap',
			'dataTable': '/spa/engine/widget/dataTable/datatable',
			'common': '/spa/components/common/common',
			'komapping': '/spa/engine/lib/knockout.mapping',
			'linq': '/Scripts/linq/linq.min',
			'fileBindings': '/spa/engine/widget/fileUploader/knockout-file-upload',
			'clientViewModel': '/spa/components/client/clientViewModel',
			'clientValidator': '/spa/components/client/clientValidator',
			'corsOriginViewModel': '/spa/components/corsOrigin/corsOriginViewModel',
			'corsOriginValidator':'/spa/components/corsOrigin/corsOriginValidator',	  
			'postLogoutViewModel': '/spa/components/postLogoutRedirectURI/postLogoutViewModel',
			'postLogoutValidator': '/spa/components/postLogoutRedirectURI/postLogoutValidator',
			'clientRedirectViewModel': '/spa/components/clientRedirect/clientRedirectViewModel',
			'clientRedirectValidator': '/spa/components/clientRedirect/clientRedirectValidator',
			'clientScopsViewModel': '/spa/components/clientScop/clientScopsViewModel',
			'clientScopsValidator': '/spa/components/clientScop/clientScopsValidator',
			'clientSecretViewModel': '/spa/components/clientSecret/clientSecretViewModel',
			'clientSecretValidator': '/spa/components/clientSecret/clientSecretValidator',
			'scopeViewModel': '/spa/components/scope/scopeViewModel',
			'scopeValidator': '/spa/components/scope/scopeValidator',
			'scopeClaimViewModel': '/spa/components/scopeClaim/scopeClaimViewModel',
			'scopeClaimValidator': '/spa/components/scopeClaim/scopeClaimValidator',
			'scopeSecretViewModel': '/spa/components/scopeSecret/scopeSecretViewModel',
			'scopeSecretValidator': '/spa/components/scopeSecret/scopeSecretValidator',
			'roleViewModel': '/spa/components/role/roleViewModel',
			'roleValidator': '/spa/components/role/roleValidator',
			'permisionViewModel': '/spa/components/permision/permisionViewModel',
			'permisionValidator': '/spa/components/permision/permisionValidator',
			'userValidator': '/spa/components/user/userValidator',
			'userViewModel': '/spa/components/user/userViewModel',
			'userClientPermissionViewModel': '/spa/components/userClientPermission/userClientPermissionViewModel'
			
				  
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
		styler.load("/Content/Style/animate.css");
		styler.load("/Content/Style/font-awesome/font-awesome.min.css");
		styler.load("/Scripts/animsition/css/animsition.min.css");
		styler.load("/Content/Style/main.css");
		styler.load("/Content/ExtendedTheme.css");
		require(["modernizr"], function (modernizr) {
			require(["bootstrap", "jRespond", "jquery.sparkline"],
                   function () {
                   	require(["jquery.slimscroll"],
				   function (slimscroll) {
				   	require(["jquery.animsition", "Components/components"],
					function () {
						require(["toastr", "screenfull", "parsley", "jquery.bootstrap.wizard"],
					   function () {
					   });
					});
				   });
                   });
		});
	});
})();