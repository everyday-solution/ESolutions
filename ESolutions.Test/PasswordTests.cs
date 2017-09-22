﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESolutions.Security.Cryptography;

namespace ESolutions.Test
{
	[TestClass]
	public class PasswordTests
	{
		[TestMethod]
		public void TestThatHashIsComputedCorrectlyIfPasswordShorterSalt()
		{
			var hash = Security.Cryptography.PasswordHash.GetSaltedSHA512Hash("test");
			Assert.IsNotNull(hash);
			Assert.IsNotNull(hash.Hash);
			Assert.IsNotNull(hash.Salt);
		}

		[TestMethod]
		public void TestThatHashIsComutedCorrectylIfPasswordLongerSalt()
		{
			var hash = Security.Cryptography.PasswordHash.GetSaltedSHA512Hash("testtesttesttesttesttesttesttesttesttesttesttesttesttest");
			Assert.IsNotNull(hash);
			Assert.IsNotNull(hash.Hash);
			Assert.IsNotNull(hash.Salt);
		}

		[TestMethod]
		public void TestThatHashIsComputedWithExternalSalt()
		{
			var hashCreated = Security.Cryptography.PasswordHash.GetSaltedSHA512Hash("test");
			var hashLoaded = Security.Cryptography.PasswordHash.GetSaltedSHA512Hash("test", hashCreated.Salt);
			Assert.IsNotNull(hashCreated);
			Assert.IsNotNull(hashLoaded);
			Assert.IsNotNull(hashCreated.Hash);
			Assert.IsNotNull(hashLoaded.Hash);
			Assert.AreEqual(hashCreated.Hash, hashLoaded.Hash);
		}
	}
}
