using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESolutions.Core
{
	[global::System.Serializable]
	public class ConverterException : System.Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class.
		/// </summary>
		public ConverterException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public ConverterException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		public ConverterException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
