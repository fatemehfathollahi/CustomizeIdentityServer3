using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SecurityService.Infrastructure.Core.Stores
{
	public class GroupStoreBase<TGroup, TKey> : IGroupStore<TGroup, TKey> where TGroup : class, IGroup<TKey>
	{
		#region Fields

		private DbContext _context;
		private DbSet<TGroup> _dbSet;

		#endregion Fields

		#region Ctors

		public GroupStoreBase(DbContext context)
		{
			if (context == null)
				throw new NullReferenceException("context is Null or Empty.");

			_context = context;
			_dbSet = _context.Set<TGroup>();
		}

		#endregion Ctors

		#region IGroupStore Members

		public virtual void Create(TGroup group)
		{
			_dbSet.Add(group);

			_context.SaveChanges();
		}

		public virtual void Update(TGroup group)
		{
			if (group != null)
				_context.Entry<IGroup<TKey>>(group).State = EntityState.Modified;

			_context.SaveChanges();
		}

		public virtual void Delete(TGroup group)
		{
			_dbSet.Remove(group);

			_context.SaveChanges();
		}

		public async virtual Task<TGroup> FindByIdAsync(TKey groupId)
		{
			return await _dbSet.FindAsync(groupId);
		}

		#endregion IGroupStore Members

		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion IDisposable Members
	}
}