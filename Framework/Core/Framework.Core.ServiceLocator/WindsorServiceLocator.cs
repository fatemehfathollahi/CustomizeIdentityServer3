using Castle.Windsor;
using Framework.Core.Contracts.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.ServiceLocator
{
	public class WindsorServiceLocator : IServiceLocator
	{
		private IWindsorContainer _container;

		public WindsorServiceLocator(IWindsorContainer container)
		{
			_container = container;
		}

		public T Resolve<T>()
		{
			return _container.Resolve<T>();
		}

		public T Resolve<T>(string key)
		{
			return _container.Resolve<T>(key);
		}

		public List<T> ResolveAll<T>()
		{
			return _container.ResolveAll<T>().ToList();
		}

		public void Release(object instance)
		{
			_container.Release(instance);
		}

		public void Dispose()
		{
			if (_container != null)
			{
				_container.Dispose();
				_container = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}
	}
}