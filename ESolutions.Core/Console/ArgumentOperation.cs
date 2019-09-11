using System;
using System.Collections.Generic;
using System.Text;

namespace ESolutions.Core.Console
{
	#region ArgumentOperation
	public class ArgumentOperation
	{
		//Properties
		#region ArgumentSwitch
		public String ArgumentSwitch
		{
			get;
			set;
		}
		#endregion

		#region Description
		public String Description
		{
			get;
			set;
		}
		#endregion

		#region Operation
		public Action<IEnumerable<String>> Operation
		{
			get;
			set;
		}
		#endregion

		//Constructor
		#region ArgumentOperation
		public ArgumentOperation(String argumentSwitch, String description, Action<IEnumerable<String>> operation)
		{
			this.ArgumentSwitch = argumentSwitch;
			this.Description = description;
			this.Operation = operation;
		}
		#endregion
	}
	#endregion
}
