using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Core
{
	/// <summary>
	/// Extender for the class System.Exception
	/// </summary>
	public static class ExceptionExtender
	{
		#region DeepParse
		/// <summary>
		/// Returns the message of the exception and all innerexceptions sepearted by Environment.NewLine.
		/// </summary>
		/// <param name="ex">The exception to be deep parsed.</param>
		/// <returns></returns>
		public static String DeepParse(this System.Exception ex)
		{
			String result = String.Empty;

			if (ex is AggregateException castedEx)
			{
				result += castedEx.Message + Environment.NewLine;
				foreach (var aggregateRunner in castedEx.InnerExceptions)
				{
					result += "====" + Environment.NewLine;
					Exception runner = aggregateRunner;
					while (runner != null)
					{
						result += runner.Message + Environment.NewLine;
						runner = runner.InnerException;
					}
					
				}
			}
			else
			{
				Exception runner = ex;
				while (runner != null)
				{
					result += runner.Message + Environment.NewLine;
					runner = runner.InnerException;
				}
			}

			return result;
		}
		#endregion
	}
}
