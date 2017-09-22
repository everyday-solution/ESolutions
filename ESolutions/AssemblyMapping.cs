using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ESolutions
{
	/// <summary>
	/// Represents a mapping between a Type and its containing assembly
	/// </summary>
	public class AssemblyMapping
	{
		//Fields
		#region Fullname
		/// <summary>
		/// The fullname
		/// </summary>
		public String Fullname;
		#endregion

		#region PathToFile
		/// <summary>
		/// The path to file
		/// </summary>
		public String PathToFile;
		#endregion

		#region Filename
		/// <summary>
		/// The filename
		/// </summary>
		public String Filename;
		#endregion

		//Properties
		#region File
		/// <summary>
		/// Gets the file.
		/// </summary>
		/// <value>
		/// The file.
		/// </value>
		public FileInfo File
		{
			get
			{
				String fullPath = Path.Combine(this.PathToFile, this.Filename);
				return new FileInfo(fullPath);
			}
		}
		#endregion
	}
}
