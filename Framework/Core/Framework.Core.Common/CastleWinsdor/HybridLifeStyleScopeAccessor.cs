using Castle.Facilities.WcfIntegration.Lifestyles;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Lifestyle.Scoped;
using System;
using System.ServiceModel;
using System.Web;

namespace Framework.Core.Common.CastleWinsdor
{
	public class HybridLifeStyleScopeAccessor : IScopeAccessor
	{
		#region Fields

		private IScopeAccessor _WcfOperationScopeAccessor = new WcfOperationScopeAccessor();
		private IScopeAccessor _WebRequestScopeAccessor = new WebRequestScopeAccessor();
		private IScopeAccessor _TransientScopeAccessor = new TransientScopeAccessor();

		#endregion Fields

		#region IScopeAccessor Members

		///<summary>
		/// If Wcf Opertaion Context is available then returns WcfOperation Lifetime Scope
		/// Else If HttpContext is avilable then return WebRequest Lifetime Scope
		/// Else Returns Transient LifetimeScope
		/// </summary>
		public ILifetimeScope GetScope(CreationContext context)
		{
			if (OperationContext.Current != null)
			{
				return _WcfOperationScopeAccessor.GetScope(context);
			}
			else if (HttpContext.Current != null)
			{
				return _WebRequestScopeAccessor.GetScope(context);
			}
			else
			{
				return _TransientScopeAccessor.GetScope(context);
			}
		}

		#endregion IScopeAccessor Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_WcfOperationScopeAccessor != null)
			{
				_WcfOperationScopeAccessor.Dispose();
				_WcfOperationScopeAccessor = null;
			}

			if (_WebRequestScopeAccessor != null)
			{
				_WebRequestScopeAccessor.Dispose();
				_WebRequestScopeAccessor = null;
			}

			if (_TransientScopeAccessor != null)
			{
				_TransientScopeAccessor.Dispose();
				_TransientScopeAccessor = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}