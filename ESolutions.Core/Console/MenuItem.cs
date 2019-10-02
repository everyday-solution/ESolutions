using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Core.Console
{
	/// <summary>
	/// Menu items that can be used for a console application selection menu.
	/// </summary>
	public class MenuItem
	{
		//Properties
		#region Key
		/// <summary>
		/// Gets the char key triggering the menu action.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public Char Key
		{
			get;
			private set;
		}
		#endregion

		#region Text
		/// <summary>
		/// Gets the text the menu will diplay in the console for the menu item.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public String Text
		{
			get;
			private set;
		}
		#endregion

		#region Action
		/// <summary>
		/// Gets the action that is triggered when hitting the associated char key.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public Action<IEnumerable<String>> Action
		{
			get;
			private set;
		}
		#endregion

		//Constructor
		#region MenuItem
		/// <summary>
		/// Initializes a new instance of the <see cref="MenuItem"/> class.
		/// </summary>
		/// <param name="key">Gets the char key triggering the menu action.</param>
		/// <param name="text">Gets the text the menu will diplay in the console for the menu item.</param>
		/// <param name="action">Gets the action that is triggered when hitting the associated char key.</param>
		public MenuItem(Char key, String text, Action<IEnumerable<String>> action)
		{
			this.Key = key;
			this.Text = text;
			this.Action = action;
		}
		#endregion
	}
}
