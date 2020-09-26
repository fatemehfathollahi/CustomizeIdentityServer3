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
	public class ScopeApiController : ApiController
	{
		#region Fields

		private IScopeFacadeService scopeFacadeService;

		#endregion Fields

		#region Constructor

		public ScopeApiController(IScopeFacadeService scopeFacadeService)
		{
			this.scopeFacadeService = scopeFacadeService;
		}

		#endregion Constructor

		#region Method(s)

		public object GetClient()
		{
			System.Collections.Specialized.NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
			string FilterData = nvc["FilterData"];
			//if (FilterData !=null && FilterData.Length != 2 && FilterData.Length > 3 && FilterData.Substring(FilterData.Length - 3, 1) == "\"")
			//{
			//	FilterData = FilterData.Insert(FilterData.Length - 2, "0");
			//}

			JavaScriptSerializer objSerializer = new JavaScriptSerializer();
			ScopeDTO objFoodResult = string.IsNullOrEmpty(FilterData) ? null : objSerializer.Deserialize<ScopeDTO>(FilterData);

			int PageIndex = Convert.ToInt32(nvc["iDisplayStart"]);
			int PageSize = Convert.ToInt32(nvc["iDisplayLength"]);
			string SortColumnName = string.Empty;
			string SortColumnDirection = string.Empty;
			if (!string.IsNullOrEmpty(nvc["mDataProp_" + nvc["iSortCol_0"]]))
			{
				SortColumnName = nvc["mDataProp_" + nvc["iSortCol_0"]].Replace("Formated", "").Replace("WithCurrency", "");
				SortColumnDirection = nvc["sSortDir_0"];
			}

			IEnumerable<ScopeDTO> ClientList = scopeFacadeService.GetAllScopes();
			if (ClientList == null)
			{
				ClientList = new List<ScopeDTO>();
			}

			//var clientList = new List<ClientDTO>();

			DataTablePager<ScopeDTO> objDataTable = new DataTablePager<ScopeDTO>
			{
				sEcho = System.Convert.ToInt32(nvc["sEcho"]).ToString(),
				iTotalRecords = ClientList.Count(),
				iTotalDisplayRecords = ClientList.Count(),
				aaData = ClientList
			};
			return objDataTable;
		}

		public object GetScopeWithMode(int mode, int temp, string temp1)
		{
			switch (mode)
			{
				case 1:
					return scopeFacadeService.ExistsName(temp, temp1);

				default:
					break;
			}
			return null;
		}

		public ScopeDTO GetScopelWithId(int id)
		{
			ScopeDTO result = scopeFacadeService.GetScopeByID(id);
			return result;
		}

		public IHttpActionResult PostScope(ScopeDTO dto)
		{
			scopeFacadeService.AddScope(dto);
			return Ok("");
		}

		public IHttpActionResult PutScope(ScopeDTO dto)
		{
			scopeFacadeService.UpdateScope(dto);
			return Ok("");
		}

		public IHttpActionResult DeleteScope(ScopeDTO dto)
		{
			scopeFacadeService.DeleteScope(dto);
			return Ok("");
		}

		#endregion Method(s)
	}
}