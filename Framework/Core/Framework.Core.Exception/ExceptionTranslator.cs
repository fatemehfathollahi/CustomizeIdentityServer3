using Framework.Core.Common.Exceptions;
using Framework.Core.Contracts.Exceptions;
using System;

namespace Framework.Core.Exception
{
	public class ExceptionTranslator : IExecptionTranslator
	{
		#region IExecptionTranslator Members

		public System.Exception TranslateException(System.Exception exception)
		{
			return new BaseException("خطایی در سمت سرور رخ داده است لطفا با مدیر سیستم تماس حاصل فرمایید.");
		}

		#endregion IExecptionTranslator Members

		#region IDisposable Members

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}