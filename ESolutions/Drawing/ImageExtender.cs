using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ESolutions.Drawing
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
			Size newSize = original.Size.ResizeLongestSide(longestSide);

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

		#region RotateByCameraAngle
		/// <summary>
		/// Rotates the image accorings to the camera angle.
		/// </summary>
		/// <param name="originalBitmap">The original bitmap.</param>
		private static void RotateByCameraAngle(this Image originalBitmap)
		{
			var propertyId = 0x112;

			if (originalBitmap != null &&
				originalBitmap.PropertyIdList != null &&
				originalBitmap.PropertyIdList.Contains(propertyId))
			{
				var rotationValue = originalBitmap.GetPropertyItem(propertyId).Value.FirstOrDefault();
				switch (rotationValue)
				{
					case 1: // landscape, do nothing
					{
						break;
					}
					case 3: // bottoms up
					{
						originalBitmap.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
						break;
					}
					case 6: // rotated 90 left
					{
						originalBitmap.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
						break;
					}
					case 8: // rotated 90 right
					{
						// de-rotate:
						originalBitmap.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
						break;
					}
				}
			}
		}
		#endregion
	}
}
