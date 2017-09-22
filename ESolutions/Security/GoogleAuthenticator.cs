using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Security
{
	/// <summary>
	/// Code to generate and verify google authenticator codes
	/// </summary>
	/// <remarks>
	/// Based on the code at http://stackoverflow.com/a/12398317/465404
	/// </remarks>
	public class GoogleAuthenticator
	{
		//Constants
		#region IntervalLength
		/// <summary>
		/// The interval length
		/// </summary>
		const int IntervalLength = 30;
		#endregion

		#region PinLength
		/// <summary>
		/// The pin length
		/// </summary>
		const int PinLength = 6;
		#endregion

		#region PinModuo
		/// <summary>
		/// The pin modulo
		/// </summary>
		static readonly int PinModulo = (int)Math.Pow(10, PinLength);
		#endregion

		#region UnixEpoch
		/// <summary>
		/// The unix epoch
		/// </summary>
		static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		#endregion

		//Properties
		#region CurrentInvterval
		/// <summary>
		/// Number of intervals that have elapsed.
		/// </summary>
		private static long CurrentInterval
		{
			get
			{
				var elapsedSeconds = (long)Math.Floor((DateTime.UtcNow - UnixEpoch).TotalSeconds);

				return elapsedSeconds / IntervalLength;
			}
		}
		#endregion

		//Methods
		#region GenerateSecretKey
		/// <summary>
		/// Generates the secret key.
		/// </summary>
		/// <returns></returns>
		public static byte[] GenerateSecretKey()
		{
			var secretKey = new byte[10];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(secretKey);
			}

			return secretKey;
		}
		#endregion

		#region GenerateProvisioningImage
		/// <summary>
		/// Generates a QR code bitmap for provisioning.
		/// </summary>
		public byte[] GenerateProvisioningImage(string identifier, byte[] key, int width, int height)
		{
			var keyString = Base32Encoder.ToBase32String(key);
			var provisionUrl = WebUtility.UrlEncode(string.Format("otpauth://totp/{0}?secret={1}", identifier, keyString));

			var chartUrl = string.Format("https://chart.apis.google.com/chart?cht=qr&chs={0}x{1}&chl={2}", width, height, provisionUrl);
			using (var client = new WebClient())
			{
				return client.DownloadData(chartUrl);
			}
		}
		#endregion

		#region GeneratePin
		/// <summary>
		/// Generates a pin for the given key.
		/// </summary>
		public static String GeneratePin(String secretKeyBase32String)
		{
			return GeneratePin(secretKeyBase32String, GoogleAuthenticator.CurrentInterval);
		}
		#endregion

		#region GeneratePin
		/// <summary>
		/// Generates a pin by hashing a key and counter.
		/// </summary>
		private static string GeneratePin(String secretKeyBase32String, long counter)
		{
			const Int32 sizeOfInt32 = 4;

			var counterBytes = BitConverter.GetBytes(counter);
			var secretKey = Base32Encoder.FromBase32String(secretKeyBase32String);

			if (BitConverter.IsLittleEndian)
			{
				//spec requires bytes in big-endian order
				Array.Reverse(counterBytes);
			}

			var hash = new HMACSHA1(secretKey).ComputeHash(counterBytes);
			var offset = hash[hash.Length - 1] & 0xF;

			var selectedBytes = new byte[sizeOfInt32];
			Buffer.BlockCopy(hash, offset, selectedBytes, 0, sizeOfInt32);

			if (BitConverter.IsLittleEndian)
			{
				//spec interprets bytes in big-endian order
				Array.Reverse(selectedBytes);
			}

			var selectedInteger = BitConverter.ToInt32(selectedBytes, 0);

			//remove the most significant bit for interoperability per spec
			var truncatedHash = selectedInteger & 0x7FFFFFFF;

			//generate number of digits for given pin length
			var pin = truncatedHash % PinModulo;

			return pin.ToString(CultureInfo.InvariantCulture).PadLeft(PinLength, '0');
		}
		#endregion

		#region Authenticate
		/// <summary>
		/// Authenticates the pin.
		/// </summary>
		/// <param name="secretKeyBase32String">The secret key base32 string.</param>
		/// <param name="pin">The pin.</param>
		/// <returns></returns>
		public static Boolean Authenticate(String secretKeyBase32String, String pin)
		{
			var currentInterval = GoogleAuthenticator.CurrentInterval;
			var secondFactorMatched = false;

			// The currentInterval +- 1 has been added to allow for devices which are slightly out of sync
			// to connect still, this does decrease the security of the application slightly but I feel that
			// the modification is an acceptable usability/security compromise.
			if (GoogleAuthenticator.GeneratePin(secretKeyBase32String, currentInterval) == pin)
			{
				secondFactorMatched = true;
			}
			else if (GoogleAuthenticator.GeneratePin(secretKeyBase32String, currentInterval + 1) == pin)
			{
				secondFactorMatched = true;
			}
			else if (GoogleAuthenticator.GeneratePin(secretKeyBase32String, currentInterval - 1) == pin)
			{
				secondFactorMatched = true;
			}

			return secondFactorMatched;
		}
		#endregion
	}
}
