using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Test
{
	[TestClass]
	public class ExceptionExtenderTests
	{
		#region TestDeepParse
		[TestMethod]
		public void TestDeepParse()
		{
			Exception ex0_3 = new Exception("Layer0_3");
			Exception ex0_2 = new Exception("Layer0_2", ex0_3);
			Exception ex0_1 = new Exception("Layer0_1", ex0_2);
			Exception ex0_0 = new Exception("Layer0_0", ex0_1);

			Assert.AreEqual("Layer0_0\r\nLayer0_1\r\nLayer0_2\r\nLayer0_3\r\n", ex0_0.DeepParse());
		}
		#endregion

		#region TestDeepParseWithAggregatedExceptions
		[TestMethod]
		public void TestDeepParseWithAggregatedExceptions()
		{
			Exception ex0_3 = new Exception("0_3");
			Exception ex0_2 = new Exception("0_2", ex0_3);
			Exception ex0_1 = new Exception("0_1", ex0_2);
			Exception ex0_0 = new Exception("0_0", ex0_1);

			Exception ex1_1 = new Exception("1_1");
			Exception ex1_0 = new Exception("1_0", ex1_1);

			AggregateException exActual = new AggregateException("aggregated", ex0_0, ex1_0);

			var expected = "aggregated\r\n====\r\n0_0\r\n0_1\r\n0_2\r\n0_3\r\n====\r\n1_0\r\n1_1\r\n";
			Assert.AreEqual(expected, exActual.DeepParse());
		}
		#endregion
	}
}
