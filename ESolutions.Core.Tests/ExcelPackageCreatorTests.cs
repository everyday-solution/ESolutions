using System;
using System.Linq;
using ESolutions;
using ESolutions.Core.IO;
using Xunit;

namespace ESolutions.Test
{
	public class ExcelPackageCreatorTests
	{
		[Fact]
		public void TestPreparePackage()
		{
			var creator = new ExcelPackageCreator("testSheet");

			creator.WriteCells("A1", "B1", "C1", "D1");
			creator.NextRow();
			creator.WriteCells("B2", "B2", "C2", "D2");
			creator.NextRow();
			creator.WriteCells("B3", "B3", "C3", "D3");

			var package = creator.PreparePackage();

			Assert.Equal(1, package.Workbook.Worksheets.Count);
			Assert.Equal(3, package.Workbook.Worksheets[0].Rows.Count());
			Assert.Equal(4, package.Workbook.Worksheets[0].Columns.Count());
		}
	}
}
