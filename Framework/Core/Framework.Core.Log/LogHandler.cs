using Framework.Core.Common.Extensions;
using Framework.Core.Contracts.Log;
using Framework.Core.Contracts.Log.Model;
using Framework.Core.Log.LogStorages;
using System;

namespace Framework.Core.Log
{
	public class LogHandler : ILogHandler
	{
		#region Fields

		private ILogFactory _logFactory;

		#endregion Fields

		#region Ctor

		public LogHandler(ILogFactory logFactory)
		{
			_logFactory = logFactory;
		}

		#endregion Ctor

		#region ILogHandler Members

		public void LogHandle(Exception exception, RequestInfo requestInfo)
		{
			LogStorage logStorage = (LogStorage)_logFactory.CreateStorage();

			using (logStorage)
			{
				LiteDB.LiteCollection<ExceptionLog> Exceptions = logStorage.GetCollection<ExceptionLog>("ExceptionsLog");

				// use extension method
				exception.GetStackTrace(out string stackTraceClassName, out string stackTraceFileName, out int stackTraceLineNumber);

				ExceptionLog newExceptionLog = new ExceptionLog
				{
					RequestInformation = requestInfo,
					Date = DateTime.Now,
					FullMessage = exception.ExtractFullMessage(),
					Message = exception.Message,
					Type = exception.GetType().Name,
					StackTraceClassName = stackTraceClassName,
					StackTraceFileName = stackTraceFileName,
					StackTraceLineNumber = stackTraceLineNumber.ToString()
				};

				Exceptions.Insert(newExceptionLog);
			}
		}

		#endregion ILogHandler Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_logFactory != null)
			{
				_logFactory.Dispose();
				_logFactory = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}