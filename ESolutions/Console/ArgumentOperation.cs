using System;
using System.Collections.Generic;
using System.Text;

namespace ESolutions.Console
{
	/// <summary>
	/// Operation tied to a console argument.
	/// </summary>
	public class ArgumentOperation
	{
		//Properties
		#region ArgumentSwitch
		/// <summary>
		/// Gets or sets argument switch triggering the associated operation.
		/// </summary>
		/// <value>
		/// The argument switch.
		/// </value>
		public String ArgumentSwitch
		{
			get;
			set;
		}
		#endregion

		#region Description
		/// <summary>
		/// Gets or sets the description that is displayed when calling help.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public String Description
		{
			get;
			set;
		}
		#endregion

		#region Operation
		/// <summary>
		/// Gets or sets the operation that is triggered if the command line contained the argumentSwitch.
		/// </summary>
		/// <value>
		/// The operation.
		/// </value>
		public Action<IEnumerable<String>> Operation
		{
			get;
			set;
		}
		#endregion

		//Constructor
		#region ArgumentOperation
		/// <summary>
		/// Initializes a new instance of the <see cref="ArgumentOperation"/> class.
		/// </summary>
		/// <param name="argumentSwitch">The argument switch triggering the associated operation.</param>
		/// <param name="description">The description that is displayed when calling help.</param>
		/// <param name="operation">The operation that is triggered if the command line contained the argumentSwitch.</param>
		public ArgumentOperation(String argumentSwitch, String description, Action<IEnumerable<String>> operation)
		{
			this.ArgumentSwitch = argumentSwitch;
			this.Description = description;
			this.Operation = operation;
		}
		#endregion
	}
}
