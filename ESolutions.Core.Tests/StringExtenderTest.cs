using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Xunit;

namespace ESolutions.Core.Test
{
	/// <summary>
	/// Summary description for StringExtenderTest
	/// </summary>
	public class StringExtenderTest
	{
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
		[Fact]
		public void ConvertValidInt()
		{
			Int32 check = 33;
			Int32 result = "33".ToInt32();

			Assert.Equal(check, result);
		}
		#endregion

		#region ConvertValidNegativeInt
		[Fact]
		public void ConvertValidNegativeInt()
		{
			Int32 check = -33;
			Int32 result = "-33".ToInt32();

			Assert.Equal(check, result);
		}
		#endregion

		#region ConvertInvalidInt
		[Fact]
		public void ConvertInvalidInt()
		{
			Assert.Throws<ConverterException>(() => "test".ToInt32());
		}
		#endregion

		#region ConvertValidDouble
		[Fact]
		public void ConvertValidDouble()
		{
			Double result = "4,55".ToDouble(new CultureInfo("de-DE"));
			Assert.Equal(4.55, result);
		}
		#endregion

		#region ConvertValidDoubleEnUs
		[Fact]
		public void ConvertValidDoubleEnUS()
		{
			Double result = "4.55".ToDouble(new CultureInfo("en-US"));
			Assert.Equal(4.55, result);
		}
		#endregion

		#region ConvertInvalidDouble
		[Fact]
		public void ConvertInvalidDouble()
		{
			Double result = "4,55aa".ToDouble(new CultureInfo("de-DE"));
			Assert.Equal(4.55, result);
		}
		#endregion

		#region ConvertValidNegativeDouble
		[Fact]
		public void ConvertValidNegativeDouble()
		{
			Double result = "-4,55".ToDouble(new CultureInfo("de-DE"));
			Assert.Equal(-4.55, result);
		}
		#endregion

		#region TestThatUnderscoreConvertsCamelcaseTextToUnderscoredText
		[Fact]
		public void TestThatUnderscoreConvertsCamelcaseTextToUnderscoredText()
		{
			String testText = "activeRecord";

			Assert.Equal("active_record", testText.Underscore());
		}
		#endregion

		#region TestThatUnderscoreConvertsPascalcaseTextToUnderscoredText
		[Fact]
		public void TestThatUnderscoreConvertsPascalcaseTextToUnderscoredText()
		{
			String testText = "ActiveRecord";

			Assert.Equal("active_record", testText.Underscore());
		}
		#endregion

		#region TestThatCamelizeConvertsUnderscoredTextToCamelizedText
		[Fact]
		public void TestThatCamelizeConvertsUnderscoredTextToCamelizedText()
		{
			String testText = "active_record";

			Assert.Equal("activeRecord", testText.Camelize());
		}
		#endregion

