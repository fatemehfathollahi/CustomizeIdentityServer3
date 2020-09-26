using Management.Infrastructure.Facade.FacadeServices.Contracts;
using System.Linq;

namespace ManagementApplication.Api
{
	public class ClientPermisionApiController : BaseApiController
	{
		#region Fields

		private IUserClientPermissionFacadeService permissionFacadeService;

		#endregion Fields

		#region Constructor

		public ClientPermisionApiController(IUserClientPermissionFacadeService permissionFacadeService)
		{
			this.permissionFacadeService = permissionFacadeService;
		}

		#endregion Constructor

		#region Method(s)

		public object GetPermissionListWithMode(int mode, int temp)
		{
			switch (mode)
			{
				case 1:
					return permissionFacadeService.Get().OrderBy(o => o.Client_Id);

				default:
					break;
			}
			return null;
		}

		#endregion Method(s)
	}
}