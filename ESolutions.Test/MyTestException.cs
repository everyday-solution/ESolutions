using System;
using System.Collections.Generic;
using System.Text;

namespace ESolutions.Test
{
	[global::System.Serializable]
	/// <summary>
	/// For guidelines regarding the creation of new exception types, see
	/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
	/// and
	/// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
	/// </summary>
	public class MyTestException : Exception
	{
		#region MyTestException
		public MyTestException ()
		{
		}
		#endregion

		#region MyTestException
		public MyTestException (string message) : base (message)
		{
		}
		#endregion

		#region MyTestException
		public MyTestException (string message, Exception inner) : base (message, inner)
		{
		}
		#endregion

		#region MyTestException
		protected MyTestException (
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base (info, context)
		{
		}
		#endregion
	}
}
