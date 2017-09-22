using System;
using System.Collections.Generic;
using System.Text;

namespace ESolutions
{
	/// <summary>
	/// Result of a validation process.
	/// </summary>
	public class ValidationResult
	{
		//Fields
		#region errorMessages
		private IList<String> errorMessages = new List<String>();
		#endregion

		//Properties
		#region IsValid
		public Boolean IsValid
		{
			get
			{
				return this.errorMessages.Count == 0;
			}
		}
		#endregion

		#region ErrorMessages
		public IList<String> ErrorMessages
		{
			get
			{
				return this.errorMessages;
			}
		}
		#endregion

		//Methods
		#region GetErrorMessage
		public String GetErrorMessage()
		{
			System.Text.StringBuilder result = new StringBuilder();

			foreach (String current in this.errorMessages)
			{
				result.AppendLine(current);
			}

			return result.ToString();
		}
		#endregion
	}
}
