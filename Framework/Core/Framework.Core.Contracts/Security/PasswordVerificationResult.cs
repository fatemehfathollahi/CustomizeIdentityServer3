namespace Framework.Core.Contracts.Security
{
	public enum PasswordVerificationResult
	{
		/// <summary>
		///     Password verification failed
		/// </summary>
		Failed = 0,

		/// <summary>
		///     Success
		/// </summary>
		Success = 1,

		/// <summary>
		///     Success but should update and rehash the password
		/// </summary>
		SuccessRehashNeeded = 2
	}
}