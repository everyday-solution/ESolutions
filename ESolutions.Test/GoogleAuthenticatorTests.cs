using ESolutions.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Test
{
	/// <summary>
	/// Tests the google Authenticator class
	/// </summary>
	[TestClass]
	public class GoogleAuthenticatorTests
	{
		#region TestPinGenerationAndValidation
		/// <summary>
		/// Tests the pin generation and validation.
		/// </summary>
		[TestMethod]
		public void TestPinGenerationAndValidation()
		{
			Byte[] secretKey = GoogleAuthenticator.GenerateSecretKey();
			String secretString = Base32Encoder.ToBase32String(secretKey);
			var pin = GoogleAuthenticator.GeneratePin(secretString);

			var result = GoogleAuthenticator.Authenticate(secretString, pin);

			Assert.IsTrue(result);
		}
		#endregion
	}
}
