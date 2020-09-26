using Framework.Core.Common.Extensions;
using Management.Infrastructure.Facade.DTOModel;
using Management.Infrastructure.Facade.FacadeService.Contracts;
using ManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace ManagementApplication.Api
{
	public class ClientApiController : BaseApiController
	{
		#region Fields

		private IClientFacadeService clientFacadeService;

		#endregion Fields

		#region Constructor

		public ClientApiController(IClientFacadeService clientFacadeService)
		{
			this.clientFacadeService = clientFacadeService;
		}

		#endregion Constructor

		#region Method(s)

		public object GetClient()
		{
			NameValueCollection nvc = System.Web.HttpUtility.ParseQueryString(Request.RequestUri.Query);
			string FilterData = nvc["FilterData"];
			//if (FilterData !=null && FilterData.Length != 2 && FilterData.Length > 3 && FilterData.Substring(FilterData.Length - 3, 1) == "\"")
			//{
			//	FilterData = FilterData.Insert(FilterData.Length - 2, "0");
			//}

			JavaScriptSerializer objSerializer = new JavaScriptSerializer();
			ClientDTO objFoodResult = string.IsNullOrEmpty(FilterData) ? null : objSerializer.Deserialize<ClientDTO>(FilterData);

			int PageIndex = Convert.ToInt32(nvc["iDisplayStart"]);
			int PageSize = Convert.ToInt32(nvc["iDisplayLength"]);
			string SortColumnName = string.Empty;
			string SortColumnDirection = string.Empty;
			if (!string.IsNullOrEmpty(nvc["mDataProp_" + nvc["iSortCol_0"]]))
			{
				SortColumnName = nvc["mDataProp_" + nvc["iSortCol_0"]].Replace("Formated", "").Replace("WithCurrency", "");
				SortColumnDirection = nvc["sSortDir_0"];
			}

			IEnumerable<ClientListDTO> ClientList = clientFacadeService.GetAllClients();
			if (ClientList == null)
			{
				ClientList = new List<ClientListDTO>();
			}

			DataTablePager<ClientListDTO> objDataTable = new DataTablePager<ClientListDTO>
			{
				sEcho = System.Convert.ToInt32(nvc["sEcho"]).ToString(),
				iTotalRecords = ClientList.Count(),
				iTotalDisplayRecords = ClientList.Count(),
				aaData = ClientList
			};
			return objDataTable;
		}

		public object GetClientWithMode(int mode, int temp, string temp1)
		{
			switch (mode)
			{
				case 1:
					return clientFacadeService.ExistsClientId(temp, temp1);

				case 2:
					return clientFacadeService.ExistsClientName(temp, temp1);

				case 3:
					return temp1.Trim().Sha256();

				default:
					break;
			}
			return null;
		}

		public ClientDTO GetClientlWithId(int id)
		{
			ClientDTO result = clientFacadeService.GetClientById(id);
			return result;
		}

		//public IHttpActionResult PostClient(ClientDTO dto)
		//{
		//	clientFacadeService.AddClient(dto);
		//	return Ok("");
		//}
		public IHttpActionResult PostClient()
		{
			try
			{
				NameValueCollection nvc = HttpContext.Current.Request.Form;
				ClientDTO dtoModel = new ClientDTO();

				// iterate through and map to strongly typed model
				foreach (string kvp in nvc.AllKeys)
				{
					if (kvp.ToLower().Equals("dtomodel"))
					{
						JavaScriptSerializer objSerializer = new JavaScriptSerializer();
						dtoModel = objSerializer.Deserialize<ClientDTO>(nvc[kvp]);
					}
				}

				if (HttpContext.Current.Request.Files.AllKeys.Any())
				{
					// Get the uploaded image from the Files collection
					HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

					if (httpPostedFile != null)
					{
						dtoModel.LogoUri = new byte[httpPostedFile.ContentLength];
						httpPostedFile.InputStream.Read(dtoModel.LogoUri, 0, httpPostedFile.ContentLength);
					}
				}
				clientFacadeService.AddClient(dtoModel);
				//model.Image = HttpContext.Current.Request.Files["Image"];
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok("");
		}

		//public IHttpActionResult PutClient(ClientDTO dto)
		//{
		//	clientFacadeService.UpdateClient(dto);
		//	return Ok("");
		//}
		public IHttpActionResult PutClient()
		{
			try
			{
				//var t = Guid.Empty;
				// get variables first
				NameValueCollection nvc = HttpContext.Current.Request.Form;
				ClientDTO dtoModel = new ClientDTO();

				// iterate through and map to strongly typed model
				foreach (string kvp in nvc.AllKeys)
				{
					if (kvp.ToLower().Equals("dtomodel"))
					{
						JavaScriptSerializer objSerializer = new JavaScriptSerializer();
						string c = nvc[kvp];
						dtoModel = objSerializer.Deserialize<ClientDTO>(nvc[kvp]);
						//var json = new JavaScriptSerializer().Serialize(nvc[kvp]);
						//System.Reflection.PropertyInfo pi
						//    = model.GetType().GetProperty(kvp, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
						//if (pi != null)
						//{
						//    pi.SetValue(model, nvc[kvp], null);
						//}
					}
				}
				if (dtoModel.PhotoChanged)
				{
					dtoModel.LogoUri = new byte[0];
					if (HttpContext.Current.Request.Files.AllKeys.Any())
					{
						// Get the uploaded image from the Files collection
						HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

						if (httpPostedFile != null)
						{
							// Validate the uploaded image(optional)
							//var image = httpPostedFile;
							// Get the complete file path
							//var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

							// Save the uploaded file to "UploadedFiles" folder
							//httpPostedFile.SaveAs(fileSavePath);

							dtoModel.LogoUri = new byte[httpPostedFile.ContentLength];
							httpPostedFile.InputStream.Read(dtoModel.LogoUri, 0, httpPostedFile.ContentLength);
						}
					}
				}
				else
				{
					dtoModel.LogoUri = clientFacadeService.GetClientById(dtoModel.Id).LogoUri;
				}

				clientFacadeService.UpdateClient(dtoModel);
				//model.Image = HttpContext.Current.Request.Files["Image"];
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok("");
		}

		public IHttpActionResult DeleteClient(ClientDTO dto)
		{
			clientFacadeService.DeleteClient(dto);
			return Ok("");
		}

		#endregion Method(s)
	}
}