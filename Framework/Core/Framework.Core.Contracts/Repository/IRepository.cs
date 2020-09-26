using Framework.Core.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Core.Contracts.Repository
{
	public interface IRepository : IDisposable
	{
	}

	public interface IRepository<TKey, TEntity> : IRepository where TEntity : class, IEntityObject
	{
		TEntity Get(TKey id);

		List<TEntity> GetAll();

		IQueryable<TEntity> GetAllAsQuery();

		List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

		void Insert(TEntity entity);

		void Update(TEntity entity);

		void Delete(TKey id);

		void Delete(TEntity entity);
	}
}