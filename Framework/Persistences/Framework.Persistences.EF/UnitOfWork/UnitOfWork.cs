using Framework.Persistences.Contracts.Infrastructure;
using Framework.Persistences.Contracts.UnitOfWork;
using Framework.Persistences.EF.DataStore;
using System;
using System.Data.Entity;

namespace Framework.Persistences.EF.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		#region Fields

		private DataContext _context;
		private DbContextTransaction _transaction;

		#endregion Fields

		#region Ctor

		public UnitOfWork(DataContext context)
		{
			_context = context;
		}

		#endregion Ctor

		#region IUnitOfWork Members

		public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
		{
			System.Data.IsolationLevel oIsolationLevel;

			switch (isolationLevel)
			{
				case IsolationLevel.Serializable:
					{
						oIsolationLevel = System.Data.IsolationLevel.Serializable;

						break;
					}
				case IsolationLevel.RepeatableRead:
					{
						oIsolationLevel = System.Data.IsolationLevel.RepeatableRead;

						break;
					}
				case IsolationLevel.ReadCommitted:
					{
						oIsolationLevel = System.Data.IsolationLevel.ReadCommitted;

						break;
					}
				case IsolationLevel.ReadUncommitted:
					{
						oIsolationLevel = System.Data.IsolationLevel.ReadUncommitted;

						break;
					}
				case IsolationLevel.Snapshot:
					{
						oIsolationLevel = System.Data.IsolationLevel.Snapshot;

						break;
					}
				case IsolationLevel.Chaos:
					{
						oIsolationLevel = System.Data.IsolationLevel.Chaos;

						break;
					}
				case IsolationLevel.Unspecified:
					{
						oIsolationLevel = System.Data.IsolationLevel.Unspecified;

						break;
					}
				default:
					{
						oIsolationLevel = System.Data.IsolationLevel.Unspecified;

						break;
					}
			}

			_transaction = _context.Database.BeginTransaction(oIsolationLevel);
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}

		public void Commit()
		{
			_context.SaveChanges();

			_transaction.Commit();
		}

		#endregion IUnitOfWork Members

		#region IDisposeble Member

		public void Dispose()
		{
			if (_context != null)
			{
				_context.Dispose();
				_context = null;
			}

			if (_transaction != null)
			{
				_transaction.Dispose();
				_transaction = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposeble Member
	}
}