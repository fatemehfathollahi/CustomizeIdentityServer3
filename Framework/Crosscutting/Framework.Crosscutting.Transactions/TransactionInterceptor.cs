using Castle.DynamicProxy;
using Framework.Core.Common.Attributes;
using Framework.Crosscutting.Contracts.Infrastructure;
using Framework.Persistences.Contracts.UnitOfWork;
using System;
using System.Linq;

namespace Framework.Crosscutting.Transactions
{
	public class TransactionInterceptor : ICrosscutting
	{
		#region Fields

		private IUnitOfWork _unitOfWork;

		#endregion Fields

		#region Ctors

		public TransactionInterceptor(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#endregion Ctors

		#region ICrosscutting Member

		public void Intercept(IInvocation invocation)
		{
			System.Collections.Generic.IEnumerable<System.Reflection.CustomAttributeData> customAttributes = invocation.MethodInvocationTarget.CustomAttributes;
			System.Reflection.MethodInfo interfaceIDisposable = invocation.Method;

			if (customAttributes.Any(a => a.AttributeType == typeof(NonTransactionalAttribute))
				|| interfaceIDisposable.Name.Equals("Dispose"))
			{
				invocation.Proceed();
			}
			else
			{
				try
				{
					_unitOfWork.BeginTransaction();
					invocation.Proceed();
					_unitOfWork.Commit();
				}
				catch (Exception ex)
				{
					_unitOfWork.Rollback();

					throw ex;
				}
			}
		}

		#endregion ICrosscutting Member
	}
}