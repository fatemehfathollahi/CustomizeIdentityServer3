using Castle.Windsor;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Framework.Core.Common.CastleWinsdor
{
	public class WindsorControllerFactory : DefaultControllerFactory
	{
		private readonly IWindsorContainer _container;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="container"></param>
		public WindsorControllerFactory(IWindsorContainer container)
		{
			_container = container;
		}

		/// <summary>
		/// Release the controller at the end of it's life cycle
		/// </summary>
		/// <param name="controller">The Interface to an MVC controller</param>
		public override void ReleaseController(IController controller)
		{
			_container.Release(controller);
		}

		/// <summary>
		/// Resolve a controller dependency
		/// </summary>
		/// <param name="requestContext">The HTTP context</param>
		/// <param name="controllerType">Type of controller to resolve</param>
		/// <returns>IController</returns>
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.",
					requestContext.HttpContext.Request.Path));
			}
			return (IController)_container.Resolve(controllerType);
		}
	}
}