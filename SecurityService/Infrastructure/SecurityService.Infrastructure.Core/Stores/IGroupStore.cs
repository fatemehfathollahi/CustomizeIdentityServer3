using System;
using System.Threading.Tasks;

namespace SecurityService.Infrastructure.Core.Stores
{
	internal interface IGroupStore<TGroup, in TKey> : IDisposable where TGroup : class, IGroup<TKey>
	{
		/// <summary>
		///     Insert a new group
		/// </summary>
		/// <param name="group"></param>
		/// <returns></returns>
		void Create(TGroup group);

		/// <summary>
		///     Update a group
		/// </summary>
		/// <param name="group"></param>
		/// <returns></returns>
		void Update(TGroup group);

		/// <summary>
		///     Delete a group
		/// </summary>
		/// <param name="group"></param>
		/// <returns></returns>
		void Delete(TGroup group);

		/// <summary>
		///     Finds a group
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<TGroup> FindByIdAsync(TKey groupId);
	}
}