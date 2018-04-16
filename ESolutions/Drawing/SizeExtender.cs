using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ESolutions.Drawing
{
	/// <summary>
	/// Extender for System.Size
	/// </summary>
	public static class SizeExtender
	{
		#region IsLandscape
		/// <summary>
		/// Determines whether the specified size is landscape.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>
		/// 	<c>true</c> if the specified size is landscape; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsLandscape(this Size size)
		{
			return size.Width > size.Height;
		}
		#endregion

		#region IsPortrait
		/// <summary>
		/// Determines whether the specified size is portrait.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>
		/// 	<c>true</c> if the specified size is portrait; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsPortrait(this Size size)
		{
			return size.Height > size.Width;
		}
		#endregion

		#region IsQuadratic
		/// <summary>
		/// Determines whether the specified size is quadratic.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns>
		/// 	<c>true</c> if the specified size is quadratic; otherwise, <c>false</c>.
		/// </returns>
		public static Boolean IsQuadratic(this Size size)
		{
			return size.Height == size.Width;
		}
		#endregion

		#region ResizeLongestSide
		/// <summary>
		/// Gets the size of an original size object if its longest side would be resized to the given value maintaining the ratio.
		/// </summary>
		/// <param name="original">The original size.</param>
		/// <param name="longestSide">The longest side of the new size.</param>
		/// <returns></returns>
		public static Size ResizeLongestSide(this Size original, Int32 longestSide)
		{
			Double ratio = Convert.ToDouble(original.Width) / Convert.ToDouble(original.Height);

			Int32 width = 0;
			Int32 height = 0;

			if (ratio > 1)
			{
				width = longestSide;
				height = Convert.ToInt32(longestSide / ratio);
			}
			else
			{
				width = Convert.ToInt32(longestSide * ratio);
				height = longestSide;
			}

			Size newSize = new Size(width, height);
			return newSize;
		}
		#endregion

		#region ResizeWidth
		/// <summary>
		/// Resizes the specified bitmap to the specified width and the corresponding 
		/// the height regarding the image ration.
		/// </summary>
		/// <param name="originalSize">The bitmap.</param>
		/// <param name="width">The width.</param>
		/// <returns></returns>
		public static Size ResizeWidth(this Size originalSize, Int32 width)
		{
			return new Size
			{
				Height = (Int32)((Double)width / originalSize.GetRatio()),
				Width = width
			};
		}
		#endregion

		#region ResizeHeight
		/// <summary>
		/// Resizes the specified bitmap to the specified height and the corresponding 
		/// the width regarding the image ration.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public static Size ResizeHeight(this Size originalSize, Int32 height)
		{
			return new Size
			{
				Height = height,
				Width = (Int32)((Double)height * originalSize.GetRatio())
			};
		}
		#endregion

		#region GetRatio
		/// <summary>
		/// Gets the ratio of the bitmap width/height;
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <returns></returns>
		public static Double GetRatio(this Size bitmap)
		{
			return (Double)bitmap.Width / (Double)bitmap.Height;
		}
		#endregion

		#region ToRectangle
		/// <summary>
		/// To the rectangle.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		public static Rectangle ToRectangle(this Size size)
		{
			return new Rectangle(new Point(0, 0), size);
		}
		#endregion
	}
}
