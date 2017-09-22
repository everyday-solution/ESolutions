using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ESolutions.IO
{
	/// <summary>
	/// Extender calss for FileInfo.
	/// </summary>
	public static class FileInfoExtender
	{
		#region ReadToString
		/// <summary>
		/// Reads the content of the file into a string assuming the specified encoding.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="encoding">The encoding.</param>
		/// <returns></returns>
		public static String ReadToString(this FileInfo file, Encoding encoding)
		{
			return File.ReadAllText(file.FullName, encoding);
		}
		#endregion
	}
}
