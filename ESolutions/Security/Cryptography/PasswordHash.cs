using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ESolutions.Security.Cryptography
{
	/// <summary>
	/// A salted SHA512 hash.
	/// </summary>
	public class PasswordHash
	{
		//Properties
		#region Hash
		/// <summary>
		/// Gets or sets the hash.
		/// </summary>
		/// <value>
		/// The hash.
		/// </value>
		public String Hash
		{
			get;
			set;
		}
		#endregion

		#region Salt
		/// <summary>
		/// Gets or sets the salt.
		/// </summary>
		/// <value>
		/// The salt.
		/// </value>
		public String Salt
		{
			get;
			set;
		}
		#endregion

		//Methods
		#region GetSaltedSHA512Hash
		/// <summary>
		/// Gets the salted sh a512 hash.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public static PasswordHash GetSaltedSHA512Hash(String password)
		{
			var salt = Guid.NewGuid().ToString();
			return PasswordHash.GetSaltedSHA512Hash(password, salt);
		}
		#endregion

		#region GetSaltedSHA512Hash
		/// <summary>
		/// Gets the salted sh a512 hash.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static PasswordHash GetSaltedSHA512Hash(String password, String salt)
		{
			StringBuilder saltedPassword = new StringBuilder();

			Int32 saltIndex = 0;
			foreach (var runner in password)
			{
				saltedPassword.Append(runner + salt[saltIndex % salt.Length]);
				saltedPassword.Append(salt[saltIndex % salt.Length]);
			}

			var hash = SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(saltedPassword.ToString()));
			return new PasswordHash() { Hash = Encoding.Unicode.GetString(hash), Salt = salt };
		}
		#endregion
	}
}
