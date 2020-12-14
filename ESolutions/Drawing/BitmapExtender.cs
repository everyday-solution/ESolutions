using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ESolutions.Drawing
{
	/// <summary>
	/// Extender class for System.Extender
	/// </summary>
	public static class BitmapExtender
	{
		#region ResizeToWidth
		/// <summary>
		/// Resizes the specified bitmap to the specified width and the corresponding 
		/// the height regarding the image ration.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="width">The width.</param>
		/// <returns></returns>
		public static Bitmap ResizeToWidth(this Bitmap bitmap, Int32 width)
		{
			Size newSize = bitmap.Size.ResizeWidth(width);
			return bitmap.ResizeToSize(newSize);
		}
		#endregion

		#region ResizeToHeight
		/// <summary>
		/// Resizes the specified bitmap to the specified height and the corresponding 
		/// the width regarding the image ration.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="height">The height.</param>
		/// <returns></returns>
		public static Bitmap ResizeToHeight(this Bitmap bitmap, Int32 height)
		{
			Size newSize = bitmap.Size.ResizeHeight(height);
			return bitmap.ResizeToSize(newSize);
		}
		#endregion

		#region ResizeToSize
		/// <summary>
		/// Resizes bitmap to the specified size
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		private static Bitmap ResizeToSize(this Bitmap bitmap, Size size)
		{
			Bitmap result = new Bitmap(size.Width, size.Height);

			using (Graphics g = Graphics.FromImage(result))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBilinear;
				g.DrawImage(bitmap, 0, 0, size.Width, size.Height);
			}

			return result;
		}
		#endregion

		#region DrawWaterMark
		/// <summary>
		/// Draws specified watermark image transparent on the right bottom corner of the bitmap.
		/// </summary>
		/// <param name="originalBitmap">The original bitmap.</param>
		/// <param name="watermark">The watermark.</param>
		/// <remarks>
		/// The watermark width is 30% of the originalBitmap, hence the magic Number 0.3.
		/// </remarks>
		public static void DrawWaterMark(this Bitmap originalBitmap, Bitmap watermark)
		{
			//Transparent Matrix
			List<Single[]> pts = new List<Single[]>
			{
				new Single[] { 1, 0, 0, 0, 0 },
				new Single[] { 0, 1, 0, 0, 0 },
				new Single[] { 0, 0, 1, 0, 0 },
				new Single[] { 0, 0, 0, 0.5F, 0 },
				new Single[] { 0, 0, 0, 0, 1 }
			};
			ColorMatrix matrix = new ColorMatrix(pts.ToArray());
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(matrix);

			//watermark is 30% of the width of the original image
			Double watermarkRatio = (Double)watermark.Width / (Double)watermark.Height;
			Int32 width = (Int32)((Double)originalBitmap.Width * 0.3);
			Int32 height = (Int32)((Double)width / (Double)watermarkRatio);
			Rectangle destination = new Rectangle(originalBitmap.Width - width, originalBitmap.Height - height, width, height);

			//Draw watermark on image
			using (Graphics g = Graphics.FromImage(originalBitmap))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBilinear;
				g.DrawImage(
					watermark,
					destination,
					0,
					0,
					watermark.Width,
					watermark.Height,
					GraphicsUnit.Pixel,
					imageAttributes);
			}
		}
		#endregion
	}
}
