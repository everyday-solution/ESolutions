using System;
using System.Collections.Generic;
using System.Linq;

namespace ESolutions
{
	/// <summary>
	/// Extender class for the DateTime class.
	/// </summary>
	public static class DateTimeExtender
	{
		//Methods
		#region GetNearestOccurence
		/// <summary>
		/// Gets the last or next occurence if the reference date that is the nearest to the current date.
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Set the birthday is 01.07.1950 and the reference date is 01.08.2009 the result is 01.07.2009.
		/// Set the birthday is 01.07.1950 and the reference date is 01.02.2009 the result is 01.07.2009
		/// </remarks>
		public static DateTime GetNearestOccurence(this DateTime me, DateTime referenceDate)
		{
			Dictionary<DateTime, Int64> pairs = new Dictionary<DateTime, Int64>();

			for (Int32 year = referenceDate.Year - 1; year <= referenceDate.Year + 1; year++)
			{
				DateTime date = new DateTime(year, me.Month, me.Day);
				Int64 distance = Math.Abs(date.Ticks - referenceDate.Ticks);
				pairs.Add(date, distance);
			}

			return pairs.OrderBy(runner => runner.Value).First().Key;
		}
		#endregion

		#region GetFirstDayOfWeek
		/// <summary>
		/// Gets the first day of the week the specified input value is in.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		/// The first day of the week relative to the input date.
		/// </returns>
		/// <remarks>
		/// The DayOfWeek starts with Sunday. This causes a special case to the calculation.
		/// </remarks>
		public static DateTime GetFirstDayOfWeek(this DateTime input)
		{
			Int32 daysBetween = 6;
			if (input.DayOfWeek != DayOfWeek.Sunday)
			{
				daysBetween = input.DayOfWeek - DayOfWeek.Monday;
			}

			return input.Date.AddDays(-1 * daysBetween);
		}
		#endregion

		#region GetLastDayOfWeek
		/// <summary>
		/// Gets the last day of the week the specified input value is in.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		/// The last day of the week relative to the input date.
		/// </returns>
		/// <remarks>
		/// The DayOfWeek starts with Sunday. This causes a special case to the calculation.
		/// </remarks>
		public static DateTime GetLastDayOfWeek(this DateTime input)
		{
			DateTime result = input.Date;

			Int32 daysBetween = 0;
			if (input.DayOfWeek != DayOfWeek.Sunday)
			{
				daysBetween = 7 - (Int32)input.DayOfWeek;
			}

			DateTime sunday = input.AddDays(daysBetween).Date;
			return new DateTime(sunday.Year, sunday.Month, sunday.Day, 23, 59, 59);
		}
		#endregion

		#region GetFirstDayOfMonth
		/// <summary>
		/// Gets the first day of month.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static DateTime GetFirstDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}
		#endregion

		#region GetLastDayOfMonth
		/// <summary>
		/// Gets the last day of month.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static DateTime GetLastDayOfMonth(this DateTime date)
		{
			return new DateTime(
				date.Year,
				date.Month,
				DateTime.DaysInMonth(date.Year, date.Month));
		}
		#endregion

		#region GetLastDayOfYear
		/// <summary>
		/// Gets the last day of the specified year.
		/// </summary>
		/// <param name="referenceDate">The reference date.</param>
		/// <returns></returns>
		public static DateTime GetLastDayOfYear(this DateTime referenceDate)
		{
			return new DateTime(referenceDate.Year, 12, 31);
		}
		#endregion

		#region GetLastDayOfYear
		/// <summary>
		/// Gets the last day of the specified year.
		/// </summary>
		/// <param name="referenceDate">The reference date.</param>
		/// <returns></returns>
		public static DateTime GetFirstDayOfYear(this DateTime referenceDate)
		{
			return new DateTime(referenceDate.Year, 1, 1);
		}
		#endregion

		#region SqlLowerBound
		/// <summary>
		/// Creates and SQL compatible datetime lower bound
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static DateTime SqlLowerBound(this DateTime value)
		{
			return value.Date;
		}
		#endregion

		#region SqlUpperBound
		/// <summary>
		/// Creates and SQL compatible datetime upper bound
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static DateTime SqlUpperBound(this DateTime value)
		{
			return value.Date.AddDays(1).AddTicks(-1);
		}
		#endregion

		#region IsGermanHoliday
		/// <summary>
		/// Determines whether the specfied date is a holiday in germany.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///   <c>true</c> if the date is a holiday in germany; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsGermanHoliday(this DateTime value)
		{
			return value.GetGermanHolidaysGrouped(GermanFederalStates.Unknown).ContainsKey(value.Date);
		}
		#endregion

