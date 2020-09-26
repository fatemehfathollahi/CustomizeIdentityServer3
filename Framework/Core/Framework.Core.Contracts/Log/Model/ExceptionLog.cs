using System;

namespace Framework.Core.Contracts.Log.Model
{
	public class ExceptionLog
	{
		public DateTime Date { get; set; }
		public string Message { get; set; }
		public string FullMessage { get; set; }
		public string Type { get; set; }
		public string StackTraceClassName { get; set; }
		public string StackTraceFileName { get; set; }
		public string StackTraceLineNumber { get; set; }
		public RequestInfo RequestInformation { get; set; }
	}
}