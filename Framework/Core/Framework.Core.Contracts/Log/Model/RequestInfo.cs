using System;

namespace Framework.Core.Contracts.Log.Model
{
	public class RequestInfo
	{
		public string TargetTypeNamespace { get; set; }
		public string TargetTypeName { get; set; }
		public string MethodName { get; set; }
		public string Arguments { get; set; }
		public Guid RequestId { get; set; }
	}
}