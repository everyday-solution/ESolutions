using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Core
{
	public static class GuidExtender
	{
		#region ToShortString
		/// <summary>
		/// To the short string.
		/// </summary>
		/// <param name="guid">The unique identifier.</param>
		/// <returns></returns>
		public static String ToShortString(this Guid guid)
		{
			return guid.ToString().Replace("-", "");
		}
		#endregion
	}
}
