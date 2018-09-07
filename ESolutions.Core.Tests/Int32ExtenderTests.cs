using System;
using ESolutions;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace ESolutions.Core.Test
{
	public class Int32ExtenderTests
	{
		#region TestTimes
		[Fact]
		public void TestTimes()
		{
			Int32 counter = 0;
			5.Times(runner =>
			{
				counter++;
			});

			Assert.Equal(5, counter);
		}
		#endregion

		#region TestTimes2
		[Fact]
		public void TestTimes2()
		{
			var times = 5.Times();

			Assert.Equal(5, times.Count());

			Int32 counter = 0;

			foreach (var runner in times)
			{
				Assert.Equal(counter, runner);
				counter++;
			}
		}
		#endregion

		#region TestTimes3
		[Fact]
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

			Assert.Equal(5, result.Count());
			Assert.Equal(generated[0], result[0].Id);
			Assert.Equal(generated[1], result[1].Id);
			Assert.Equal(generated[2], result[2].Id);
			Assert.Equal(generated[3], result[3].Id);
		}
		#endregion
	}
}
