using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Test
{
	[TestClass]
	public class SettingsTests
	{
		[TestMethod]
		public void ReadSettings()
		{
			Assert.AreEqual("33", MySettings.Default.TestValue);
		}
	}
}
