using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace ESolutions.Core.Security.Cryptography
{
	/// <summary>
	/// Rijndael synchronous encrypter.
	/// </summary>
	public class Rijndael
	{
		//Properties
		#region EncryptionSecret
		/// <summary>
		/// Gets or sets the encryption secret.
		/// </summary>
		/// <value>
		/// The encryption secret.
		/// </value>
		public String EncryptionSecret
		{
			get;
			set;
		}
		#endregion

		#region EncryptionIV
		/// <summary>
		/// Gets or sets the encryption IV.
		/// </summary>
		/// <value>
		/// The encryption IV.
		/// </value>
		public String EncryptionIV
		{
			get;
			set;
		}
		#endregion

		//Methods
		#region Encrypt
		/// <summary>
		/// Encrypts the specified clear text.
		/// </summary>
		/// <param name="clearText">The clear text.</param>
		/// <returns></returns>
		public String Encrypt(String clearText)
		{
			var clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

			var pdb = new PasswordDeriveBytes(
				Encoding.Unicode.GetBytes(this.EncryptionSecret),
				Encoding.Unicode.GetBytes(this.EncryptionIV));

			var alg = Aes.Create();
			alg.Key = pdb.GetBytes(32);
			alg.IV = pdb.GetBytes(16);

			var ms = new MemoryStream();
			var cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(clearBytes, 0, clearBytes.Length);
			cs.Close();

			var encryptedData = ms.ToArray();
			return Convert.ToBase64String(encryptedData);
		}
		#endregion

		#region Decrypt
		/// <summary>
		/// Decrypts the specified cipher text.
		/// </summary>
		/// <param name="cipherText">The cipher text.</param>
		/// <returns></returns>
		public String Decrypt(String cipherText)
		{
			var pdb = new PasswordDeriveBytes(
				Encoding.Unicode.GetBytes(this.EncryptionSecret),
				Encoding.Unicode.GetBytes(this.EncryptionIV));

			var alg = Aes.Create();
			alg.Key = pdb.GetBytes(32);
			alg.IV = pdb.GetBytes(16);

			var ms = new MemoryStream();
			var cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
			var cypherBytes = Convert.FromBase64String(cipherText);
			cs.Write(cypherBytes, 0, cypherBytes.Length);
			cs.Close();

			byte[] decryptedData = ms.ToArray();
			return System.Text.Encoding.Unicode.GetString(decryptedData);
		}
		#endregion
	}
}
