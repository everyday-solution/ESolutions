using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions
{
	/// <summary>
	/// Extensions methods for Int32
	/// </summary>
	public static class Int32Extender
	{
		#region Times
		/// <summary>
		/// repeates the callback for the specified amount of times.
		/// </summary>
		/// <param name="times">The times.</param>
		/// <param name="callback">The callback.</param>
		public static void Times(this Int32 times, Action<Int32> callback)
		{
			for (Int32 index = 0; index < times; index++)
			{
				callback(index);
			}
		}
		#endregion

		#region Times
		/// <summary>
		/// Returns an IEnumerable with the numbers from 0 to times.
		/// </summary>
		/// <param name="times">The times.</param>
		/// <returns></returns>
		/// <remarks>
		/// Can be used to replace for loops. This is just 4 % slower than a for loop.
		/// </remarks>
		public static IEnumerable<Int32> Times(this Int32 times)
		{
			Int32 index = 0;
			foreach (var runner in Enumerable.Range(0, times))
			{
				yield return index++;
			}
		}
		#endregion

		#region Times
		/// <summary>
		/// Returns an IEnumerable with the numbers from 0 to times.
		/// </summary>
		/// <param name="times">The times.</param>
		/// <returns></returns>
		/// <remarks>
		/// Can be used to replace for loops. This is just 4 % slower than a for loop.
		/// </remarks>
		public static IEnumerable<T> Times<T>(this Int32 times, Func<Int32, T> callback)
		{
			List<T> result = new List<T>();

			for (Int32 index = 0; index < times; index++)
			{
				result.Add(callback(index));
			}

			return result;
		}
		#endregion
	}
}
