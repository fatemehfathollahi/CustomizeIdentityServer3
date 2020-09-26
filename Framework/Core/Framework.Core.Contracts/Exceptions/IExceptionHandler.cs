using Framework.Core.Contracts.Log.Model;
using System;

namespace Framework.Core.Contracts.Exceptions
{
	public interface IExceptionHandler : IDisposable
	{
		Exception Handle(Exception exception, RequestInfo requestInfo);
	}
}