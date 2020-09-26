using Framework.Core.Contracts.Log.Model;
using System;

namespace Framework.Core.Contracts.Log
{
	public interface ILogHandler : IDisposable
	{
		void LogHandle(Exception exception, RequestInfo requestInfo);
	}
}