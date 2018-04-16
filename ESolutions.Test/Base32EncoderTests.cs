using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Test
{
	[TestClass]
	public class Base32EncoderTests
	{
		#region EncodeDecode
		[TestMethod]
		public void EncodeDecode()
		{
			String testString = "Hallo Welt 123";
			Byte[] testBytes = Encoding.UTF8.GetBytes(testString);

			var base32String = Base32Encoder.ToBase32String(testBytes);
			var backConvertBytes = Base32Encoder.FromBase32String(base32String);

			string backConvertString = Encoding.UTF8.GetString(backConvertBytes);
			Assert.IsTrue(testBytes.SequenceEqual(backConvertBytes));
		}
		#endregion
	}
}
