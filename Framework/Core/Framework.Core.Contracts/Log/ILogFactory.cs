using System;

namespace Framework.Core.Contracts.Log
{
	public interface ILogFactory : IDisposable
	{
		ILogStorage CreateStorage();
	}
}