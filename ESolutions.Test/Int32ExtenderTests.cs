using System;
using ESolutions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace ESolutions.Test
{
	[TestClass]
	public class Int32ExtenderTests
	{
		#region TestTimes
		[TestMethod]
		public void TestTimes()
		{
			Int32 counter = 0;
			5.Times(runner =>
			{
				counter++;
			});

			Assert.AreEqual(5, counter);
		}
		#endregion

		#region TestTimes2
		[TestMethod]
		public void TestTimes2()
		{
			var times = 5.Times();

			Assert.AreEqual(5, times.Count());

			Int32 counter = 0;

			foreach (var runner in times)
			{
				Assert.AreEqual(counter, runner);
				counter++;
			}
		}
		#endregion

		#region TestTimes3
		[TestMethod]
		public void TestTimes3()
		{
			List<Int32> generated = new List<int>();
			Random randomizer = new Random();
			var result = 5.Times(runner =>
			{
				var rand = randomizer.Next(1, 1000);
				generated.Add(rand);
				return new { Id = rand };
			}).ToArray();

			Assert.AreEqual(5, result.Count());
			Assert.AreEqual(generated[0], result[0].Id);
			Assert.AreEqual(generated[1], result[1].Id);
			Assert.AreEqual(generated[2], result[2].Id);
			Assert.AreEqual(generated[3], result[3].Id);
		}
		#endregion
	}
}
