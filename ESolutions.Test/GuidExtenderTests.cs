using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESolutions;

namespace ESolutions.Test
{
	[TestClass]
	public class GuidExtenderTests
	{
		#region TestToShortString
		[TestMethod]
		public void TestToShortString()
		{
			String rawGuid = "FD9DB2EE7C7B41E9BBE21FADA0A4F697";
			Guid guid = new Guid(rawGuid);

			Assert.AreEqual(rawGuid.ToLower(), guid.ToShortString());
		}
		#endregion
	}
}
