using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions
{
	/// <summary>
	/// Extender class for the DateTime class.
	/// </summary>
	public static class DateTimeExtender
	{
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
			Dictionary<DateTime, long> pairs = new Dictionary<DateTime, long>();

			for (Int32 year = referenceDate.Year - 1; year <= referenceDate.Year + 1; year++)
			{
				DateTime date = new DateTime(year, me.Month, me.Day);
				long distance = Math.Abs(date.Ticks - referenceDate.Ticks);
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
		/// <param name="state">The state.</param>
		/// <returns>
		///   <c>true</c> if the date is a holiday in germany; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsGermanHoliday(this DateTime value, GermanFederalStates state)
		{
			return value.GetGermanHolidays(state).ContainsKey(value.Date);
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
			return value.GetGermanHolidays(GermanFederalStates.Unknown).ContainsKey(value.Date);
		}
		#endregion

		#region GetGermanHolidayOfYear
		public static SortedList<DateTime, String> GetGermanHolidays(this DateTime value, GermanFederalStates state)
		{
			SortedList<DateTime, String> result = new SortedList<DateTime, string>();
			Int32 year = value.Year;
			DateTime osterSonntag = value.GetOsterSonntag();

			//Neujahr
			result.Add(new DateTime(year, 1, 1), "Neujahr");

			//Heilige Drei Könige
			if (
				state == GermanFederalStates.BadenWuerttemberg ||
				state == GermanFederalStates.Bayern ||
				state == GermanFederalStates.SachsenAnhalt)
			{
				result.Add(new DateTime(year, 1, 6), "Heilige Drei Könige");
			}

			//Karfreitag
			result.Add(osterSonntag.AddDays(-2), "Karfreitag");

			//Ostersonntag
			result.Add(osterSonntag, "Ostersonntag");

			//Ostermontag
			result.Add(osterSonntag.AddDays(1), "Ostermontag");

			//Tag der Arbeit
			result.Add(new DateTime(year, 5, 1), "Tag der Arbeit");

			//Christi Himmelfahrt
			result.Add(osterSonntag.AddDays(39), "Christi Himmelfahrt");

			//Pfingstsonntag
			result.Add(osterSonntag.AddDays(49), "Pfingstsonntag");

			//Pfingstmontag
			result.Add(osterSonntag.AddDays(50), "Pfingstmontag");

			//Fronleichnam
			if (
				state == GermanFederalStates.BadenWuerttemberg ||
				state == GermanFederalStates.Bayern ||
				state == GermanFederalStates.Hessen ||
				state == GermanFederalStates.NordrheinWestfalen ||
				state == GermanFederalStates.RheinlandPfalz ||
				state == GermanFederalStates.Saarland)
			{
				result.Add(osterSonntag.AddDays(60), "Fronleichnam");
			}

			//Mariä Himmelfahrt
			if (state == GermanFederalStates.Saarland)
			{
				result.Add(new DateTime(year, 8, 15), "Mariä Himmelfahrt");
			}

			//Tag der dt. Einheit
			result.Add(new DateTime(year, 10, 3), "Tag der dt. Einheit");

			//500 Jahre Reformationstag
			if (year == 2017)
			{
				result.Add(new DateTime(2017, 10, 31), "Reformationstag");
			}
			else
			{
				//Reformationstag
				if (
					state == GermanFederalStates.Brandenburg ||
					state == GermanFederalStates.MecklenburgVorpommern ||
					state == GermanFederalStates.Sachsen ||
					state == GermanFederalStates.SachsenAnhalt ||
					state == GermanFederalStates.Thueringen)
				{
					result.Add(new DateTime(year, 10, 31), "Reformationstag");
				}
			}

			//Allerheiligen
			if (
				state == GermanFederalStates.BadenWuerttemberg ||
				state == GermanFederalStates.Bayern ||
				state == GermanFederalStates.NordrheinWestfalen ||
				state == GermanFederalStates.RheinlandPfalz ||
				state == GermanFederalStates.Saarland)
			{
				result.Add(new DateTime(year, 11, 1), "Allerheiligen");
			}

			//Buß und Bettag
			if (state == GermanFederalStates.Sachsen)
			{
				DateTime referenceDay = new DateTime(year, 11, 23);
				do
				{
					referenceDay = referenceDay.AddDays(-1);
				}
				while (referenceDay.DayOfWeek != DayOfWeek.Wednesday);

				result.Add(referenceDay, "Buß- und Bettag");
			}

			//1. Weihnachtstag
			result.Add(new DateTime(year, 12, 25), "1. Weihnachtstag");

			//2. Weihnachtstag
			result.Add(new DateTime(year, 12, 26), "2. Weihnachtstag");

			return result;
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