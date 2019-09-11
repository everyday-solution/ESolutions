using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Core.Console
{
	public class MenuItem
	{
		//Properties
		#region Key
		public Char Key
		{
			get;
			private set;
		}
		#endregion

		#region Text
		public String Text
		{
			get;
			private set;
		}
		#endregion

		#region Action
		public Action<IEnumerable<String>> Action
		{
			get;
			private set;
		}
		#endregion

		//Constructor
		#region MenuItem
		public MenuItem(Char key, String text, Action<IEnumerable<String>> action)
		{
			this.Key = key;
			this.Text = text;
			this.Action = action;
		}
		#endregion
	}
}
