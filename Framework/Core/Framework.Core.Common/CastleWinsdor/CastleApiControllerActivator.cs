using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Framework.Core.Common.CastleWinsdor
{
	public class CastleApiControllerActivator : IHttpControllerActivator
	{
		private IWindsorContainer _container;

		public CastleApiControllerActivator(IWindsorContainer container)
		{
			_container = container;
		}

		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
		{
			return (IHttpController)_container.Resolve(controllerType);
		}
	}
}