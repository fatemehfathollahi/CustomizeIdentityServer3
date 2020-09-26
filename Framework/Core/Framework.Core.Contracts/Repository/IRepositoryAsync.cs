using Framework.Core.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Core.Contracts.Repository
{
	public interface IRepositoryAsync<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IEntityObject
	{
		Task<TEntity> GetAsync(TKey id);

		Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);

		Task<List<TEntity>> GetAllAsync();

		Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

		Task<IQueryable<TEntity>> GetAllAsQueryAsync();

		Task<IQueryable<TEntity>> GetAllAsQueryAsync(CancellationToken cancellationToken);

		Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

		Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

		Task InsertAsync(TEntity entity);

		Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

		Task UpdateAsync(TEntity entity);

		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

		Task DeleteAsync(TKey id);

		Task DeleteAsync(TKey id, CancellationToken cancellationToken);

		Task DeleteAsync(TEntity entity);

		Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

		void DisableAutoDetectChanges();

		void EnableAutoDetectChanges();
	}
}