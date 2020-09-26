namespace SecurityService.Infrastructure.Core
{
	public interface IGroup<out TKey>
	{
		/// <summary>
		///     Unique key for the user
		/// </summary>
		TKey Id { get; }
	}
}