		#region TestThatPascalizeConvertsUnderscoredTextToPascalizedText
		[Fact]
		public void TestThatPascalizeConvertsUnderscoredTextToPascalizedText()
		{
			String testText = "active_record";

			Assert.Equal("ActiveRecord", testText.Pascalize());
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithoutMarker
		[Fact]
		public void TestThatSplitMethodSplitsStringsWithoutMarker()
		{
			String input = "Hage de Maulwurfn";
			List<String> parts = input.Split(";").ToList();

			Assert.Single(parts);
			Assert.Equal(input, parts[0]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithMarkers
		[Fact]
		public void TestThatSplitMethodSplitsStringsWithMarkers()
		{
			String input = "Hage de;Maulwurfn";
			List<String> parts = input.Split(";").ToList();

			Assert.Equal(2, parts.Count);
			Assert.Equal("Hage de", parts[0]);
			Assert.Equal("Maulwurfn", parts[1]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithMarkersInFront
		[Fact]
		public void TestThatSplitMethodSplitsStringsWithMarkersInFront()
		{
			String input = ";;Hage de Maulwurfn";
			List<String> parts = input.Split(";").ToList();

			Assert.Equal(3, parts.Count);
			Assert.Equal("Hage de Maulwurfn", parts[2]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsWithMarkersInBack
		[Fact]
		public void TestThatSplitMethodSplitsStringsWithMarkersInBack()
		{
			String input = "Hage de Maulwurfn;;";
			List<String> parts = input.Split(";").ToList();

			Assert.Equal(3, parts.Count);
			Assert.Equal("Hage de Maulwurfn", parts[0]);
		}
		#endregion

		#region TestThatSplitMethodSplitsStringsLeavingBlankLinesOut
		[Fact]
		public void TestThatSplitMethodSplitsStringsLeavingBlankLinesOut()
		{
			String input = "Hage de Maulwurfn;;;;";
			List<String> parts = input.Split(";", false);

			Assert.Single(parts);
			Assert.Equal("Hage de Maulwurfn", parts[0]);
		}
		#endregion

		#region TestThatSplitMethodsSplitsStringWithSouroundings
		[Fact]
		public void TestThatSplitMethodsSplitsStringWithSouroundings()
		{
			String input = @"""HAGE"";""Maulwurfn""";
			List<String> parts = input.Split(";", "\"");

			Assert.Equal(2, parts.Count);
			Assert.Equal(@"""HAGE""", parts[0]);
			Assert.Equal(@"""Maulwurfn""", parts[1]);
		}
		#endregion

		#region TestThatSplitMethodsSplitsStringWithSouroundingsAndSplitMarkersNotSourrounded
		[Fact]
		public void TestThatSplitMethodsSplitsStringWithSouroundingsAndSplitMarkersNotSourrounded()
		{
			String input = @"""HA;GE"";""Maulw;urfn""";
			List<String> parts = input.Split(";", "\"");

			Assert.Equal(2, parts.Count);
			Assert.Equal(@"""HA;GE""", parts[0]);
			Assert.Equal(@"""Maulw;urfn""", parts[1]);
		}
		#endregion

		#region TestIsBooleanOk
		[Fact]
		public void TestIsBooleanOk()
		{
			String test = "TRUE";
			Assert.True(test.IsBoolean());
		}
		#endregion

		#region TestIsBooleanFail
		[Fact]
		public void TestIsBooleanFail()
		{
			String test = "asd";
			Assert.False(test.IsBoolean());
		}
		#endregion

		#region TestIsGuidFromStringOk
		[Fact]
		public void TestIsGuidFromStringOk()
		{
			String test = Guid.NewGuid().ToString();
			Assert.True(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromStringOk
		[Fact]
		public void TestIsEmptyGuidFromStringOk()
		{
			String test = Guid.Empty.ToString();
			Assert.True(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromStringFail
		[Fact]
		public void TestIsEmptyGuidFromStringFail()
		{
			String test = "asdasdad";
			Assert.False(test.IsGuid());
		}
		#endregion

		#region TestIsGuidFromObjectOk
		[Fact]
		public void TestIsGuidFromObjectOk()
		{
			Object test = Guid.NewGuid();
			Assert.True(test.IsGuid());
		}
		#endregion

		#region TestIsGuidFromNull
		[Fact]
		public void TestIsGuidFromNull()
		{
			String test = null;
			Assert.False(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromObjectOk
		[Fact]
		public void TestIsEmptyGuidFromObjectOk()
		{
			Object test = Guid.Empty;
			Assert.True(test.IsGuid());
		}
		#endregion

		#region TestIsEmptyGuidFromObjectFail
		[Fact]
		public void TestIsEmptyGuidFromObjectFail()
		{
			Object test = "asdasdad";
			Assert.False(test.IsGuid());
		}
		#endregion

		#region TestToShortGuid
		[Fact]
		public void TestToShortGuid()
		{
			String guidString = "503aaad8584a4461bb3273e3b724d53d";
			Guid myGuid = new Guid(guidString);
			Assert.Equal(guidString, myGuid.ToShortString());
		}
		#endregion

		#region TestIsInt32FromStringOk
		[Fact]
		public void TestIsInt32FromStringOk()
		{
			String test = "23";
			Assert.True(test.IsInt32());
		}
		#endregion

		#region TestIsInt32FromStringFail
		[Fact]
		public void TestIsInt32FromStringFail()
		{
			String test = "2as";
			Assert.False(test.IsInt32());
		}
		#endregion

		#region TestIsInt32FromStringWithFallback
		[Fact]
		public void TestIsInt32FromStringWithFallback()
		{
			String test = "2as";
			Int32 actual = test.ToInt32(10);
			Assert.Equal(10, actual);
		}
		#endregion

		#region TestIsDoubleFromStringOk
		[Fact]
		public void TestIsDoubleFromStringOk()
		{
			String test = "23.4";
			Assert.True(test.IsDouble());
		}
		#endregion

		#region TestIsDoubleFromStringFail
		[Fact]
		public void TestIsDoubleFromStringFail()
		{
			String test = "a";
			Assert.False(test.IsDouble());
		}
		#endregion

		#region TestToDecimalNoDeciamlDigits
		[Fact]
		public void TestToDecimalNoDeciamlDigits()
		{
			Decimal acutal = "4444".ToDecimal();
			Assert.Equal(4444m, acutal);
		}
		#endregion

		#region TestToDecimalWithCommaSeparator
		[Fact]
		public void TestToDecimalWithCommaSeparator()
		{
			Decimal actual = "44,5".ToDecimal();
			Assert.Equal(44.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartor
		[Fact]
		public void TestToDecimalWithDotSepeartor()
		{
			Decimal actual = "44.5".ToDecimal();
			Assert.Equal(44.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartor
		[Fact]
		public void TestToDecimalWithDotSepeartorAndCommaBlock()
		{
			Decimal actual = "444,444.5".ToDecimal();
			Assert.Equal(444444.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartor
		[Fact]
		public void TestToDecimalWithCommaSepeartorAndDotBlock()
		{
			Decimal actual = "444.444,5".ToDecimal();
			Assert.Equal(444444.5m, actual);
		}
		#endregion

		#region TestToDecimalWithCommaSepeartorAndDotBlockNegative
		[Fact]
		public void TestToDecimalWithCommaSepeartorAndDotBlockNegative()
		{
			Decimal actual = "-444.444,5".ToDecimal();
			Assert.Equal(-444444.5m, actual);
		}
		#endregion

		#region TestToDecimalWithCommaSepeartorAndMultiDotBlockNegative
		[Fact]
		public void TestToDecimalWithCommaSepeartorAndMultiDotBlockNegative()
		{
			Decimal actual = "-123.456.789,5".ToDecimal();
			Assert.Equal(-123456789.5m, actual);
		}
		#endregion

		#region TestToDecimalWithDotSepeartorAndMultiCommaBlock
		[Fact]
		public void TestToDecimalWithDotSepeartorAndMultiCommaBlock()
		{
			Decimal actual = "123,456,789.123".ToDecimal();
			Assert.Equal(123456789.123m, actual);
		}
		#endregion

		#region TestToDecimalFromCurrencyWithDotSepeartor
		[Fact]
		public void TestToDecimalFromCurrencyWithDotSepeartor()
		{
			Decimal actual = "123,456,789.123 €".ToDecimal();
			Assert.Equal(123456789.123m, actual);
		}
		#endregion

		#region TestToDecimalFromCurrencyWithCommaSepeartor
		[Fact]
		public void TestToDecimalFromCurrencyWithCommaSepeartor()
		{
			Decimal actual = "123.456.789,123 €".ToDecimal();
			Assert.Equal(123456789.123m, actual);
		}
		#endregion
	}
}
