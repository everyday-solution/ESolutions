using System;
using ESolutions;
using Xunit;

namespace ESolutions.Core.Test
{
	public class GuidExtenderTests
	{
		#region TestToShortString
		[Fact]
		public void TestToShortString()
		{
			String rawGuid = "FD9DB2EE7C7B41E9BBE21FADA0A4F697";
			Guid guid = new Guid(rawGuid);

			Assert.Equal(rawGuid.ToLower(), guid.ToShortString());
		}
		#endregion
	}
}
