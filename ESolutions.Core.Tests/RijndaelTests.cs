using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESolutions.Core.Test
{
	public class RijndaelTests
	{
		[Fact]
		public void TestThatTextCanByEnryptedAndDecrypted()
		{
			Security.Cryptography.Rijndael crypter = new Security.Cryptography.Rijndael();
			crypter.EncryptionSecret = "THISISIT";
			crypter.EncryptionIV = "TISISIHT";

			String excepted = "karmu123#";

			String encryptedString = crypter.Encrypt(excepted);
			String actual = crypter.Decrypt(encryptedString);

			Assert.Equal(excepted, actual);
		}

		[Fact]
		public void TestThatSerializedStringCanBeDecrypted()
		{
			String input = "pOKLHn01s2tshq8+Rjqgvo5P2VZRMFdd4KML38jqpdA=";
			Security.Cryptography.Rijndael crypter = new Security.Cryptography.Rijndael();
			crypter.EncryptionSecret = "THISISIT";
			crypter.EncryptionIV = "TISISIHT";

			String actual = crypter.Decrypt(input);

			Assert.Equal("karmu123#", actual);
		}

		[Fact]
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

			Assert.Equal(expected, actual);
		}
	}
}
