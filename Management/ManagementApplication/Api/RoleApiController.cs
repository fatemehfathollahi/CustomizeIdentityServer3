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
	public class RoleApiController : BaseApiController
	{
		#region Fields

		private IRoleFacadeService roleFacadeService;

		#endregion Fields

		#region Constructor

		public RoleApiController(IRoleFacadeService roleFacadeService)
		{
			this.roleFacadeService = roleFacadeService;
		}

		#endregion Constructor

		#region Method(s)

		public object GetRole()
		{
			System.Collections.Specialized.NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
			string FilterData = nvc["FilterData"];
			//if (FilterData !=null && FilterData.Length != 2 && FilterData.Length > 3 && FilterData.Substring(FilterData.Length - 3, 1) == "\"")
			//{
			//	FilterData = FilterData.Insert(FilterData.Length - 2, "0");
			//}

			JavaScriptSerializer objSerializer = new JavaScriptSerializer();
			RoleDTO objFoodResult = string.IsNullOrEmpty(FilterData) ? null : objSerializer.Deserialize<RoleDTO>(FilterData);

			int PageIndex = Convert.ToInt32(nvc["iDisplayStart"]);
			int PageSize = Convert.ToInt32(nvc["iDisplayLength"]);
			string SortColumnName = string.Empty;
			string SortColumnDirection = string.Empty;
			if (!string.IsNullOrEmpty(nvc["mDataProp_" + nvc["iSortCol_0"]]))
			{
				SortColumnName = nvc["mDataProp_" + nvc["iSortCol_0"]].Replace("Formated", "").Replace("WithCurrency", "");
				SortColumnDirection = nvc["sSortDir_0"];
			}

			IEnumerable<RoleDTO> roleList = roleFacadeService.GetRolesByClient(objFoodResult.ClientId);
			if (roleList == null)
			{
				roleList = new List<RoleDTO>();
			}

			//var clientList = new List<ClientDTO>();

			DataTablePager<RoleDTO> objDataTable = new DataTablePager<RoleDTO>
			{
				sEcho = System.Convert.ToInt32(nvc["sEcho"]).ToString(),
				iTotalRecords = roleList.Count(),
				iTotalDisplayRecords = roleList.Count(),
				aaData = roleList
			};
			return objDataTable;
		}

		public RoleDTO GetScopelWithId(int id)
		{
			RoleDTO result = roleFacadeService.FindRoleById(id);
			return result;
		}

		public IHttpActionResult PostRole(RoleDTO dto)
		{
			roleFacadeService.CreateRole(dto.ClientId, dto);
			return Ok("");
		}

		public IHttpActionResult PutRole(RoleDTO dto)
		{
			roleFacadeService.UpdateRole(dto);
			return Ok("");
		}

		public IHttpActionResult DeleteScope(RoleDTO dto)
		{
			roleFacadeService.DeleteRole(dto);
			return Ok("");
		}

		#endregion Method(s)
	}
}