		#region IsGermanHoliday
		/// <summary>
		/// Determines whether the specfied date is a holiday in germany.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="state">The state.</param>
		/// <returns>
		///   <c>true</c> if the date is a holiday in germany; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsGermanHoliday(this DateTime value, GermanFederalStates state)
		{
			return value.GetGermanHolidaysGrouped(state).ContainsKey(value.Date);
		}
		#endregion

		#region IsGermanHoliday
		/// <summary>
		/// Determines whether the specfied date is a holiday in germany.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="state">The state.</param>
		/// <returns>
		///   <c>true</c> if the date is a holiday in germany; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsGermanHoliday(this DateTime value, GermanHolidays holiday)
		{
			return value
				.GetGermanHolidays(GermanFederalStates.All)
				.Where(runner => runner.GermanHoliday == holiday)
				.FirstOrDefault(runner => runner.Day.Date == value.Date) != null;
		}
		#endregion

		#region GetGermanHolidaysGrouped
		/// <summary>
		/// Gets the german holidays grouped by day.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="state">The state.</param>
		/// <returns></returns>
		public static Dictionary<DateTime, IEnumerable<Holiday>> GetGermanHolidaysGrouped(this DateTime value, GermanFederalStates state)
		{
			var holidays = value.GetGermanHolidays(state)
				.GroupBy(runner => runner.Day)
				.ToDictionary(runner => runner.Key, runner => runner.ToList() as IEnumerable<Holiday>);

			return holidays;
		}
		#endregion

