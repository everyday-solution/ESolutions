using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Test
{
	/// <summary>
	/// Summary description for DateTimeExtenderTests
	/// </summary>
	[TestClass]
	public class DateTimeExtenderTests
	{
		#region DateTimeExtenderTests
		public DateTimeExtenderTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region testContextInstance
		private TestContext testContextInstance;
		#endregion

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

		#region IsNearestToNext
		[TestMethod]
		public void IsNearestToNext()
		{
			DateTime birthday = new DateTime(1950, 7, 1);
			DateTime reference = new DateTime(2009, 6, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.AreEqual(new DateTime(2009, 7, 1), result);
		}
		#endregion

		#region IsNearestToLast
		[TestMethod]
		public void IsNearestToLast()
		{
			DateTime birthday = new DateTime(1950, 7, 1);
			DateTime reference = new DateTime(2009, 8, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.AreEqual(new DateTime(2009, 7, 1), result);
		}
		#endregion

		#region TestYearChangeUpper
		[TestMethod]
		public void TestYearChangeUpper()
		{
			DateTime birthday = new DateTime(1950, 4, 1);
			DateTime reference = new DateTime(2009, 11, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.AreEqual(new DateTime(2010, 4, 1), result);
		}
		#endregion

		#region TestYearChangeLower
		[TestMethod]
		public void TestYearChangeLower()
		{
			DateTime birthday = new DateTime(1950, 11, 1);
			DateTime reference = new DateTime(2009, 3, 1);

			DateTime result = birthday.GetNearestOccurence(reference);

			Assert.AreEqual(new DateTime(2008, 11, 1), result);
		}
		#endregion

		#region TestGetFirstDayOfWeek
		[TestMethod]
		public void TestGetFirstDayOfWeek()
		{
			DateTime now = new DateTime(2010, 12, 20);
			DateTime expected = new DateTime(2010, 12, 20);

			for (Int32 index = 0; index < 6; index++)
			{
				now = now.AddDays(1);
				DateTime actual = now.GetFirstDayOfWeek();
				Assert.AreEqual(expected, actual);
			}
		}
		#endregion

		#region TestGetLastDayOfWeek
		[TestMethod]
		public void TestGetLastDayOfWeek()
		{
			DateTime now = new DateTime(2010, 12, 20);
			DateTime expected = new DateTime(2010, 12, 26, 23, 59, 59);

			for (Int32 index = 0; index < 6; index++)
			{
				now = now.AddDays(1);
				DateTime actual = now.GetLastDayOfWeek();
				Assert.AreEqual(expected, actual);
			}
		}
		#endregion

		#region TestSqlLowerBound
		[TestMethod]
		public void TestSqlLowerBound()
		{
			DateTime pointOfTime = new DateTime(2011, 01, 01, 12, 12, 12);
			DateTime actual = pointOfTime.SqlLowerBound();
			DateTime expected = new DateTime(2011, 01, 01, 0, 0, 0, 0);
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestSqlUpperBound
		[TestMethod]
		public void TestSqlUpperBound()
		{
			DateTime pointOfTime = new DateTime(2011, 01, 01, 12, 12, 12);
			DateTime actual = pointOfTime.SqlUpperBound();
			DateTime expected = new DateTime(2011, 01, 02).AddTicks(-1);
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestOsterSonntagOf2011
		[TestMethod]
		public void TestOsterSonntagOf2011()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			DateTime actual = reference.GetOsterSonntag();
			DateTime expected = new DateTime(2011, 4, 24);
			Assert.AreEqual(expected, actual);
		}
        #endregion

        #region TestIsHoliday
        [TestMethod]
        public void TestIsHoliday()
        {
            var date = new DateTime(2018, 5, 31);
            var isHoliday = date.IsGermanHoliday(GermanHolidays.Ostersonntag);

            Assert.IsFalse(isHoliday);
        }
        #endregion


        #region TestGetGermanHolidaysOfUnknwonFederalState
        [TestMethod]
		public void TestGetGermanHolidaysOfUnknwonFederalState()
		{
			DateTime reference = new DateTime(2011, 1, 1);
			var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Unknown);
            Assert.AreEqual(9, actual.Count());
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
        #endregion

        #region TestGetGermanHolidaysOfAllFederalState
        [TestMethod]
        public void TestGetGermanHolidaysOfAllFederalState()
        {
            DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.All);
            Assert.AreEqual(17, actual.Count());
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            //Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            //Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 8, 8))) //Augsburger Hohes Friedensfest
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26))); //2. Weihnachtsfeiertag
        }
        #endregion

        #region TestHolidaysInBadenWuertemberg
        [TestMethod]
		public void TestHolidaysInBadenWuertemberg()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.BadenWuerttemberg);
			Assert.AreEqual(12, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInBayern
		[TestMethod]
		public void TestHolidaysInBayern()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Bayern);
			Assert.AreEqual(12, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInBerlin
		[TestMethod]
		public void TestHolidaysInBerlin()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Berlin);
			Assert.AreEqual(9, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInBrandenburg
		[TestMethod]
		public void TestHolidaysInBrandenburg()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Brandenburg);
			Assert.AreEqual(12, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInBremen
		[TestMethod]
		public void TestHolidaysInBremen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Bremen);
			Assert.AreEqual(9, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInHamburg
		[TestMethod]
		public void TestHolidaysInHamburg()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Hamburg);
			Assert.AreEqual(10, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInHessen
		[TestMethod]
		public void TestHolidaysInHessen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Hessen);
			Assert.AreEqual(10, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInMecklenburgVorpommern
		[TestMethod]
		public void TestHolidaysInMecklenburgVorpommern()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.MecklenburgVorpommern);
			Assert.AreEqual(10, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInNiedersachsen
		[TestMethod]
		public void TestHolidaysInNiedersachsen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Niedersachsen);
			Assert.AreEqual(9, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInNordrheinWestfalen
		[TestMethod]
		public void TestHolidaysInNordrheinWestfalen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.NordrheinWestfalen);
			Assert.AreEqual(11, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInRheinlandPfalz
		[TestMethod]
		public void TestHolidaysInRheinlandPfalz()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.RheinlandPfalz);
			Assert.AreEqual(11, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInSaarland
		[TestMethod]
		public void TestHolidaysInSaarland()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Saarland);
			Assert.AreEqual(12, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInSachsen
		[TestMethod]
		public void TestHolidaysInSachsen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Sachsen);
			Assert.AreEqual(11, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInSachsenAnhalt
		[TestMethod]
		public void TestHolidaysInSachsenAnhalt()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.SachsenAnhalt);
			Assert.AreEqual(11, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInSchleswigHolstein
		[TestMethod]
		public void TestHolidaysInSchleswigHolstein()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.SchleswigHolstein);
			Assert.AreEqual(10, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
		#endregion

		#region TestHolidaysInThueringen
		[TestMethod]
		public void TestHolidaysInThueringen()
		{
			DateTime reference = new DateTime(2011, 1, 1);
            var actual = reference.GetGermanHolidaysGrouped(GermanFederalStates.Thueringen);
			Assert.AreEqual(10, actual.Count);
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 1, 1))); //Neujahr
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 1, 6))); //Heilige Drei Könige
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 21))); //Gründonnerstag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 22))); //Karfreitag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 4, 24))); //Ostersonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 4, 25))); //Ostermontag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 5, 1))); //Tag der Arbeit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 2))); //Christi Himmelfahrt
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 12))); //Pfingstsonntag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 6, 13))); //Pfingstmontag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 6, 23))); //Fronleichnam
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 8))); //Augsburger Hohes Friedensfest
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 8, 15))); //Mariä Himmlefahrt
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 3))); //Tag der dt. Einheit
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 10, 31))); //Reformationstag
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 1))); //Allerheiligen
            Assert.IsFalse(actual.ContainsKey(new DateTime(2011, 11, 16))); //Buß und Bettag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 25))); //1. Weihnachtsfeiertag
            Assert.IsTrue(actual.ContainsKey(new DateTime(2011, 12, 26)));  //2. Weihnachtsfeiertag
        }
        #endregion

        #region TestIsGermanHoliday
        [TestMethod]
		public void TestIsGermanHoliday()
		{
			Assert.IsTrue(new DateTime(2011, 4, 24).IsGermanHoliday(GermanFederalStates.Brandenburg));
            Assert.IsTrue(new DateTime(2012, 4, 6).IsGermanHoliday(GermanFederalStates.Unknown));

            Assert.IsTrue(new DateTime(2018, 1, 1).IsGermanHoliday(GermanHolidays.Neujahr));
            Assert.IsFalse(new DateTime(2018, 1, 2).IsGermanHoliday(GermanHolidays.Neujahr));
        }
		#endregion

		#region TestGetHolidaysIn2008
		/// <summary>
		/// Tests the get holidays in2008. In 2008 "Christi Himmelfahrt" und "Tag der Arbeit" fell on the same day. 
		/// SortedListe does not like equal keys ;-)
		/// </summary>
		[TestMethod]
		public void TestGetHolidaysIn2008()
		{
			try
			{
				DateTime actual = new DateTime(2008, 1, 1);
				var holidays = actual.GetGermanHolidaysGrouped(GermanFederalStates.Unknown);
				Assert.IsTrue(holidays.ContainsKey(new DateTime(2008, 5, 1)));
				var firstOfMay = holidays[new DateTime(2008, 5, 1)];
                Assert.AreEqual(2, firstOfMay.Count());
				Assert.AreEqual("Tag der Arbeit", firstOfMay.First().Name);
                Assert.AreEqual(GermanHolidays.TagDerArbeit, firstOfMay.First().GermanHoliday);
                Assert.AreEqual("Christi Himmelfahrt", firstOfMay.Last().Name);
                Assert.AreEqual(GermanHolidays.ChristiHimmelfahrt, firstOfMay.Last().GermanHoliday);

            }
			catch
			{
				Assert.Fail();
			}
		}
		#endregion

		#region TestFirstDayOfMonth
		[TestMethod]
		public void TestFirstDayOfMonth()
		{
			DateTime testDate = new DateTime(2011, 08, 20);
			DateTime actual = testDate.GetFirstDayOfMonth();
			DateTime expected = new DateTime(2011, 08, 1);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestLastDayOfMonth
		[TestMethod]
		public void TestLastDayOfMonth()
		{
			DateTime testDate = new DateTime(2011, 08, 20);
			DateTime actual = testDate.GetLastDayOfMonth();
			DateTime expected = new DateTime(2011, 08, 31);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestFirstDayOfYear
		[TestMethod]
		public void TestFirstDayOfYear()
		{
			DateTime testDate = new DateTime(2016, 2, 9);
			DateTime actual = testDate.GetFirstDayOfYear();
			DateTime expected = new DateTime(2016, 1, 1);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestFirstDayOfYear
		[TestMethod]
		public void TestLastDayOfYear()
		{
			DateTime testDate = new DateTime(2016, 2, 9);
			DateTime actual = testDate.GetLastDayOfYear();
			DateTime expected = new DateTime(2016, 12, 31);

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region TestTomorrow
		[TestMethod]
		public void TestTomorrow()
		{
			var time = new DateTime(2016, 1, 1);
			var actual = time.Tomorrow();

			Assert.AreEqual(2016, actual.Year);
			Assert.AreEqual(1, actual.Month);
			Assert.AreEqual(2, actual.Day);
		}
		#endregion

		#region TestYesterday
		[TestMethod]
		public void TestYesterday()
		{
			var time = new DateTime(2016, 1, 1);
			var actual = time.Yesterday();

			Assert.AreEqual(2015, actual.Year);
			Assert.AreEqual(12, actual.Month);
			Assert.AreEqual(31, actual.Day);
		}
		#endregion
	}
}