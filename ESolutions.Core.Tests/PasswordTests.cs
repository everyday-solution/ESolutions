using System;
using ESolutions.Core.Security.Cryptography;
using Xunit;

namespace ESolutions.Core.Test
{
	public class PasswordTests
	{
		[Fact]
		public void TestThatHashIsComputedCorrectlyIfPasswordShorterSalt()
		{
			var hash = Security.Cryptography.PasswordHash.GetSecurePasswordHash("test", "key");
			Assert.NotNull(hash);
			Assert.NotNull(hash.Hash);
			Assert.NotNull(hash.Salt);
		}

		[Fact]
		public void TestThatHashIsComutedCorrectylIfPasswordLongerSalt()
		{
			var hash = Security.Cryptography.PasswordHash.GetSecurePasswordHash("testtesttesttesttesttesttesttesttesttesttesttesttesttest", "key");
			Assert.NotNull(hash);
			Assert.NotNull(hash.Hash);
			Assert.NotNull(hash.Salt);
		}

		[Fact]
		public void TestThatHashIsComputedWithExternalSalt()
		{
			var hashCreated = Security.Cryptography.PasswordHash.GetSecurePasswordHash("test", "key");
			var hashLoaded = Security.Cryptography.PasswordHash.GetSecurePasswordHash("test", hashCreated.Salt, "key");
			Assert.NotNull(hashCreated);
			Assert.NotNull(hashLoaded);
			Assert.NotNull(hashCreated.Hash);
			Assert.NotNull(hashLoaded.Hash);
			Assert.Equal(hashCreated.Hash, hashLoaded.Hash);
		}

		[Fact]
		public void TestSHA512Hash()
		{
			var hashCreated = Security.Cryptography.PasswordHash.GetSaltedSHA512Hash("test");
			var hashLoaded = Security.Cryptography.PasswordHash.GetSaltedSHA512Hash("test", hashCreated.Salt);
			Assert.NotNull(hashCreated);
			Assert.NotNull(hashLoaded);
			Assert.NotNull(hashCreated.Hash);
			Assert.NotNull(hashLoaded.Hash);
			Assert.Equal(hashCreated.Hash, hashLoaded.Hash);
		}
	}
}
