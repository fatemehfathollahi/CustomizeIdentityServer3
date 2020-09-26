define(['jquery'], function ($) {
	//var app = app || {};
	//app.enumerations = app.enumerations || {};
	//app.methods = app.methods || {};
	var action = function () { }
	action.prototype = {
		none: 0,
		create: 1,
		read: 2,
		update: 3,
		delete: 4,

		//toString dar inja mesle override kardane toString dar class dar C# ast
		toString: function (item) {
			switch (item) {
				case 1:
					return "ایجاد";
				case 2:
					return "نمایش";
				case 3:
					return "اصلاح";
				case 4:
					return "حذف";
				default:
					return "";
			}
		}
	}

	var extraAction = function () { }
	extraAction.prototype = {
		confirmOperation: 0,
		addCorsOrigin: 1,
		deleteCorsOrigin: 2,
		addURI: 3,
		deleteURI: 4,
		addClientURI: 5,
		deleteClientURI: 6,
		addClientScops: 7,
		deleteClientScops: 8,
		addClientSecrets: 9,
		deleteClientSecrets: 10,
		addScopeClaim: 11,
		deleteScopeClaim:12	,
		addScopeSecret: 13,
		deleteScopeSecret: 14,
		addPermision: 15,
		deletePermision: 16,
		addPermission: 17,
	    deletePermission:18
	}

	return {
		action: action,
		extraAction: extraAction,
	};
});