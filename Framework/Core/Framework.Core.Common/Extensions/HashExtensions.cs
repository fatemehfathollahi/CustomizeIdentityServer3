using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Core.Common.Extensions
{
	/// <summary>
	/// Extension methods for hashing strings and byte arrays
	/// </summary>
	public static class HashExtensions
	{
		/// <summary>
		/// Creates a SHA256 hash of the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A hash</returns>
		public static string Sha256(this string input)
		{
			if (input.IsMissing())
			{
				return string.Empty;
			}

			using (SHA256 sha = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(input);
				byte[] hash = sha.ComputeHash(bytes);

				return Convert.ToBase64String(hash);
			}
		}

		/// <summary>
		/// Creates a SHA256 hash of the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A hash.</returns>
		public static byte[] Sha256(this byte[] input)
		{
			if (input == null)
			{
				return null;
			}

			using (SHA256 sha = SHA256.Create())
			{
				return sha.ComputeHash(input);
			}
		}

		/// <summary>
		/// Creates a SHA512 hash of the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>A hash</returns>
		public static string Sha512(this string input)
		{
			if (input.IsMissing())
			{
				return string.Empty;
			}

			using (SHA512 sha = SHA512.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(input);
				byte[] hash = sha.ComputeHash(bytes);

				return Convert.ToBase64String(hash);
			}
		}
	}
}