		#region GetGermanHolidays
		public static List<Holiday> GetGermanHolidays(this DateTime value, GermanFederalStates state)
		{
			List<Holiday> holidays = new List<Holiday>();
			Int32 year = value.Year;
			DateTime osterSonntag = value.GetOsterSonntag();

			//1. Neujahr
			holidays.Add(new Holiday(new DateTime(year, 1, 1), GermanHolidays.Neujahr, "Neujahr"));

			//2. Heilige Drei Könige
			if (
				state == GermanFederalStates.All ||
				state == GermanFederalStates.BadenWuerttemberg ||
				state == GermanFederalStates.Bayern ||
				state == GermanFederalStates.SachsenAnhalt)
			{
				holidays.Add(new Holiday(new DateTime(year, 1, 6), GermanHolidays.HeiligeDreiKoenige, "Heilige Drei Könige"));
			}

			//3. Gründonnerstag (nur für Schüler)
			if (state == GermanFederalStates.All ||
				state == GermanFederalStates.BadenWuerttemberg)
			{
				//holidays.Add(new Holiday(osterSonntag.AddDays(-3), GermanHolidays.Gruendonnerstag, "Gründonnerstag"));
			}

			//4. Frauenkampftag
			if (state == GermanFederalStates.All ||
				state == GermanFederalStates.Berlin)
			{
				holidays.Add(new Holiday(new DateTime(year, 3, 8), GermanHolidays.InternationalerFrauentag, "Internationaler Frauentag"));
			}

			//5. Karfreitag
			holidays.Add(new Holiday(osterSonntag.AddDays(-2), GermanHolidays.Karfreitag, "Karfreitag"));

			//6. Ostersonntag
			if (state == GermanFederalStates.All ||
				state == GermanFederalStates.Brandenburg)
			{
				holidays.Add(new Holiday(osterSonntag, GermanHolidays.Ostersonntag, "Ostersonntag"));
			}

			//7. Ostermontag
			holidays.Add(new Holiday(osterSonntag.AddDays(1), GermanHolidays.Ostermontag, "Ostermontag"));

			//8. Erster Mai, Tag der Arbeit
			holidays.Add(new Holiday(new DateTime(year, 5, 1), GermanHolidays.TagDerArbeit, "Tag der Arbeit"));

			//9. (Christi-)Himmelfahrt(stag)
			holidays.Add(new Holiday(osterSonntag.AddDays(39), GermanHolidays.ChristiHimmelfahrt, "Christi Himmelfahrt"));

			//10. Pfingstsonntag
			if (
				state == GermanFederalStates.All ||
				state == GermanFederalStates.Brandenburg)
			{
				holidays.Add(new Holiday(osterSonntag.AddDays(49), GermanHolidays.Pfingstsonntag, "Pfingstsonntag"));
			}

			//11. Pfingstmontag
			holidays.Add(new Holiday(osterSonntag.AddDays(50), GermanHolidays.Pfingstmontag, "Pfingstmontag"));

			//12. Fronleichnam(stag)
			if (
				state == GermanFederalStates.All ||
				state == GermanFederalStates.BadenWuerttemberg ||
				state == GermanFederalStates.Bayern ||
				state == GermanFederalStates.Hessen ||
				state == GermanFederalStates.NordrheinWestfalen ||
				state == GermanFederalStates.RheinlandPfalz ||
				state == GermanFederalStates.Saarland)
			{
				holidays.Add(new Holiday(osterSonntag.AddDays(60), GermanHolidays.Fronleichnam, "Fronleichnam"));
			}

			//13. Augsburger Hohes Friedensfest (nur in Augsburg)

			//14. Mariä Himmelfahrt
			if (
				state == GermanFederalStates.All ||
				state == GermanFederalStates.Saarland)
			{
				holidays.Add(new Holiday(new DateTime(year, 8, 15), GermanHolidays.MariaHimmelfahrt, "Mariä Himmelfahrt"));
			}

			//15. Tag der dt. Einheit
			holidays.Add(new Holiday(new DateTime(year, 10, 3), GermanHolidays.TagDerDeutschenEinheit, "Tag der dt. Einheit"));

			//16. 500 Jahre Reformationstag
			if (year == 2017)
			{
				holidays.Add(new Holiday(new DateTime(2017, 10, 31), GermanHolidays.Reformationstag, "Reformationstag"));
			}
			else
			{
				//16. Reformationstag/-fest
				if (
					state == GermanFederalStates.All ||
					state == GermanFederalStates.Brandenburg ||
					(state == GermanFederalStates.Niedersachsen && year >= 2018) || 
					state == GermanFederalStates.MecklenburgVorpommern ||
					state == GermanFederalStates.Hamburg ||
					state == GermanFederalStates.Sachsen ||
					state == GermanFederalStates.SachsenAnhalt ||
					state == GermanFederalStates.SchleswigHolstein ||
					state == GermanFederalStates.Thueringen)
				{
					holidays.Add(new Holiday(new DateTime(year, 10, 31), GermanHolidays.Reformationstag, "Reformationstag"));
				}

			}

			//17. Allerheiligen(tag)
			if (
				state == GermanFederalStates.All ||
				state == GermanFederalStates.BadenWuerttemberg ||
				state == GermanFederalStates.Bayern ||
				state == GermanFederalStates.NordrheinWestfalen ||
				state == GermanFederalStates.RheinlandPfalz ||
				state == GermanFederalStates.Saarland)
			{
				holidays.Add(new Holiday(new DateTime(year, 11, 1), GermanHolidays.Allerheiligen, "Allerheiligen"));
			}

			//18. Buß und Bettag
			if (
				state == GermanFederalStates.All ||
				state == GermanFederalStates.Sachsen)
			{
				DateTime referenceDay = new DateTime(year, 11, 23);
				do
				{
					referenceDay = referenceDay.AddDays(-1);
				}
				while (referenceDay.DayOfWeek != DayOfWeek.Wednesday);

				holidays.Add(new Holiday(referenceDay, GermanHolidays.BussUndBettag, "Buß- und Bettag"));
			}

			//19. 1. Weihnachtstag
			holidays.Add(new Holiday(new DateTime(year, 12, 25), GermanHolidays.Weihnachtsfeiertag1, "1. Weihnachtstag"));

			//20. 2. Weihnachtstag
			holidays.Add(new Holiday(new DateTime(year, 12, 26), GermanHolidays.Weihnachtsfeiertag2, "2. Weihnachtstag"));

			return holidays;
		}
		#endregion

		#region GetOsterSonntag
		/// <summary>
		/// Gets the day of Ostersonntag in the year of the specified DateTime value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		internal static DateTime GetOsterSonntag(this DateTime value)
		{
			Int32 g, h, c, j, l, i;

			g = value.Year % 19;
			c = value.Year / 100;
			h = ((c - (c / 4)) - (((8 * c) + 13) / 25) + (19 * g) + 15) % 30;
			i = h - (h / 28) * (1 - (29 / (h + 1)) * ((21 - g) / 11));
			j = (value.Year + (value.Year / 4) + i + 2 - c + (c / 4)) % 7;

			l = i - j;
			Int32 month = (Int32)(3 + ((l + 40) / 44));
			Int32 day = (Int32)(l + 28 - 31 * (month / 4));

			return new DateTime(value.Year, month, day);
		}
		#endregion

		#region Tomorrow
		/// <summary>
		/// Get the day after the specified day.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static DateTime Tomorrow(this DateTime date)
		{
			return date.AddDays(1);
		}
		#endregion

		#region Yesterday
		/// <summary>
		/// Gets the day before the specified date.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns></returns>
		public static DateTime Yesterday(this DateTime date)
		{
			return date.AddDays(-1);
		}
		#endregion
	}
}