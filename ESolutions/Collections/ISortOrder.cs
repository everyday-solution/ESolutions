using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Collection
{
	/// <summary>
	/// Interface for classes that can have a sort order inside of lists.
	/// </summary>
	public interface ISortOrder
	{
		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>
		/// The sort order.
		/// </value>
		Int32 SortOrder
		{
			get;
			set;
		}
	}
}
