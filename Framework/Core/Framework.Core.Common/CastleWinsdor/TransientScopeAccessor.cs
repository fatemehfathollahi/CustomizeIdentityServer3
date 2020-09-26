using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;
using System;

namespace Framework.Core.Common.CastleWinsdor
{
	internal class TransientScopeAccessor : IScopeAccessor
	{
		#region IScopeAccessor Members

		public ILifetimeScope GetScope(CreationContext context)
		{
			return new DefaultLifetimeScope();
		}

		#endregion IScopeAccessor Members

		#region IDisposable Members

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}