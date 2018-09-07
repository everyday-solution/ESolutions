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

		/// <summary>
		/// Initializes a new instance of the <see cref="Exception"/> class.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
		protected ConverterException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{
		}
	}
}
