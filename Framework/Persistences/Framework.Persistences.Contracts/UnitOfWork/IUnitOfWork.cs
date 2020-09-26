using Framework.Persistences.Contracts.Infrastructure;
using System;

namespace Framework.Persistences.Contracts.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

		void Commit();

		void Rollback();
	}
}