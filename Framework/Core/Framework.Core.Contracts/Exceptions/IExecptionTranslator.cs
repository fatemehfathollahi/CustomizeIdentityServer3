using System;

namespace Framework.Core.Contracts.Exceptions
{
	public interface IExecptionTranslator : IDisposable
	{
		Exception TranslateException(Exception exception);
	}
}