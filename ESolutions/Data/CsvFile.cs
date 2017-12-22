using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ESolutions.IO;
using System.Data;

namespace ESolutions.Data
{
	/// <summary>
	/// A Wrapper that can be used to access csv-files
	/// </summary>
	public class CsvFile
	{
		//Classes
		#region CsvRow
		/// <summary>
		/// A single row in a csvfile.
		/// </summary>
		public class CsvRow
		{
			//Properties
			#region Cells
			/// <summary>
			/// Gets the cells of a single row. The number of cells must 
			/// always equal the numer of columns in the contained csvfile.
			/// </summary>
			/// <value>The cells.</value>
			public List<CsvCell> Cells
			{
				get;
				private set;
			}
			#endregion

			#region this[]
			/// <summary>
			/// Gets the <see cref="ESolutions.Data.CsvFile.CsvCell"/> at the specified index.
			/// </summary>
			/// <value></value>
			public CsvCell this[Int32 index]
			{
				get
				{
					return this.Cells[index];
				}
			}
			#endregion

			//Constructors
			#region CsvRow
			/// <summary>
			/// Initializes a new instance of the <see cref="CsvRow"/> class.
			/// </summary>
			public CsvRow()
			{
				this.Cells = new List<CsvCell>();
			}
			#endregion

			#region CsvRow
			/// <summary>
			/// Initializes a new instance of the <see cref="CsvRow"/> class.
			/// </summary>
			/// <param name="cellValues">The cell values.</param>
			public CsvRow(params String[] cellValues) : this()
			{
				foreach (var runner in cellValues)
				{
					this.Cells.Add(new CsvCell(runner));
				}
			}
			#endregion

			//Methods
			#region ToString
			public override String ToString()
			{
				String result = String.Empty;

				foreach (CsvCell currentCell in this.Cells)
				{
					result += currentCell.ToString();
					result += ";";
				}

				result = result.TrimEnd(';');

				return result;
			}
			#endregion
		}
		#endregion

		#region CsvCell
		/// <summary>
		/// A single cell in a csv row
		/// </summary>
		public class CsvCell
		{
			//Constructors
			#region CsvCell
			/// <summary>
			/// Initializes a new instance of the <see cref="CsvCell"/> class.
			/// </summary>
			public CsvCell()
			{

			}
			#endregion

			#region CsvCell
			/// <summary>
			/// Initializes a new instance of the <see cref="CsvCell"/> class.
			/// </summary>
			/// <param name="value">The value.</param>
			public CsvCell(String value)
			{
				this.Value = value;
			}
			#endregion

			//Properties
			#region Value
			/// <summary>
			/// Gets or sets the value of the cell.
			/// </summary>
			/// <value>The value.</value>
			public String Value
			{
				get;
				set;
			}
			#endregion

			//Methods
			#region ToString
			public override String ToString()
			{
				return this.Value;
			}
			#endregion
		}
		#endregion

		//Properties
		#region Rows
		/// <summary>
		/// Gets the rows in the csv file
		/// </summary>
		/// <value>The rows.</value>
		public List<CsvRow> Rows
		{
			get;
			private set;
		}
		#endregion

		#region this[]
		/// <summary>
		/// Gets the <see cref="CsvRow"/> at the specified index.
		/// </summary>
		/// <value>
		/// The <see cref="CsvRow"/>.
		/// </value>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public CsvRow this[Int32 index]
		{
			get
			{
				return this.Rows[index];
			}
		}
		#endregion

		//Constructors
		#region CsvFile
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvFile"/> class.
		/// </summary>
		public CsvFile()
		{
			this.Rows = new List<CsvRow>();
		}
		#endregion

		#region CsvFile
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvFile"/> class and loads the content of the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="encoding">The encoding.</param>
		public CsvFile(FileInfo file, Encoding encoding)
			: this(file, encoding, ';', '\"')
		{
		}
		#endregion

		#region CsvFile
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvFile" /> class and loads the content of the specified file.
		/// </summary>
		/// <param name="file">The file to load initially.</param>
		/// <param name="encoding">The encoding used for reading</param>
		/// <param name="seperationSign">The seperation sign between cells</param>
		public CsvFile(FileInfo file, Encoding encoding, Char seperationSign)
			: this(file, encoding, seperationSign, '\"')
		{
		}
		#endregion

