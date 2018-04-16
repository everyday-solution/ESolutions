using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.IO;
using ESolutions.Data;

namespace ESolutions.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class CsvFileTests
	{
		//Fields
		#region testContextInstance
		/// <summary>
		/// The test context instance
		/// </summary>
		private TestContext testContextInstance;
		#endregion

		//Properties
		#region TestContext
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#endregion

		//Methods
		#region TestThatCsvFileCantBeCreatedFromEmptyEncoding
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestThatCsvFileCantBeCreatedFromEmptyEncoding()
		{
			FileInfo nonExisting = new FileInfo("NonExisting.csv");
			CsvFile newFile = new CsvFile(nonExisting, null);
		}
		#endregion

		#region TestThatCsvFileCantBeCreatedFromNull
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestThatCsvFileCantBeCreatedFromNull()
		{
			CsvFile newFile = new CsvFile(null, null);
		}
		#endregion

		#region TestThatCsvFileCantBeCreateFromNonExistingFile
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestThatCsvFileCantBeCreateFromNonExistingFile()
		{
			FileInfo nonExisting = new FileInfo("NonExisting.csv");
			CsvFile newFile = new CsvFile(nonExisting, Encoding.UTF8);
		}
		#endregion

		#region TestThatCsvFileCanBeCreatedFromValidCsvWithoutColumnHeadsInFirstLine
		[TestMethod]
		public void TestThatCsvFileCanBeCreatedFromValidCsvWithoutColumnHeadsInFirstLine()
		{
			FileInfo testFile1 = new FileInfo(".\\Files\\TestFile1.csv");
			CsvFile actual = new CsvFile(testFile1, Encoding.UTF8);

			Assert.AreEqual(4, actual.Rows.Count);
			Assert.AreEqual(2, actual.Rows[0].Cells.Count);
			Assert.AreEqual(2, actual.Rows[1].Cells.Count);
			Assert.AreEqual(2, actual.Rows[2].Cells.Count);
			Assert.AreEqual(2, actual.Rows[3].Cells.Count);
			Assert.AreEqual("Hamburg", actual[0][0].Value);
			Assert.AreEqual("1000000", actual[0][1].Value);
			Assert.AreEqual("Berlin", actual[1][0].Value);
			Assert.AreEqual("3000000", actual[1][1].Value);
			Assert.AreEqual("Köln", actual[2][0].Value);
			Assert.AreEqual("1000000", actual[2][1].Value);
			Assert.AreEqual("Aachen", actual[3][0].Value);
			Assert.AreEqual("150000", actual[3][1].Value);
		}
		#endregion

		#region TestThatNonUniformAnsiFileCanBeRead
		[TestMethod]
		public void TestThatNonUniformAnsiFileCanBeRead()
		{
			FileInfo testFile2 = new FileInfo(".\\Files\\TestFile2.csv");
			CsvFile file = new CsvFile(testFile2, Encoding.GetEncoding(1256));

			Assert.AreEqual(18, file.Rows.Count);
			Assert.AreEqual(1, file.Rows[0].Cells.Count);
			Assert.AreEqual(1, file.Rows[1].Cells.Count);
			Assert.AreEqual(1, file.Rows[2].Cells.Count);
			Assert.AreEqual(36, file.Rows[3].Cells.Count);
			Assert.AreEqual(36, file.Rows[4].Cells.Count);
			Assert.AreEqual(36, file.Rows[5].Cells.Count);
			Assert.AreEqual(36, file.Rows[6].Cells.Count);
			Assert.AreEqual(36, file.Rows[7].Cells.Count);
			Assert.AreEqual(36, file.Rows[8].Cells.Count);
			Assert.AreEqual(36, file.Rows[9].Cells.Count);
			Assert.AreEqual(36, file.Rows[10].Cells.Count);
			Assert.AreEqual(36, file.Rows[11].Cells.Count);
			Assert.AreEqual(36, file.Rows[12].Cells.Count);
			Assert.AreEqual(36, file.Rows[13].Cells.Count);
			Assert.AreEqual(36, file.Rows[14].Cells.Count);
			Assert.AreEqual(1, file.Rows[15].Cells.Count);
			Assert.AreEqual(1, file.Rows[16].Cells.Count);
			Assert.AreEqual(1, file.Rows[17].Cells.Count);
		}
		#endregion

		#region TestThatCsvFileCanBeConvertedToDataTable
		[TestMethod]
		public void TestThatCsvFileCanBeConvertedToDataTable()
		{
			FileInfo testFile1 = new FileInfo(".\\Files\\TestFile1.csv");
			CsvFile file = new CsvFile(testFile1, Encoding.UTF8);
			DataTable actual = file.ToDataTable();

			Assert.AreEqual(4, actual.Rows.Count);
			Assert.AreEqual(2, actual.Rows[0].ItemArray.Length);
			Assert.AreEqual(2, actual.Rows[1].ItemArray.Length);
			Assert.AreEqual(2, actual.Rows[2].ItemArray.Length);
			Assert.AreEqual(2, actual.Rows[3].ItemArray.Length);
			Assert.AreEqual("Hamburg", actual.Rows[0].ItemArray[0].ToString());
			Assert.AreEqual("1000000", actual.Rows[0].ItemArray[1].ToString());
			Assert.AreEqual("Berlin", actual.Rows[1].ItemArray[0].ToString());
			Assert.AreEqual("3000000", actual.Rows[1].ItemArray[1].ToString());
			Assert.AreEqual("Köln", actual.Rows[2].ItemArray[0].ToString());
			Assert.AreEqual("1000000", actual.Rows[2].ItemArray[1].ToString());
			Assert.AreEqual("Aachen", actual.Rows[3].ItemArray[0].ToString());
			Assert.AreEqual("150000", actual.Rows[3].ItemArray[1].ToString());
		}
		#endregion

		#region TestThatSeperationSignCanBeConfigured
		[TestMethod]
		public void TestThatSeperationSignCanBeConfigured()
		{
			FileInfo testFile1 = new FileInfo(".\\Files\\TestFile3.csv");
			CsvFile actual = new CsvFile(testFile1, Encoding.UTF8, ';', null);

			Assert.AreEqual(4, actual.Rows.Count);
			Assert.AreEqual(2, actual.Rows[0].Cells.Count);
			Assert.AreEqual(2, actual.Rows[1].Cells.Count);
			Assert.AreEqual(2, actual.Rows[2].Cells.Count);
			Assert.AreEqual(2, actual.Rows[3].Cells.Count);
			Assert.AreEqual("Hamburg", actual[0][0].Value);
			Assert.AreEqual("1000000", actual[0][1].Value);
			Assert.AreEqual("Berlin", actual[1][0].Value);
			Assert.AreEqual("3000000", actual[1][1].Value);
			Assert.AreEqual("Köln", actual[2][0].Value);
			Assert.AreEqual("1000000", actual[2][1].Value);
			Assert.AreEqual("Aachen", actual[3][0].Value);
			Assert.AreEqual("150000", actual[3][1].Value);
		}
		#endregion

		#region TestThatDataTableCanBeConvertedToCsv
		[TestMethod]
		public void TestThatDataTableCanBeConvertedToCsv()
		{
			DataTable table = new DataTable();
			table.Columns.Add("one");
			table.Columns.Add("two");
			table.Rows.Add("1", "Zeile 1");
			table.Rows.Add("2", "Zeile 2");

			CsvFile file = new CsvFile(table);
			Assert.AreEqual("1", file.Rows[0].Cells[0].Value);
			Assert.AreEqual("Zeile 1", file.Rows[0].Cells[1].Value);
			Assert.AreEqual("2", file.Rows[1].Cells[0].Value);
			Assert.AreEqual("Zeile 2", file.Rows[1].Cells[1].Value);
		}
		#endregion

		#region TestThatCsvFileCanBeSaved
		[TestMethod]
		public void TestThatCsvFileCanBeSaved()
		{
			DataTable table = new DataTable();
			table.Columns.Add("one");
			table.Columns.Add("two");
			table.Rows.Add("1", "Zeile 1");
			table.Rows.Add("2", "Zeile 2");

			FileInfo newFile = new FileInfo(Path.GetTempFileName());
			Stream stream = newFile.Create();

			CsvFile file = new CsvFile(table);
			file.Save(stream, Encoding.UTF8, ";");

			stream.Close();

			FileStream readingStream = newFile.OpenRead();
			StreamReader reader = new StreamReader(readingStream);
			String content = reader.ReadToEnd();
			readingStream.Close();

			Assert.AreEqual('1', content[0]);
			Assert.AreEqual(';', content[1]);
			Assert.AreEqual('Z', content[2]);
			Assert.AreEqual('e', content[3]);
			Assert.AreEqual('i', content[4]);
			Assert.AreEqual('l', content[5]);
			Assert.AreEqual('e', content[6]);
			Assert.AreEqual(' ', content[7]);
			Assert.AreEqual('1', content[8]);
			Assert.AreEqual('\r', content[9]);
			Assert.AreEqual('\n', content[10]);
			Assert.AreEqual('2', content[11]);
			Assert.AreEqual(';', content[12]);
			Assert.AreEqual('Z', content[13]);
			Assert.AreEqual('e', content[14]);
			Assert.AreEqual('i', content[15]);
			Assert.AreEqual('l', content[16]);
			Assert.AreEqual('e', content[17]);
			Assert.AreEqual(' ', content[18]);
			Assert.AreEqual('2', content[19]);

			newFile.Delete();
		}
		#endregion

		#region TestToString
		[TestMethod]
		public void TestToString()
		{
			CsvFile file = new CsvFile();
			CsvFile.CsvRow row1 = new CsvFile.CsvRow();
			row1.Cells.Add(new CsvFile.CsvCell("cell.1.1"));
			row1.Cells.Add(new CsvFile.CsvCell("cell.1.2"));
			file.Rows.Add(row1);
			Assert.AreEqual("cell.1.1;cell.1.2", file.ToString());
		}
		#endregion

		#region TestBuildingACsvFromScratch
		[TestMethod]
		public void TestBuildingACsvFromScratch()
		{
			CsvFile file = new CsvFile();
			file.Rows.Add(new CsvFile.CsvRow("cell.1.1", "cell.2.1"));
			file.Rows.Add(new CsvFile.CsvRow("cell.1.2", "cell.2.2"));
			file.Rows.Add(new CsvFile.CsvRow("cell.1.3", "cell.2.3"));

			MemoryStream stream = new MemoryStream();
			file.Save(stream);
			stream.Position = 0;

			StreamReader reader = new StreamReader(stream);
			var streamContent = reader.ReadToEnd();

			Assert.AreEqual("cell.1.1;cell.2.1\r\ncell.1.2;cell.2.2\r\ncell.1.3;cell.2.3", streamContent);
		}
		#endregion
	}
}
