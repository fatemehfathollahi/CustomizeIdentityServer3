using Framework.Core.Contracts.Exceptions;
using Framework.Core.Contracts.Log;
using Framework.Core.Contracts.Log.Model;
using System;

namespace Framework.Core.Exception
{
	public class ExceptionHandler : IExceptionHandler
	{
		#region Fields

		private IExecptionTranslator _exceptionTranslator;
		private ILogHandler _logHandler;

		#endregion Fields

		#region Ctor(s)

		public ExceptionHandler(IExecptionTranslator exceptionTranslator, ILogHandler logHandler)
		{
			_exceptionTranslator = exceptionTranslator;
			_logHandler = logHandler;
		}

		#endregion Ctor(s)

		#region IExceptionHandler Members

		public System.Exception Handle(System.Exception exception, RequestInfo requestInfo)
		{
			_logHandler.LogHandle(exception, requestInfo);

			return _exceptionTranslator.TranslateException(exception);
		}

		#endregion IExceptionHandler Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_exceptionTranslator != null)
			{
				_exceptionTranslator.Dispose();
				_exceptionTranslator = null;
			}

			if (_logHandler != null)
			{
				_logHandler.Dispose();
				_logHandler = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}