		#region CsvFile
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvFile" /> class and loads the content of the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="encoding">The encoding.</param>
		/// <param name="seperationSign">The sign by which fields in a row are seperated. E.g.: ;</param>
		/// <param name="valueSurroundings">The sign by which values of fields are surrounded. E.g.: "</param>
		/// <exception cref="System.ArgumentException">
		/// Parameter 'file' can not be null
		/// or
		/// Parameter 'encoding' can not be null
		/// or
		/// Parameter 'file' must point to an existing file.
		/// </exception>
		public CsvFile(FileInfo file, Encoding encoding, Char seperationSign, Char? valueSurroundings)
			: this()
		{
			if (file == null)
			{
				throw new ArgumentException("Parameter 'file' can not be null");
			}

			if (encoding == null)
			{
				throw new ArgumentException("Parameter 'encoding' can not be null");
			}

			if (file.Exists == false)
			{
				throw new ArgumentException("Parameter 'file' must point to an existing file.");
			}

			String fileContent = file.ReadToString(encoding);
			List<String> lines = fileContent.Split("\r\n");

			foreach (String currentLine in lines)
			{
				CsvRow newRow = new CsvRow();

				List<String> cells = null;
				if (valueSurroundings.HasValue)
				{
					cells = currentLine.Split(
					  seperationSign.ToString(),
					  valueSurroundings.Value.ToString());
				}
				else
				{
					cells = currentLine.Split(
						seperationSign.ToString());
				}

				foreach (String current in cells)
				{
					CsvCell newCell = new CsvCell();

					if (valueSurroundings.HasValue)
					{
						newCell.Value = current.Trim(valueSurroundings.Value);
					}
					else
					{
						newCell.Value = current;
					}

					newRow.Cells.Add(newCell);
				}

				this.Rows.Add(newRow);
			}
		}
		#endregion

		#region CsvFile
		/// <summary>
		/// Initializes a new instance of the <see cref="CsvFile" /> class initializing it from a data table.
		/// </summary>
		/// <param name="table">The table.</param>
		public CsvFile(DataTable table)
			: this()
		{
			foreach (DataRow currentRow in table.Rows)
			{
				CsvRow newRow = new CsvRow();

				foreach (object currentCell in currentRow.ItemArray)
				{
					CsvCell newCell = new CsvCell();
					newCell.Value = currentCell.ToString();
					newRow.Cells.Add(newCell);
				}

				this.Rows.Add(newRow);
			}
		}
		#endregion

		//Methods
		#region ToString
		public override String ToString()
		{
			String result = String.Empty;

			foreach (CsvRow currentRow in this.Rows)
			{
				result += currentRow.ToString();
				result += Environment.NewLine;
			}

			result = result.TrimEnd(Environment.NewLine.ToArray());

			return result;
		}
		#endregion

		#region ToDataTable
		public DataTable ToDataTable()
		{
			DataTable result = new DataTable();

			foreach (CsvRow currentRow in this.Rows)
			{
				while (currentRow.Cells.Count > result.Columns.Count)
				{
					result.Columns.Add();
				}
				DataRow newRow = result.NewRow();

				Int32 cellIndex = 0;
				foreach (CsvCell currentCell in currentRow.Cells)
				{
					newRow[cellIndex] = currentCell.Value;
					cellIndex++;
				}

				result.Rows.Add(newRow);
			}

			return result;
		}
		#endregion

		#region Save
		/// <summary>
		/// Saves the specified stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		public void Save(MemoryStream stream)
		{
			this.Save(stream, Encoding.UTF8, ";");
		}
		#endregion

		#region Save
		/// <summary>
		/// Saves the the current data to a csv file
		/// </summary>
		/// <param name="stream">The stream to save to.</param>
		/// <param name="encoding">The encoding to use.</param>
		/// <param name="p">The string that shall be used to separate cells.</param>
		public void Save(Stream stream, Encoding encoding, String seperator)
		{
			Int32 rowIndex = 0;
			foreach (CsvRow currentRow in this.Rows)
			{
				Int32 columnIndex = 0;
				foreach (CsvCell currentCell in currentRow.Cells)
				{
					String writeThis = currentCell.Value;
					if (columnIndex < currentRow.Cells.Count - 1)
					{
						writeThis += seperator;
					}
					Byte[] writeBuffer = encoding.GetBytes(writeThis);
					stream.Write(writeBuffer, 0, writeBuffer.Length);
					columnIndex++;
				}

				if (rowIndex < this.Rows.Count - 1)
				{
					Byte[] lineBreakBuffer = encoding.GetBytes(Environment.NewLine.ToArray());
					stream.Write(lineBreakBuffer, 0, lineBreakBuffer.Length);
				}
				rowIndex++;
			}
		}
		#endregion
	}
}
