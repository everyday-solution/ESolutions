using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ESolutions;
using System.Linq;
using System.Globalization;

namespace ESolutions
{
	/// <summary>
	/// Extender class for useful string operations.
	/// </summary>
	public static class StringExtender
	{
		#region IsBoolean
		/// <summary>
		/// Determines whether this instance is boolean.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static Boolean IsBoolean(this String input)
		{
			Boolean result = true;

			try
			{
				result = input.ToBoolean();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region ToBoolean
		/// <summary>
		/// Converts the string to an Boolean if possbile.
		/// </summary>
		/// <param name="input">The string.</param>
		/// <returns>The string a an integer</returns>
		/// <exception cref="ConverterException">Is thrown if the string does not contain a valid integer.</exception>
		public static Boolean ToBoolean(this String input)
		{
			Boolean result = false;

			try
			{
				result = Boolean.Parse(input);
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForBoolean, ex);
			}

			return result;
		}
		#endregion

		#region IsGuid
		/// <summary>
		/// Determines whether the string value is a valid guid
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		///   <c>true</c> if input string is a guid; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsGuid(this String input)
		{
			Boolean result = true;

			try
			{
				input.ToGuid();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region IsGuid
		/// <summary>
		/// Determines whether the string value is a valid guid
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		///   <c>true</c> if input string is a guid; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsGuid(this Object input)
		{
			Boolean result = true;

			try
			{
				input.ToGuid();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region ToGuid
		/// <summary>
		/// Converts the string to an guid if possbile.
		/// <param name="input">The string.</param>
		/// <returns>The string a an giud</returns>
		/// <exception cref="ConverterException">Is thrown if the string does not contain a valid giud.</exception>
		public static Guid ToGuid(this String input)
		{
			Guid result;

			try
			{
				result = new Guid(input);
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForGuid, ex);
			}

			return result;
		}
		#endregion

		#region ToGuid
		/// <summary>
		/// Converts the string to an guid if possbile.
		/// <param name="input">The string.</param>
		/// <returns>The string a an giud</returns>
		/// <exception cref="ConverterException">Is thrown if the string does not contain a valid giud.</exception>
		public static Guid ToGuid(this Object input)
		{
			Guid result;

			try
			{
				result = new Guid(input.ToString());
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForGuid, ex);
			}

			return result;
		}
		#endregion

		#region ToShortGuid
		/// <summary>
		/// Converts a guid to a short string without dashed or other obstacles
		/// </summary>
		/// <param name="guid">The unique identifier.</param>
		/// <returns></returns>
		public static String ToShortString(this Guid guid)
		{
			return guid.ToString().Replace("-", String.Empty);
		}
		#endregion

		#region IsInt32
		/// <summary>
		/// Determines whether the specified input is int32.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		///   <c>true</c> if the specified input is int32; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsInt32(this String input)
		{
			Boolean result = true;

			try
			{
				input.ToInt32();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region ToInt32
		/// <summary>
		/// Converts the string to an Int32 if possbile.
		/// </summary>
		/// <param name="input">The string.</param>
		/// <returns>The string a an integer</returns>
		/// <exception cref="ConverterException">Is thrown if the string does not contain a valid integer.</exception>
		public static int ToInt32(this String input)
		{
			Int32 result = 0;

			try
			{
				result = Int32.Parse(input);
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForInt32, ex);
			}

			return result;
		}
		#endregion

		#region ToInt32
		public static Int32 ToInt32(this String input, Int32 defaultValue)
		{
			Int32 result = defaultValue;

			try
			{
				result = Int32.Parse(input);
			}
			catch
			{
			}

			return result;
		}
		#endregion

		#region IsDouble
		/// <summary>
		/// Determines whether the specified input is double.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		///   <c>true</c> if the specified input is double; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsDouble(this String input)
		{
			Boolean result = true;

			try
			{
				input.ToDouble();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region ToDouble
		/// <summary>
		/// Converts the input string into a double if possible
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		public static double ToDouble(this String input)
		{
			return StringExtender.ToDouble(input, Thread.CurrentThread.CurrentCulture);
		}
		#endregion

		#region ToDouble
		/// <summary>
		/// Removes all characters from the string and converts it into a double.
		/// </summary>
		/// <param name="input">The input string.</param>
		/// <returns>The converted double value</returns>
		/// <exception cref="ConverterException">Thrown if the input string can not be converted.</exception>
		public static double ToDouble(this String input, CultureInfo culture)
		{
			Double result = 0.0;

			try
			{
				String decimalSeperator = Regex.Escape(culture.NumberFormat.NumberDecimalSeparator);
				String pattern = @"\d{1,}" + decimalSeperator + @"{0,1}\d{0,}";
				MatchCollection matches = Regex.Matches(input, pattern);
				String cleanedString = String.Empty;

				foreach (Match current in matches)
				{
					cleanedString += current.Value;
				}

				if (String.IsNullOrEmpty(cleanedString))
				{
					throw new Exception(StringTable.InvalidFormatNoDigits);
				}

				result = Convert.ToDouble(cleanedString, culture);

				if (input.StartsWith("-"))
				{
					result *= -1;
				}
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForDouble, ex);
			}

			return result;
		}
		#endregion

		#region IsDecimal
		/// <summary>
		/// Determines whether the specified input is Decimal.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		///   <c>true</c> if the specified input is Decimal; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsDecimal(this String input)
		{
			Boolean result = true;

			try
			{
				input.ToDecimal();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region ToDecimal
		/// <summary>
		/// Removes all characters from the string and converts it into a Decimal.
		/// </summary>
		/// <param name="input">The input string.</param>
		/// <returns>The converted Decimal value</returns>
		/// <exception cref="ConverterException">Thrown if the input string can not be converted.</exception>
		public static Decimal ToDecimal(this String input)
		{
			Decimal result = 0;

			try
			{
				String decimalSeperator =
					input.IndexOf(',') < input.IndexOf('.') ?
					Regex.Escape(".") :
					Regex.Escape(",");

				String pattern = @"\d{1,}" + decimalSeperator + @"{0,1}\d{0,}";
				MatchCollection matches = Regex.Matches(input, pattern);
				String cleanedString = String.Empty;

				foreach (Match current in matches)
				{
					cleanedString += current.Value;
				}

				if (!String.IsNullOrEmpty(cleanedString))
				{
					var culture = decimalSeperator == "," ? CultureInfo.GetCultureInfo("de-DE") : CultureInfo.InvariantCulture;
					result = Convert.ToDecimal(cleanedString, culture);
				}

				if (input.StartsWith("-"))
				{
					result *= -1;
				}
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForDecimal, ex);
			}

			return result;
		}
		#endregion

		#region IsDateTime
		/// <summary>
		/// Determines whether [is date time] [the specified input].
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>
		///   <c>true</c> if [is date time] [the specified input]; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsDateTime(this String input)
		{
			Boolean result = true;

			try
			{
				input.ToDateTime();
			}
			catch
			{
				result = false;
			}

			return result;
		}
		#endregion

		#region ToDateTime
		/// <summary>
		/// Converts the string to an Boolean if possbile.
		/// </summary>
		/// <param name="input">The string.</param>
		/// <returns>The string a an integer</returns>
		/// <exception cref="ConverterException">Is thrown if the string does not contain a valid integer.</exception>
		public static DateTime ToDateTime(this String input)
		{
			DateTime result = DateTime.MinValue;

			try
			{
				result = DateTime.Parse(input);
			}
			catch (Exception ex)
			{
				throw new ConverterException(StringTable.InvalidFormatForDateTime, ex);
			}

			return result;
		}
		#endregion

		#region Underscore
		/// <summary>
		/// Convert all characters in the string to lower charaters and concats
		/// the words seperated by an underscore.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static String Underscore(this String text)
		{
			String result = String.Empty;

			String[] parts = Regex.Split(text, "(?<!^)(?=[A-Z])");

			foreach (String current in parts)
			{
				result += String.Format("{0}_", current.ToLower());
			}
			return result.Trim('_');
		}
		#endregion

		#region Camelize
		/// <summary>
		/// Capitalizes the first letter of each word in the string and
		/// removes all whitespaces.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static String Camelize(this String text)
		{
			String result = String.Empty;
			List<String> parts = text.Split('_').ToList();
			parts.ForEach(part =>
			{
				result += Char.ToUpper(part[0]) + part.Substring(1, part.Length - 1);
			});

			return result;
		}
		#endregion

		#region Split
		/// <summary>
		/// Splits the specified inuput into substring leaving out all splitmarkers 
		/// </summary>
		/// <param name="inuput">The inuput.</param>
		/// <returns></returns>
		public static List<String> Split(this String input, String splitMarker)
		{
			return input.Split(splitMarker, true);
		}
		#endregion

		#region Split
		/// <summary>
		/// Splits the specified inuput into substring leaving out all splitmarkers 
		/// </summary>
		/// <param name="inuput">The inuput.</param>
		/// <returns></returns>
		public static List<String> Split(this String input, String splitMarker, Boolean includeEmpty)
		{
			List<String> result = new List<String>();
			Int32 start = 0;
			Int32 end = 0;

			do
			{
				end = input.IndexOf(splitMarker, start);

				String substring = String.Empty;

				if (end >= 0)
				{
					substring = input.Substring(start, end - start);
					start = end + splitMarker.Length;
				}
				else
				{
					substring = input.Substring(start, input.Length - start);
				}

				if (substring != String.Empty || includeEmpty)
				{
					result.Add(substring);
				}
			}
			while (end >= 0);


			return result;
		}
		#endregion

		#region Split
		/// <summary>
		/// Splits the specified input string into parts marked by the splitmarker if it is sourrounded by the
		/// sourround by. This is used to extend the splitmarker without removing the sourrounding chars.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="splitMarker">The split marker.</param>
		/// <param name="surroundedBy">The surrounded by.</param>
		/// <returns></returns>
		public static List<String> Split(this String input, String splitMarker, String surroundedBy)
		{
			List<String> result = new List<String>();
			Int32 start = 0;
			Int32 end = 0;

			String adjustedSplitMarker = surroundedBy + splitMarker + surroundedBy;

			do
			{
				end = input.IndexOf(adjustedSplitMarker, start);

				String substring = String.Empty;

				if (end >= 0)
				{
					substring = input.Substring(start, end - start + surroundedBy.Length);
					start = end + splitMarker.Length + surroundedBy.Length;
				}
				else
				{
					substring = input.Substring(start, input.Length - start);
				}

				result.Add(substring);
			}
			while (end >= 0);


			return result;
		}
		#endregion
	}
}
