using ESolutions.Core.Security;
using System;
using Xunit;

namespace ESolutions.Core.Test
{
	/// <summary>
	/// Tests the google Authenticator class
	/// </summary>
	public class GoogleAuthenticatorTests
	{
		#region TestPinGenerationAndValidation
		/// <summary>
		/// Tests the pin generation and validation.
		/// </summary>
		[Fact]
		public void TestPinGenerationAndValidation()
		{
			Byte[] secretKey = GoogleAuthenticator.GenerateSecretKey();
			String secretString = Base32Encoder.ToBase32String(secretKey);
			var pin = GoogleAuthenticator.GeneratePin(secretString);

			var result = GoogleAuthenticator.Authenticate(secretString, pin);

			Assert.True(result);
		}
		#endregion
	}
}
