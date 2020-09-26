define(["ko", "jquery", "logger"], function (ko, $, logger) {
	function AddClient(success, error, pParam) {
		 var data = new FormData();
		var uploadFile = pParam.clientRow.viewModel.fileData().file();
		pParam.clientRow.LogoUri(null);
		pParam.clientRow.viewModel = null;
		var modelParam = JSON.stringify(ko.toJS(pParam.clientRow));
		data.append("UploadedImage", uploadFile);
		data.append("DTOModel", modelParam);

		$.ajax({
			type: "POST",
			url: "/api/ClientApi",
			cache: false,
			contentType: false,
			processData: false,
			data: data
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function UpdateClient(success, error, pParam) {
	 var data = new FormData();
		var uploadFile = pParam.clientRow.viewModel.fileData().file();
		pParam.clientRow.LogoUri(null);
		pParam.clientRow.viewModel = null;
		var modelParam = JSON.stringify(ko.toJS(pParam.clientRow));
		data.append("UploadedImage", uploadFile);
		data.append("DTOModel", modelParam);

		$.ajax({
			type: "PUT",
			url: "/api/ClientApi",
			cache: false,
			contentType: false,
			processData: false,
			data: data
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
		//$.ajax({
		//	type: "PUT",
		//	url: "/api/ClientApi",
		//	cache: false,
		//	dataType: "json",
		//	async: false,
		//	data: ko.toJS(pParam.clientRow)
		//}).success(function (result) {
		//	success(result);
		//}).error(function (err) {
		//	error(err);
		//});
	};
	function DeleteClient(success, error, pParam) {
		$.ajax({
			type: "DELETE",
			url: "/api/ClientApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.clientRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function GetClientWithId(success, error, params) {
		if (params == undefined || params == null)
			params = {};
		$.ajax({
			type: "GET",
			url: "/api/ClientApi",
			cache: false,
			dataType: "json",
			async: (params.async === false ? false : true),
			data: {
				id: (params.Id ? params.Id : '0')
			}
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function AddScope(success, error, pParam) {
		$.ajax({
			type: "POST",
			url: "/api/ScopeApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.scopeRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function UpdateScope(success, error, pParam) {
		$.ajax({
			type: "PUT",
			url: "/api/ScopeApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.scopeRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function DeleteScope(success, error, pParam) {
		$.ajax({
			type: "DELETE",
			url: "/api/ScopeApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.scopeRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function GetScopeWithId(success, error, params) {
		if (params == undefined || params == null)
			params = {};
		$.ajax({
			type: "GET",
			url: "/api/ScopeApi",
			cache: false,
			dataType: "json",
			async: (params.async === false ? false : true),
			data: {
				id: (params.Id ? params.Id : '0')
			}
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function AddRole(success, error, pParam) {
		$.ajax({
			type: "POST",
			url: "/api/RoleApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.roleRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function UpdateRole(success, error, pParam) {
		$.ajax({
			type: "PUT",
			url: "/api/RoleApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.roleRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function DeleteRole(success, error, pParam) {
		$.ajax({
			type: "DELETE",
			url: "/api/RoleApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.roleRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function GetRoleWithId(success, error, params) {
		if (params == undefined || params == null)
			params = {};
		$.ajax({
			type: "GET",
			url: "/api/RoleApi",
			cache: false,
			dataType: "json",
			async: (params.async === false ? false : true),
			data: {
				id: (params.Id ? params.Id : '0')
			}
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function AddPermission(success, error, pParam) {
		$.ajax({
			type: "POST",
			url: "/api/PermisionApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.permisionRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function UpdatePermission(success, error, pParam) {
		$.ajax({
			type: "PUT",
			url: "/api/PermisionApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.permisionRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function DeletePermission(success, error, pParam) {
		$.ajax({
			type: "DELETE",
			url: "/api/PermisionApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.permisionRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function GetPermissionWithId(success, error, params) {
		if (params == undefined || params == null)
			params = {};
		$.ajax({
			type: "GET",
			url: "/api/PermisionApi",
			cache: false,
			dataType: "json",
			async: (params.async === false ? false : true),
			data: {
				id: (params.Id ? params.Id : '0')
			}
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function GetPermissionList(success, error, params) {
	    if (params == undefined || params == null)
	        params = {};
	    $.ajax({
	        type: "GET",
	        url: "/api/PermisionApi",
	        cache: false,
	        dataType: "json",
	        async: (params.async === false ? false : true),
	        data: {
	            mode: 1, temp: 1,temp1:''
	        }
	    }).success(function (result) {
	        success(result);
	    }).error(function (err) {
	        error(err);
	    });
	};

	function AddUser(success, error, pParam) {
		$.ajax({
			type: "POST",
			url: "/api/UserApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.userRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	function GetUserWithId(success, error, params) {
		if (params == undefined || params == null)
			params = {};
		$.ajax({
			type: "GET",
			url: "/api/UserApi",
			cache: false,
			dataType: "json",
			async: (params.async === false ? false : true),
			data: {
				id: (params.Id ? params.Id : '0')
			}
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	};
	 function UpdateUser(success, error, pParam) {
		$.ajax({
			type: "PUT",
			url: "/api/UserApi",
			cache: false,
			dataType: "json",
			async: false,
			data: ko.toJS(pParam.userRow)
		}).success(function (result) {
			success(result);
		}).error(function (err) {
			error(err);
		});
	 };
	 function DeleteUser(success, error, pParam) {
	 	$.ajax({
	 		type: "DELETE",
	 		url: "/api/UserApi",
	 		cache: false,
	 		dataType: "json",
	 		async: false,
	 		data: ko.toJS(pParam.userRow)
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 function GetClientPermissionList(success, error, params) {
	 	if (params == undefined || params == null)
	 		params = {};
	 	$.ajax({
	 		type: "GET",
	 		url: "/api/ClientPermisionApi",
	 		cache: false,
	 		dataType: "json",
	 		async: (params.async === false ? false : true),
	 		data: {
	 			mode: 1, temp: 1
	 		}
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 function ExistsPermissionName(success, error, params) {
	 	$.ajax({
	 		type: "GET",
	 		url: "/api/PermisionApi",
	 		cache: false,
	 		async: (params.async === false ? false : true),
	 		data: { mode: 2, temp: params.Id, temp1: params.Name }
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 function ExistsClientId(success, error, params) {
	 	$.ajax({
	 		type: "GET",
	 		url: "/api/ClientApi",
	 		cache: false,
	 		async: (params.async === false ? false : true),
	 		data: { mode: 1, temp: params.Id, temp1: params.ClientId }
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 function GetHashCode(success, error, params) {
	 	$.ajax({
	 		type: "GET",
	 		url: "/api/ClientApi",
	 		cache: false,
	 		async: (params.async === false ? false : true),
	 		data: { mode: 3, temp: 0, temp1: params.value }
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 function ExistsClientName(success, error, params) {
	 	$.ajax({
	 		type: "GET",
	 		url: "/api/ClientApi",
	 		cache: false,
	 		async: (params.async === false ? false : true),
	 		data: { mode: 2, temp: params.Id, temp1: params.ClientName }
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 function ExistsUserName(success, error, params) {
	 	$.ajax({
	 		type: "GET",
	 		url: "/api/UserApi",
	 		cache: false,
	 		async: (params.async === false ? false : true),
	 		data: { mode: 1, temp: params.Id, temp1: params.UserName }
	 	}).success(function (result) {
	 		success(result);
	 	}).error(function (err) {
	 		error(err);
	 	});
	 };
	 	function ExistsScopeName(success, error, params) {
	 		$.ajax({
	 			type: "GET",
	 			url: "/api/ScopeApi",
	 			cache: false,
	 			async: (params.async === false ? false : true),
	 			data: { mode: 1, temp: params.Id, temp1: params.Name }
	 		}).success(function (result) {
	 			success(result);
	 		}).error(function (err) {
	 			error(err);
	 		});
	 	};
	return {
		AddClient: AddClient,
		GetClientWithId: GetClientWithId,
		UpdateClient: UpdateClient,
		DeleteClient:DeleteClient,
		AddScope:AddScope,
		UpdateScope: UpdateScope,
		DeleteScope: DeleteScope,
		GetScopeWithId: GetScopeWithId,
		AddRole:AddRole,
		UpdateRole:UpdateRole,
		DeleteRole: DeleteRole,
		GetRoleWithId: GetRoleWithId,
		AddUser:AddUser,
		AddPermission:AddPermission,
		GetPermissionWithId:GetPermissionWithId	,
		UpdatePermission:UpdatePermission,
		DeletePermission: DeletePermission,
        GetPermissionList:GetPermissionList,
        GetUserWithId: GetUserWithId,
        UpdateUser: UpdateUser,
        DeleteUser: DeleteUser,
		GetClientPermissionList:GetClientPermissionList	,
		ExistsPermissionName: ExistsPermissionName	  ,
		ExistsClientId: ExistsClientId,
		ExistsClientName: ExistsClientName,
		ExistsUserName: ExistsUserName,
		ExistsScopeName: ExistsScopeName,
		GetHashCode: GetHashCode
	};
});