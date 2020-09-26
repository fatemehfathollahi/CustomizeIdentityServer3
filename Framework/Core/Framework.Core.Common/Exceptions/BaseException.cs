using System;
using System.Runtime.Serialization;

namespace Framework.Core.Common.Exceptions
{
	public class BaseException : Exception
	{
		public BaseException()
		{
		}

		public BaseException(string message) : base(message)
		{
		}

		public BaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}