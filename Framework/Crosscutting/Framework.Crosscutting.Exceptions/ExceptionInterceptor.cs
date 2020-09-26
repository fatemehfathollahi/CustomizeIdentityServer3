using Castle.DynamicProxy;
using Framework.Core.Common.Exceptions;
using Framework.Core.Contracts.Exceptions;
using Framework.Core.Contracts.Log.Model;
using Framework.Crosscutting.Contracts.Infrastructure;
using System;

namespace Framework.Crosscutting.Exceptions
{
	public class ExceptionInterceptor : ICrosscutting
	{
		#region Fields

		private IExceptionHandler _exceptionHandler;

		#endregion Fields

		#region Ctors

		public ExceptionInterceptor(IExceptionHandler exceptionHandler)
		{
			_exceptionHandler = exceptionHandler;
		}

		#endregion Ctors

		#region ICrosscutting Members

		public void Intercept(IInvocation invocation)
		{
			try
			{
				invocation.Proceed();
			}
			catch (Exception ex)
			{
				if (ex.GetType().BaseType == typeof(BaseException))
				{
					throw ex;
				}

				throw _exceptionHandler.Handle(ex, GetRequestInfo(invocation));
			}
		}

		#endregion ICrosscutting Members

		#region Private Members

		private RequestInfo GetRequestInfo(IInvocation invocation)
		{
			RequestInfo oInfo = new RequestInfo
			{
				RequestId = Guid.NewGuid(),
				TargetTypeNamespace = invocation.TargetType.Namespace,
				TargetTypeName = invocation.TargetType.Name,
				MethodName = invocation.Method.Name,
			};

			return oInfo;
		}

		#endregion Private Members
	}
}