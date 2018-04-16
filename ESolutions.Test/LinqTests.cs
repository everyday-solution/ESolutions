using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ESolutions.Test
{
	[TestClass]
	public class LinqTests
	{
		//TestClass
		#region TestClass
		public class TestClass
		{
			#region Index
			public Int32 Index
			{
				get;
				set;
			}
			#endregion
		}
		#endregion

		#region DoWorking
		[TestMethod]
		public void DoWorking()
		{
			List<TestClass> items = new List<TestClass>();
			items.Add(new TestClass() { Index = 0 });
			items.Add(new TestClass() { Index = 1 });
			items.Add(new TestClass() { Index = 2 });

			items.Do(runner => runner.Index++);

			Assert.AreEqual(1, items[0].Index);
			Assert.AreEqual(2, items[1].Index);
			Assert.AreEqual(3, items[2].Index);
		}
		#endregion
	}
}
