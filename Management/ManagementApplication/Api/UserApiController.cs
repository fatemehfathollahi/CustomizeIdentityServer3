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
	public class UserApiController : BaseApiController
	{
		#region Fields

		private IUserFacadeService userFacadeService;

		#endregion Fields

		#region Constructor

		public UserApiController(IUserFacadeService userFacadeService)
		{
			this.userFacadeService = userFacadeService;
		}

		#endregion Constructor

		#region Method(s)

		public object GetUser()
		{
			System.Collections.Specialized.NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
			string FilterData = nvc["FilterData"];
			//if (FilterData !=null && FilterData.Length != 2 && FilterData.Length > 3 && FilterData.Substring(FilterData.Length - 3, 1) == "\"")
			//{
			//	FilterData = FilterData.Insert(FilterData.Length - 2, "0");
			//}

			JavaScriptSerializer objSerializer = new JavaScriptSerializer();
			UserDTO objUserResult = string.IsNullOrEmpty(FilterData) ? null : objSerializer.Deserialize<UserDTO>(FilterData);

			int PageIndex = Convert.ToInt32(nvc["iDisplayStart"]);
			int PageSize = Convert.ToInt32(nvc["iDisplayLength"]);
			string SortColumnName = string.Empty;
			string SortColumnDirection = string.Empty;
			if (!string.IsNullOrEmpty(nvc["mDataProp_" + nvc["iSortCol_0"]]))
			{
				SortColumnName = nvc["mDataProp_" + nvc["iSortCol_0"]].Replace("Formated", "").Replace("WithCurrency", "");
				SortColumnDirection = nvc["sSortDir_0"];
			}

			IEnumerable<UserListDTO> roleList = userFacadeService.GetUsers(objUserResult, PageIndex, PageSize);
			int totalCount = 0;
			if (roleList == null)
			{
				roleList = new List<UserListDTO>();
			}
			else if (roleList.Count() > 0)
			{
				totalCount = userFacadeService.GetTotalCount(objUserResult);
			}

			DataTablePager<UserListDTO> objDataTable = new DataTablePager<UserListDTO>
			{
				sEcho = System.Convert.ToInt32(nvc["sEcho"]).ToString(),
				iTotalRecords = roleList.Count(),
				iTotalDisplayRecords = totalCount,
				aaData = roleList
			};
			return objDataTable;
		}

		public UserDTO GetUserWithId(int id)
		{
			UserDTO result = userFacadeService.FindUserById(id);
			return result;
		}

		public object GetUserWithMode(int mode, int temp, string temp1)
		{
			switch (mode)
			{
				case 1:
					return userFacadeService.ExistsUerName(temp, temp1);

				default:
					break;
			}
			return null;
		}

		public IHttpActionResult PostUser(UserDTO dto)
		{
			//dto = GetDtoWithUserProfile(dto);
			userFacadeService.CreateUser(dto);
			return Ok("");
		}

		public IHttpActionResult PutUser(UserDTO dto)
		{
			//dto = GetDtoWithUserProfile(dto);
			userFacadeService.UpdateUser(dto);
			return Ok("");
		}

		public IHttpActionResult DeleteUser(UserDTO dto)
		{
			//dto = GetDtoWithUserProfile(dto);
			userFacadeService.DeleteUser(dto);
			return Ok("");
		}

		private UserDTO GetDtoWithUserProfile(UserDTO dto)
		{
			dto.UserProfile = new UserProfileDTO
			{
				Id = dto.Id,
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Mobile = dto.Mobile,
				InternalPhone = dto.InternalPhone,
				InternalPhoneCode = dto.InternalPhoneCode,
				EmployeeNumber = dto.EmployeeNumber,
				NationalCode = dto.NationalCode,
				PersonCode = dto.PersonCode
			};
			return dto;
		}

		#endregion Method(s)
	}
}