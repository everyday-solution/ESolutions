using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.DarkBird.Model.Exports
{
	public class ExcelPackageCreator
	{
		//Fields
		#region worksheet
		private ExcelWorksheet worksheet = null;
		#endregion

		#region rowIndex
		private Int32 rowIndex = 1;
		#endregion

		#region columnIndex
		private Int32 columnIndex = 1;
		#endregion

		#region columnCount
		private Int32 columnCount = 0;
		#endregion

		#region excelPackage
		private ExcelPackage excelPackage = new ExcelPackage();
		#endregion

		//Properties
		#region RowIndex
		public Int32 RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}
		#endregion

		//Constructors
		#region ExcelPackageCreator
		public ExcelPackageCreator(String worksheetName)
		{
			this.worksheet = this.excelPackage.Workbook.Worksheets.Add(worksheetName);
		}
		#endregion

		//Methods
		#region WriteCell
		public void WriteCell(object value, String format)
		{
			this.worksheet.Cells[this.rowIndex, this.columnCount].Style.Numberformat.Format = format;
			this.WriteCell(value);
		}
		#endregion

		#region WriteCell
		public void WriteCell(object value)
		{
			this.worksheet.Cells[this.rowIndex, this.columnIndex].Value = value;

			this.columnIndex++;
			if (this.columnIndex - 1 > this.columnCount)
			{
				this.columnCount = this.columnIndex - 1;
			}
		}
		#endregion

		#region WriteCells
		public void WriteCells(params Object[] values)
		{
			foreach (var runner in values)
			{
				this.WriteCell(runner);
			}
		}
		#endregion

		#region NextRow
		public void NextRow()
		{
			this.rowIndex++;
			this.columnIndex = 1;
		}
		#endregion

		#region PreparePackage
		public ExcelPackage PreparePackage()
		{
			this.ApplyAutoFilter();
			this.ApplyAutoWidth();

			return this.excelPackage;
		}
		#endregion

		#region ApplyAutoFilter
		private void ApplyAutoFilter()
		{
			this.worksheet.Cells[$"A1:{'A' + this.columnCount}"].AutoFilter = true;
		}
		#endregion

		#region ApplyAutoWidth
		private void ApplyAutoWidth()
		{
			for (var index = 1; index <= this.columnCount; index++)
			{
				this.worksheet.Column(index).AutoFit();
			}
		}
		#endregion
	}
}
