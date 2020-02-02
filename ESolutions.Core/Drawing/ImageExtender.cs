using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESolutions.Core.Drawing
{
	public static class ImageExtender
	{
		#region Resize
		/// <summary>
		/// Resizes the Bitmap according to the longest side.
		/// </summary>
		/// <param name="original">The original.</param>
		/// <param name="longestSide">The longest side.</param>
		/// <returns></returns>
		public static Bitmap Resize(this Image original, Int32 longestSide)
		{
			var newSize = original.Size.ResizeLongestSide(longestSide);

			return new Bitmap(original, newSize);
		}
		#endregion

		#region Crop
		/// <summary>
		/// Crops the bitmap to the specied size
		/// </summary>
		/// <param name="original">The original.</param>
		/// <param name="newSize">The new size.</param>
		/// <returns></returns>
		public static Bitmap Crop(this Image original, Size newSize)
		{
			Bitmap result = new Bitmap(newSize.Width, newSize.Height);
			Graphics canvas = Graphics.FromImage(result);
			canvas.DrawImage(original, new Point(0, 0));
			canvas.Dispose();

			return result;
		}
		#endregion
	}
}
