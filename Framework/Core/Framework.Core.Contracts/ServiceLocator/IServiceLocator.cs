using System;
using System.Collections.Generic;

namespace Framework.Core.Contracts.ServiceLocator
{
	public interface IServiceLocator : IDisposable
	{
		T Resolve<T>();

		T Resolve<T>(string key);

		List<T> ResolveAll<T>();

		void Release(object instance);
	}
}