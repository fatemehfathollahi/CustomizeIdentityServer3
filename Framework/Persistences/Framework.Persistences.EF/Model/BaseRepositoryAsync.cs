using Framework.Core.Contracts.Model;
using Framework.Core.Contracts.Repository;
using Framework.Persistences.EF.DataStore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistences.EF.Model
{
	public abstract class BaseRepositoryAsync<TKey, TEntity> : IRepositoryAsync<TKey, TEntity> where TEntity : class, IEntityObject
	{
		#region Fields

		private DataContext _context;
		private DbSet<TEntity> _dbSet;

		#endregion Fields

		#region Ctor

		public BaseRepositoryAsync(DataContext dataContext)
		{
			_context = dataContext;

			_dbSet = _context.Set<TEntity>();
		}

		#endregion Ctor

		#region IRepository Members

		public TEntity Get(TKey id)
		{
			return _dbSet.Find(id);
		}

		public List<TEntity> GetAll()
		{
			return _dbSet.ToList();
		}

		public IQueryable<TEntity> GetAllAsQuery()
		{
			return _dbSet.AsQueryable();
		}

		public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.Where(predicate).ToList();
		}

		public void Insert(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void Update(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void Delete(TKey id)
		{
			TEntity entity = _dbSet.Find(id);
			Delete(entity);
		}

		public void DisableAutoDetectChanges()
		{
			_context.Configuration.AutoDetectChangesEnabled = false;
		}

		public void EnableAutoDetectChanges()
		{
			_context.Configuration.AutoDetectChangesEnabled = true;
		}

		#endregion IRepository Members

		#region IRepositoryAsync Members

		public Task<TEntity> GetAsync(TKey id)
		{
			return GetAsync(id, CancellationToken.None);
		}

		public Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken)
		{
			return _dbSet.FindAsync(cancellationToken, id);
		}

		public Task<List<TEntity>> GetAllAsync()
		{
			return GetAllAsync(CancellationToken.None);
		}

		public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			return _dbSet.ToListAsync(cancellationToken);
		}

		public Task<IQueryable<TEntity>> GetAllAsQueryAsync()
		{
			return GetAllAsQueryAsync(CancellationToken.None);
		}

		public Task<IQueryable<TEntity>> GetAllAsQueryAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult(GetAllAsQuery());
		}

		public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await GetAsync(predicate, CancellationToken.None);
		}

		public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
		{
			return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
		}

		public async Task InsertAsync(TEntity entity)
		{
			await InsertAsync(entity, CancellationToken.None);
		}

		public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
		{
			await Task.Run(() => { Insert(entity); }, cancellationToken);
		}

		public async Task UpdateAsync(TEntity entity)
		{
			await UpdateAsync(entity, CancellationToken.None);
		}

		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			await Task.Run(() => { Update(entity); }, cancellationToken);
		}

		public async Task DeleteAsync(TEntity entity)
		{
			await DeleteAsync(entity, CancellationToken.None);
		}

		public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
		{
			await Task.Run(() => { Delete(entity); }, cancellationToken);
		}

		public async Task DeleteAsync(TKey id)
		{
			await DeleteAsync(id, CancellationToken.None);
		}

		public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
		{
			TEntity entity = await GetAsync(id, cancellationToken);

			if (entity == null)
			{
				throw new Exception("Entity Not Found.");
			}

			await Task.Run(() => { Delete(entity); }, cancellationToken);
		}

		#endregion IRepositoryAsync Members

		#region IDisposable Members

		public void Dispose()
		{
			if (_dbSet != null)
			{
				_dbSet = null;
			}

			if (_context != null)
			{
				_context.Dispose();
				_context = null;
			}

			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}