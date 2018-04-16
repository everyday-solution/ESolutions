using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Test
{
	[TestClass]
	public class RijndaelTests
	{
		[TestMethod]
		public void TestThatTextCanByEnryptedAndDecrypted()
		{
			Security.Cryptography.Rijndael crypter = new Security.Cryptography.Rijndael();
			crypter.EncryptionSecret = "THISISIT";
			crypter.EncryptionIV = "TISISIHT";

			String excepted = "karmu123#";

			String encryptedString = crypter.Encrypt(excepted);
			String actual = crypter.Decrypt(encryptedString);

			Assert.AreEqual(excepted, actual);
		}

		[TestMethod]
		public void TestThatSerializedStringCanBeDecrypted()
		{
			String input = "pOKLHn01s2tshq8+Rjqgvo5P2VZRMFdd4KML38jqpdA=";
			Security.Cryptography.Rijndael crypter = new Security.Cryptography.Rijndael();
			crypter.EncryptionSecret = "THISISIT";
			crypter.EncryptionIV = "TISISIHT";

			String actual = crypter.Decrypt(input);

			Assert.AreEqual("karmu123#", actual);
		}

		[TestMethod]
		public void TestThatEncryptedStringCanBeDecryptedWithADifferentRijndaelCrypter()
		{
			Security.Cryptography.Rijndael crypter = new Security.Cryptography.Rijndael();
			crypter.EncryptionSecret = "THISISIT";
			crypter.EncryptionIV = "TISISIHT";

			Security.Cryptography.Rijndael decrypter = new Security.Cryptography.Rijndael();
			decrypter.EncryptionSecret = "THISISIT";
			decrypter.EncryptionIV = "TISISIHT";

			String expected = "karmu123#";

			String encrypted = crypter.Encrypt(expected);
			String actual = decrypter.Decrypt(encrypted);

			Assert.AreEqual(expected, actual);
		}
	}
}
