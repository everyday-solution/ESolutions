using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
	/// <summary>
	/// Extends IEnumerables.
	/// </summary>
	public static class LinqExtender
	{
		#region Do
		/// <summary>
		/// Does the specified action.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="action">The action.</param>
		public static void Do<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T element in source)
			{
				action(element);
			}
		}
		#endregion
	}
}
