using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace ESolutions.Test
{
	/// <summary>
	/// Summary description for StringExtenderTest
	/// </summary>
	[TestClass]
	public class StringExtenderTest
	{
		//Fields
		#region testContextInstance
		private TestContext testContextInstance;
		#endregion

		//Proeprties
		#region TestContext
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#endregion

		//Constructors
		#region StringExtenderTest
		public StringExtenderTest()
		{
		}
		#endregion

		//Methods
		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		#region ConvertValidInt
		[TestMethod]
		public void ConvertValidInt()
		{
			Int32 check = 33;
			Int32 result = "33".ToInt32();

			Assert.AreEqual(check, result);
		}
		#endregion

		#region ConvertValidNegativeInt
		[TestMethod]
		public void ConvertValidNegativeInt()
		{
			Int32 check = -33;
			Int32 result = "-33".ToInt32();

			Assert.AreEqual(check, result);
		}
		#endregion

		#region ConvertInvalidInt
		[TestMethod]
		[ExpectedException(typeof(ConverterException))]
		public void ConvertInvalidInt()
		{
			Int32 result = "test".ToInt32();
		}
		#endregion

		#region ConvertValidDouble
		[TestMethod]
		public void ConvertValidDouble()
		{
			Double result = "4,55".ToDouble(new CultureInfo("de-DE"));
			Assert.AreEqual(4.55, result);
		}
		#endregion

		#region ConvertValidDoubleEnUs
		[TestMethod]
		public void ConvertValidDoubleEnUS()
		{
			Double result = "4.55".ToDouble(new CultureInfo("en-US"));
			Assert.AreEqual(4.55, result);
		}
		#endregion

		#region ConvertInvalidDouble
		[TestMethod]
		public void ConvertInvalidDouble()
		{
			Double result = "4,55aa".ToDouble(new CultureInfo("de-DE"));
			Assert.AreEqual(4.55, result);
		}
		#endregion

		#region ConvertValidNegativeDouble
		[TestMethod]
		public void ConvertValidNegativeDouble()
		{
			Double result = "-4,55".ToDouble(new CultureInfo("de-DE"));
			Assert.AreEqual(-4.55, result);
		}
		#endregion

		#region TestThatUnderscoreConvertsCamelcaseTextToUnderscoredText
		[TestMethod]
		public void TestThatUnderscoreConvertsCamelcaseTextToUnderscoredText()
		{
			String testText = "activeRecord";

			Assert.AreEqual("active_record", testText.Underscore());
		}
		#endregion

		#region TestThatUnderscoreConvertsPascalcaseTextToUnderscoredText
		[TestMethod]
		public void TestThatUnderscoreConvertsPascalcaseTextToUnderscoredText()
		{
			String testText = "ActiveRecord";

			Assert.AreEqual("active_record", testText.Underscore());
		}
		#endregion

		#region TestThatCamelizeConvertsUnderscoredTextToCamelizedText
		[TestMethod]
		public void TestThatCamelizeConvertsUnderscoredTextToCamelizedText()
		{
			String testText = "active_record";

			Assert.AreEqual("activeRecord", testText.Camelize());
		}
		#endregion

		#region TestThatPascalizeConvertsUnderscoredTextToPascalizedText
		[TestMethod]
		public void TestThatPascalizeConvertsUnderscoredTextToPascalizedText()
		{
			String testText = "active_record";

			Assert.AreEqual("ActiveRecord", testText.Pascalize());
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithoutMarker
		[TestMethod]
		public void TestThatSplitMethodSplitsStringsWithoutMarker()
		{
			String input = "Hage de Maulwurfn";
			List<String> parts = input.Split(";");

			Assert.AreEqual(1, parts.Count);
			Assert.AreEqual(input, parts[0]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithMarkers
		[TestMethod]
		public void TestThatSplitMethodSplitsStringsWithMarkers()
		{
			String input = "Hage de;Maulwurfn";
			List<String> parts = input.Split(";");

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual("Hage de", parts[0]);
			Assert.AreEqual("Maulwurfn", parts[1]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithMarkersInFront
		[TestMethod]
		public void TestThatSplitMethodSplitsStringsWithMarkersInFront()
		{
			String input = ";;Hage de Maulwurfn";
			List<String> parts = input.Split(";");

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual("Hage de Maulwurfn", parts[2]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithMarkersInBack
		[TestMethod]
		public void TestThatSplitMethodSplitsStringsWithMarkersInBack()
		{
			String input = "Hage de Maulwurfn;;";
			List<String> parts = input.Split(";");

			Assert.AreEqual(3, parts.Count);
			Assert.AreEqual("Hage de Maulwurfn", parts[0]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsLeavingBlankLinesOut
		[TestMethod]
		public void TestThatSplitMethodSplitsStringsLeavingBlankLinesOut()
		{
			String input = "Hage de Maulwurfn;;;;";
			List<String> parts = input.Split(";", false);

			Assert.AreEqual(1, parts.Count);
			Assert.AreEqual("Hage de Maulwurfn", parts[0]);
		}
		#endregion

		#region TestThatSplitMethodsSplitsStringWithSouroundings
		[TestMethod]
		public void TestThatSplitMethodsSplitsStringWithSouroundings()
		{
			String input = @"""HAGE"";""Maulwurfn""";
			List<String> parts = input.Split(";", "\"");

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(@"""HAGE""", parts[0]);
			Assert.AreEqual(@"""Maulwurfn""", parts[1]);
		}
		#endregion

		#region TestThatSplitMethodsSplitsStringWithSouroundingsAndSplitMarkersNotSourrounded
		[TestMethod]
		public void TestThatSplitMethodsSplitsStringWithSouroundingsAndSplitMarkersNotSourrounded()
		{
			String input = @"""HA;GE"";""Maulw;urfn""";
			List<String> parts = input.Split(";", "\"");

			Assert.AreEqual(2, parts.Count);
			Assert.AreEqual(@"""HA;GE""", parts[0]);
			Assert.AreEqual(@"""Maulw;urfn""", parts[1]);
		}
		#endregion

		#region TestIsBooleanOk
		[TestMethod]
		public void TestIsBooleanOk()
		{
			String test = "TRUE";
			Assert.IsTrue(test.IsBoolean());
		}
		#endregion

		#region TestIsBooleanFail
		[TestMethod]
		public void TestIsBooleanFail()
		{
			String test = "asd";
			Assert.IsFalse(test.IsBoolean());
		}
		#endregion

		#region TestIsGuidFromStringOk
		[TestMethod]
		public void TestIsGuidFromStringOk()
		{
			String test = Guid.NewGuid().ToString();
			Assert.IsTrue(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromStringOk
		[TestMethod]
		public void TestIsEmptyGuidFromStringOk()
		{
			String test = Guid.Empty.ToString();
			Assert.IsTrue(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromStringFail
		[TestMethod]
		public void TestIsEmptyGuidFromStringFail()
		{
			String test = "asdasdad";
			Assert.IsFalse(test.IsGuid());
		}
		#endregion

		#region TestIsGuidFromObjectOk
		[TestMethod]
		public void TestIsGuidFromObjectOk()
		{
			Object test = Guid.NewGuid();
			Assert.IsTrue(test.IsGuid());
		}
		#endregion

		#region TestIsGuidFromNull
		[TestMethod]
		public void TestIsGuidFromNull()
		{
			String test = null;
			Assert.IsFalse(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromObjectOk
		[TestMethod]
		public void TestIsEmptyGuidFromObjectOk()
		{
			Object test = Guid.Empty;
			Assert.IsTrue(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromObjctFail
		[TestMethod]
		public void TestIsEmptyGuidFromObjctFail()
		{
			Object test = "asdasdad";
			Assert.IsFalse(test.IsGuid());
		}
		#endregion

		#region TestToShortGuid
		[TestMethod]
		public void TestToShortGuid()
		{
			String guidString = "503aaad8584a4461bb3273e3b724d53d";
			Guid myGuid = new Guid(guidString);
			Assert.AreEqual(guidString, myGuid.ToShortString());
		}
		#endregion

		#region TestIsInt32FromStringOk
		[TestMethod]
		public void TestIsInt32FromStringOk()
		{
			String test = "23";
			Assert.IsTrue(test.IsInt32());
		}
		#endregion

		#region TestIsInt32FromStringFail
		[TestMethod]
		public void TestIsInt32FromStringFail()
		{
			String test = "2as";
			Assert.IsFalse(test.IsInt32());
		}
		#endregion

		#region TestIsInt32FromStringWithFallback
		[TestMethod]
		public void TestIsInt32FromStringWithFallback()
		{
			String test = "2as";
			Int32 actual = test.ToInt32(10);
			Assert.AreEqual(10, actual);
		}
		#endregion

		#region TestIsDoubleFromStringOk
		[TestMethod]
		public void TestIsDoubleFromStringOk()
		{
			String test = "23.4";
			Assert.IsTrue(test.IsDouble());
		}
		#endregion

		#region TestIsDoubleFromStringFail
		[TestMethod]
		public void TestIsDoubleFromStringFail()
		{
			String test = "a";
			Assert.IsFalse(test.IsDouble());
		}
		#endregion

		#region TestToDecimalNoDeciamlDigits
		[TestMethod]
		public void TestToDecimalNoDeciamlDigits()
		{
			Decimal acutal = "4444".ToDecimal();
			Assert.AreEqual(4444m, acutal);
		}
		#endregion

		#region TestToDecimalWithCommaSeparator
		[TestMethod]
		public void TestToDecimalWithCommaSeparator()
		{
			Decimal actual = "44,5".ToDecimal();
			Assert.AreEqual(44.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartor
		[TestMethod]
		public void TestToDecimalWithDotSepeartor()
		{
			Decimal actual = "44.5".ToDecimal();
			Assert.AreEqual(44.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartor
		[TestMethod]
		public void TestToDecimalWithDotSepeartorAndCommaBlock()
		{
			Decimal actual = "444,444.5".ToDecimal();
			Assert.AreEqual(444444.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartor
		[TestMethod]
		public void TestToDecimalWithCommaSepeartorAndDotBlock()
		{
			Decimal actual = "444.444,5".ToDecimal();
			Assert.AreEqual(444444.5m, actual);
		}
		#endregion

		#region TestToDecimalWithCommaSepeartorAndDotBlockNegative
		[TestMethod]
		public void TestToDecimalWithCommaSepeartorAndDotBlockNegative()
		{
			Decimal actual = "-444.444,5".ToDecimal();
			Assert.AreEqual(-444444.5m, actual);
		}
		#endregion

		#region TestToDecimalWithCommaSepeartorAndMultiDotBlockNegative
		[TestMethod]
		public void TestToDecimalWithCommaSepeartorAndMultiDotBlockNegative()
		{
			Decimal actual = "-123.456.789,5".ToDecimal();
			Assert.AreEqual(-123456789.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartorAndMultiCommaBlock
		[TestMethod]
		public void TestToDecimalWithDotSepeartorAndMultiCommaBlock()
		{
			Decimal actual = "123,456,789.123".ToDecimal();
			Assert.AreEqual(123456789.123m, actual);
		}
		#endregion

		#region TestToDecimalFromCurrencyWithDotSepeartor
		[TestMethod]
		public void TestToDecimalFromCurrencyWithDotSepeartor()
		{
			Decimal actual = "123,456,789.123 €".ToDecimal();
			Assert.AreEqual(123456789.123m, actual);
		}
		#endregion

		#region TestToDecimalFromCurrencyWithCommaSepeartor
		[TestMethod]
		public void TestToDecimalFromCurrencyWithCommaSepeartor()
		{
			Decimal actual = "123.456.789,123 €".ToDecimal();
			Assert.AreEqual(123456789.123m, actual);
		}
		#endregion
	}
}
