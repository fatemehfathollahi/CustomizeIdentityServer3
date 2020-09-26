using System;
using System.Diagnostics;
using System.Text;

namespace Framework.Core.Common.Extensions
{
	public static class ExceptionExtensions
	{
		public static string ExtractFullMessage(this Exception ex)
		{
			StringBuilder fullMessage = new StringBuilder(ex.Message);
			Exception exception = ex.InnerException;

			while (exception != null)
			{
				fullMessage.Append(exception.Message);

				exception = exception.InnerException;
			}

			return fullMessage.ToString();
		}

		public static string GetInnerTypeFullName(this Exception exception)
		{
			while (exception.InnerException != null)
			{
				exception = exception.InnerException;
			}

			return exception.GetType().FullName;
		}

		public static string GetInnerMessage(this Exception exception)
		{
			while (exception.InnerException != null)
			{
				exception = exception.InnerException;
			}

			return exception.Message;
		}

		public static Type GetInnerType(this Exception exception)
		{
			while (exception.InnerException != null)
			{
				exception = exception.InnerException;
			}

			return exception.GetType();
		}

		public static void GetStackTrace(this Exception exception, out string stackTraceClassName, out string stackTraceFileName, out int stackTraceLineNumber)
		{
			try
			{
				StackTrace stackTrace = new StackTrace(exception, true);
				StackFrame frame = null;

				for (int i = 0; i < stackTrace.FrameCount; i++)
				{
					frame = stackTrace.GetFrame(i);
					if (frame.GetFileLineNumber() > 0)
					{
						break;
					}
				}

				string stackTraceString = exception.StackTrace;
				stackTraceClassName = stackTraceString.Substring(0, stackTraceString.IndexOf(" in ", StringComparison.Ordinal));
				stackTraceClassName = stackTraceClassName.Remove(0, stackTraceClassName.LastIndexOf("at ", StringComparison.Ordinal) + 3);

				stackTraceFileName = frame.GetFileName();
				stackTraceLineNumber = frame.GetFileLineNumber();

				//var methodName = frame.GetMethod().Name;
				//var col = frame.GetFileColumnNumber();
			}
			catch
			{
				stackTraceClassName = "";
				stackTraceFileName = "";
				stackTraceLineNumber = 0;
			}
		}
	}
}