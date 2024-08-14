using System;
using System.Linq;
using ESolutions.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Test
{
	[TestClass]
	public class ExcelPackageCreatorTests
	{
		[TestMethod]
		public void TestPreparePackage()
		{
			var creator = new ExcelPackageCreator("testSheet");

			creator.WriteCells("A1", "B1", "C1", "D1");
			creator.NextRow();
			creator.WriteCells("B2", "B2", "C2", "D2");
			creator.NextRow();
			creator.WriteCells("B3", "B3", "C3", "D3");

			var package = creator.PreparePackage();

			Assert.AreEqual(1, package.Workbook.Worksheets.Count);
			Assert.AreEqual(3, package.Workbook.Worksheets[0].Rows.Count());
			Assert.AreEqual(4, package.Workbook.Worksheets[0].Columns.Count());
		}
	}
}
