using System;
using System.Linq;
using Xunit;

namespace ESolutions.Core.Test
{
	/// <summary>
	/// Summary description for DateTimeExtenderTests
	/// </summary>
	public class DateTimeExtenderTests
	{
		#region IsNearestToNext
		[Fact]
		public void IsNearestToNext()
		{
			DateTime birthday = new DateTime(1950, 7, 1);
			DateTime reference = new DateTime(2009, 6, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.Equal(new DateTime(2009, 7, 1), result);
		}
		#endregion

		#region IsNearestToLast
		[Fact]
		public void IsNearestToLast()
		{
			DateTime birthday = new DateTime(1950, 7, 1);
			DateTime reference = new DateTime(2009, 8, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.Equal(new DateTime(2009, 7, 1), result);
		}
		#endregion

		#region TestYearChangeUpper
		[Fact]
		public void TestYearChangeUpper()
		{
			DateTime birthday = new DateTime(1950, 4, 1);
			DateTime reference = new DateTime(2009, 11, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.Equal(new DateTime(2010, 4, 1), result);
		}
		#endregion

		#region TestYearChangeLower
		[Fact]
		public void TestYearChangeLower()
		{
			DateTime birthday = new DateTime(1950, 11, 1);
			DateTime reference = new DateTime(2009, 3, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.Equal(new DateTime(2008, 11, 1), result);
		}
		#endregion

		#region TestGetFirstDayOfWeek
		[Fact]
		public void TestGetFirstDayOfWeek()
		{
			DateTime now = new DateTime(2010, 12, 20);
			DateTime expected = new DateTime(2010, 12, 20);

			for (Int32 index = 0; index < 6; index++)
			{
				now = now.AddDays(1);
				DateTime actual = now.GetFirstDayOfWeek();
				Assert.Equal(expected, actual);
			}
		}
		#endregion

		#region TestGetLastDayOfWeek
		[Fact]
		public void TestGetLastDayOfWeek()
		{
			DateTime now = new DateTime(2010, 12, 20);
			DateTime expected = new DateTime(2010, 12, 26, 23, 59, 59);

			for (Int32 index = 0; index < 6; index++)
			{
				now = now.AddDays(1);
				DateTime actual = now.GetLastDayOfWeek();
				Assert.Equal(expected, actual);
			}
		}
		#endregion

		#region TestSqlLowerBound
		[Fact]
		public void TestSqlLowerBound()
		{
			DateTime pointOfTime = new DateTime(2011, 01, 01, 12, 12, 12);
			DateTime actual = pointOfTime.SqlLowerBound();
			DateTime expected = new DateTime(2011, 01, 01, 0, 0, 0, 0);
			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestSqlUpperBound
		[Fact]
		public void TestSqlUpperBound()
		{
			DateTime pointOfTime = new DateTime(2011, 01, 01, 12, 12, 12);
			DateTime actual = pointOfTime.SqlUpperBound();
			DateTime expected = new DateTime(2011, 01, 02).AddTicks(-1);
			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestOsterSonntagOf2011
		[Fact]
		public void TestOsterSonntagOf2011()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			DateTime actual = reference.GetOsterSonntag();
			DateTime expected = new DateTime(2011, 4, 24);
			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestIsHoliday
		[Fact]
		public void TestIsHoliday()
		{
			var date = new DateTime(2018, 5, 31);
			var isHoliday = date.IsGermanHoliday(GermanHolidays.Ostersonntag);

			Assert.False(isHoliday);
		}
		#endregion

		#region TestGetGermanHolidaysOfUnknwonFederalState
		[Fact]
		public void TestGetGermanHolidaysOfUnknwonFederalState()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Unknown);
			Assert.Equal(9, actual.Count());
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestGetGermanHolidaysOfAllFederalState
		[Fact]
		public void TestGetGermanHolidaysOfAllFederalState()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.All);
			Assert.Equal(18, actual.Count());
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			/*Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag*/
			Assert.True(actual.ContainsKey(new DateTime(2011, 3, 8))); //InternationalerFrauentag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			//Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 8, 8))) //Augsburger Hohes Friedensfest
			Assert.True(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26))); //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInBadenWuertemberg
		[Fact]
		public void TestHolidaysInBadenWuertemberg()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.BadenWuerttemberg);
			Assert.Equal(12, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInBayern
		[Fact]
		public void TestHolidaysInBayern()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Bayern);
			Assert.Equal(12, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInBerlin
		[Fact]
		public void TestHolidaysInBerlin()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Berlin);
			Assert.Equal(10, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 3, 8))); //InternationalerFrauentag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInBrandenburg
		[Fact]
		public void TestHolidaysInBrandenburg()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Brandenburg);
			Assert.Equal(12, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInBremen
		[Fact]
		public void TestHolidaysInBremen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Bremen);
			Assert.Equal(9, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInHamburg
		[Fact]
		public void TestHolidaysInHamburg()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Hamburg);
			Assert.Equal(10, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInHessen
		[Fact]
		public void TestHolidaysInHessen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Hessen);
			Assert.Equal(10, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInMecklenburgVorpommern
		[Fact]
		public void TestHolidaysInMecklenburgVorpommern()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.MecklenburgVorpommern);
			Assert.Equal(10, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInNiedersachsen
		[Fact]
		public void TestHolidaysInNiedersachsen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Niedersachsen);
			Assert.Equal(9, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInNordrheinWestfalen
		[Fact]
		public void TestHolidaysInNordrheinWestfalen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.NordrheinWestfalen);
			Assert.Equal(11, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInRheinlandPfalz
		[Fact]
		public void TestHolidaysInRheinlandPfalz()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.RheinlandPfalz);
			Assert.Equal(11, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInSaarland
		[Fact]
		public void TestHolidaysInSaarland()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Saarland);
			Assert.Equal(12, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.True(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.False(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInSachsen
		[Fact]
		public void TestHolidaysInSachsen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Sachsen);
			Assert.Equal(11, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.True(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInSachsenAnhalt
		[Fact]
		public void TestHolidaysInSachsenAnhalt()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.SachsenAnhalt);
			Assert.Equal(11, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInSchleswigHolstein
		[Fact]
		public void TestHolidaysInSchleswigHolstein()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.SchleswigHolstein);
			Assert.Equal(10, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestHolidaysInThueringen
		[Fact]
		public void TestHolidaysInThueringen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Thueringen);
			Assert.Equal(10, actual.Count);
			Assert.True(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
			Assert.False(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
			Assert.False(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
			Assert.True(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
			Assert.True(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
			Assert.False(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
			Assert.False(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
			Assert.True(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
			Assert.False(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
			Assert.True(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
		}
		#endregion

		#region TestIsGermanHoliday
		[Fact]
		public void TestIsGermanHoliday()
		{
			Assert.True(new DateTime(2011, 4, 24).IsGermanHoliday(GermanFederalStates.Brandenburg));
			Assert.True(new DateTime(2012, 4, 6).IsGermanHoliday(GermanFederalStates.Unknown));

			Assert.True(new DateTime(2018, 1, 1).IsGermanHoliday(GermanHolidays.Neujahr));
			Assert.False(new DateTime(2018, 1, 2).IsGermanHoliday(GermanHolidays.Neujahr));
		}
		#endregion

		#region TestGetHolidaysIn2008
		/// <summary>
		/// Tests the get holidays in2008. In 2008 "Christi Himmelfahrt" und "Tag der Arbeit" fell on the same day. 
		/// SortedListe does not like equal keys ;-)
		/// </summary>
		[Fact]
		public void TestGetHolidaysIn2008()
		{
			DateTime actual = new DateTime(2008, 1, 1);
			var holidays = actual.GetGermanHolidaysGrouped(GermanFederalStates.Unknown);
			Assert.True(holidays.ContainsKey(new DateTime(2008, 5, 1)));
			var firstOfMay = holidays[new DateTime(2008, 5, 1)];
			Assert.Equal(2, firstOfMay.Count());
			Assert.Equal("Tag der Arbeit", firstOfMay.First().Name);
			Assert.Equal(GermanHolidays.TagDerArbeit, firstOfMay.First().GermanHoliday);
			Assert.Equal("Christi Himmelfahrt", firstOfMay.Last().Name);
			Assert.Equal(GermanHolidays.ChristiHimmelfahrt, firstOfMay.Last().GermanHoliday);
		}
		#endregion

		#region TestFirstDayOfMonth
		[Fact]
		public void TestFirstDayOfMonth()
		{
			DateTime testDate = new DateTime(2011, 08, 20);
			DateTime actual = testDate.GetFirstDayOfMonth();
			DateTime expected = new DateTime(2011, 08, 1);

			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestLastDayOfMonth
		[Fact]
		public void TestLastDayOfMonth()
		{
			DateTime testDate = new DateTime(2011, 08, 20);
			DateTime actual = testDate.GetLastDayOfMonth();
			DateTime expected = new DateTime(2011, 08, 31);

			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestFirstDayOfYear
		[Fact]
		public void TestFirstDayOfYear()
		{
			DateTime testDate = new DateTime(2016, 2, 9);
			DateTime actual = testDate.GetFirstDayOfYear();
			DateTime expected = new DateTime(2016, 1, 1);

			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestFirstDayOfYear
		[Fact]
		public void TestLastDayOfYear()
		{
			DateTime testDate = new DateTime(2016, 2, 9);
			DateTime actual = testDate.GetLastDayOfYear();
			DateTime expected = new DateTime(2016, 12, 31);

			Assert.Equal(expected, actual);
		}
		#endregion

		#region TestTomorrow
		[Fact]
		public void TestTomorrow()
		{
			var time = new DateTime(2016, 1, 1);
			var actual = time.Tomorrow();

			Assert.Equal(2016, actual.Year);
			Assert.Equal(1, actual.Month);
			Assert.Equal(2, actual.Day);
		}
		#endregion

		#region TestYesterday
		[Fact]
		public void TestYesterday()
		{
			var time = new DateTime(2016, 1, 1);
			var actual = time.Yesterday();

			Assert.Equal(2015, actual.Year);
			Assert.Equal(12, actual.Month);
			Assert.Equal(31, actual.Day);
		}
		#endregion

		#region TestGetDaysBetween
		[Fact]
		public void TestGetDaysBetween()
		{
			var from = new DateTime(2019, 1, 1);
			var until = new DateTime(2019, 1, 5);

			var actual = from.GetDaysBetween(until).ToList();

			Assert.Equal(actual[0], new DateTime(2019, 1, 1));
			Assert.Equal(actual[1], new DateTime(2019, 1, 2));
			Assert.Equal(actual[2], new DateTime(2019, 1, 3));
			Assert.Equal(actual[3], new DateTime(2019, 1, 4));
			Assert.Equal(actual[4], new DateTime(2019, 1, 5));
		}
		#endregion
	}
}