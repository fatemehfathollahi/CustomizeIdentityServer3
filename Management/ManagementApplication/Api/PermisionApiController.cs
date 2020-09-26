using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using ManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ManagementApplication.Api
{
	public class PermisionApiController : BaseApiController
	{
		#region Fields

		private IPermissionFacadeService permissionFacadeService;

		#endregion Fields

		#region Constructor

		public PermisionApiController(IPermissionFacadeService permissionFacadeService)
		{
			this.permissionFacadeService = permissionFacadeService;
		}

		#endregion Constructor

		#region Method(s)

		public object GetPermission()
		{
			System.Collections.Specialized.NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
			string FilterData = nvc["FilterData"];
			//if (FilterData !=null && FilterData.Length != 2 && FilterData.Length > 3 && FilterData.Substring(FilterData.Length - 3, 1) == "\"")
			//{
			//	FilterData = FilterData.Insert(FilterData.Length - 2, "0");
			//}

			JavaScriptSerializer objSerializer = new JavaScriptSerializer();
			PermissionDTO objPermissionResult = string.IsNullOrEmpty(FilterData) ? null : objSerializer.Deserialize<PermissionDTO>(FilterData);

			int PageIndex = Convert.ToInt32(nvc["iDisplayStart"]);
			int PageSize = Convert.ToInt32(nvc["iDisplayLength"]);
			string SortColumnName = string.Empty;
			string SortColumnDirection = string.Empty;
			if (!string.IsNullOrEmpty(nvc["mDataProp_" + nvc["iSortCol_0"]]))
			{
				SortColumnName = nvc["mDataProp_" + nvc["iSortCol_0"]].Replace("Formated", "").Replace("WithCurrency", "");
				SortColumnDirection = nvc["sSortDir_0"];
			}

			IEnumerable<PermissionListDTO> PermissionList = permissionFacadeService.GetPermissions();
			if (PermissionList == null)
			{
				PermissionList = new List<PermissionListDTO>();
			}

			//var clientList = new List<ClientDTO>();

			DataTablePager<PermissionListDTO> objDataTable = new DataTablePager<PermissionListDTO>
			{
				sEcho = System.Convert.ToInt32(nvc["sEcho"]).ToString(),
				iTotalRecords = PermissionList.Count(),
				iTotalDisplayRecords = PermissionList.Count(),
				aaData = PermissionList
			};
			return objDataTable;
		}

		public PermissionDTO GetPermissionWithId(int id)
		{
			PermissionDTO result = permissionFacadeService.FindPermissionByID(id);
			return result;
		}

		public object GetPermissionWithMode(int mode, int temp, string temp1)
		{
			switch (mode)
			{
				case 1:
					return permissionFacadeService.GetPermissions();

				case 2:
					return permissionFacadeService.ExistsPermissionName(temp, temp1);

				default:
					break;
			}
			return null;
		}

		public bool GetExistsPermissionName(int Id, string Name)
		{
			bool blnResult = true;
			blnResult = permissionFacadeService.ExistsPermissionName(Id, Name);
			return blnResult;
		}

		public IHttpActionResult PostPermission(PermissionDTO dto)
		{
			permissionFacadeService.CreatePermission(dto);
			return Ok("");
		}

		public IHttpActionResult PutPermission(PermissionDTO dto)
		{
			permissionFacadeService.UpdatePermission(dto);
			return Ok("");
		}

		public IHttpActionResult DeletePermission(PermissionDTO dto)
		{
			permissionFacadeService.DeletePermission(dto);
			return Ok("");
		}

		#endregion Method(s)
	}
}