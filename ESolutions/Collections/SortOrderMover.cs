using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Collection
{
	/// <summary>
	/// This class reorders the index of ISortOrder objects nad provides moving elements forth and back in lists.
	/// </summary>
	public static class SortOrderMover
	{
		#region MoveUp
		/// <summary>
		/// Moves the specfied item one position down in the collection.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="item">The item.</param>
		public static void MoveUp(this IEnumerable<ISortOrder> collection, ISortOrder item)
		{
			SortOrderMover.Reorder(collection);

			if (collection.CanMoveUp(item))
			{
				var orderedItems = collection.OrderBy(c => c.SortOrder).ToList();
				Int32 myIndex = orderedItems.IndexOf(item);
				orderedItems[myIndex - 1].SortOrder++;
				item.SortOrder--;
			}
		}
		#endregion

		#region MoveDown
		/// <summary>
		/// Moves the specfied item one position up in the collection.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="item">The item.</param>
		public static void MoveDown(this IEnumerable<ISortOrder> collection, ISortOrder item)
		{
			SortOrderMover.Reorder(collection);

			if (collection.CanMoveDown(item))
			{
				var orderedItems = collection.OrderBy(c => c.SortOrder).ToList();
				Int32 myIndex = orderedItems.IndexOf(item);
				orderedItems[myIndex + 1].SortOrder--;
				item.SortOrder++;
			}
		}
		#endregion

		#region Reorder
		private static void Reorder(IEnumerable<ISortOrder> collection)
		{
			Int32 index = 0;
			foreach (ISortOrder current in collection.OrderBy(c => c.SortOrder))
			{
				current.SortOrder = index;
				index++;
			}
		}
		#endregion

		#region CanMoveDown
		/// <summary>
		/// Determines whether this this specified item can be moved down in the collection.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="item">The item.</param>
		/// <returns>
		///   <c>true</c> if this instance [can move down] the specified collection; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean CanMoveDown(this IEnumerable<ISortOrder> collection, ISortOrder item)
		{
			return item != collection.OrderBy(c => c.SortOrder).LastOrDefault();
		}
		#endregion

		#region CanMoveUp
		/// <summary>
		/// Determines whether this this specified item can be moved up in the collection.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="item">The item.</param>
		/// <returns>
		///   <c>true</c> if this instance [can move up] the specified collection; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean CanMoveUp(this IEnumerable<ISortOrder> collection, ISortOrder item)
		{
			return item != collection.OrderBy(c => c.SortOrder).FirstOrDefault();
		}
		#endregion
	}
}
