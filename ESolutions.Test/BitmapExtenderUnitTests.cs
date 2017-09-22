using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ESolutions.Drawing;

namespace ESolutions.Test
{
	[TestClass]
	public class BitmapExtenderUnitTests
	{
		#region TestResizeToWidth
		[TestMethod]
		public void TestResizeToWidth()
		{
			Bitmap testUnit = new Bitmap(200, 100);
			Bitmap actual = testUnit.ResizeToWidth(100);

			Assert.AreEqual(100, actual.Width);
			Assert.AreEqual(50, actual.Height);
		}
		#endregion

		#region TestResizeToHeight
		[TestMethod]
		public void TestResizeToHeight()
		{
			Bitmap testUnit = new Bitmap(200, 100);
			Bitmap actual = testUnit.ResizeToHeight(200);

			Assert.AreEqual(400, actual.Width);
			Assert.AreEqual(200, actual.Height);
		}
		#endregion

		#region TestDrawWatermark
		[TestMethod]
		public void TestDrawWatermark()
		{
			Bitmap testUnit = new Bitmap(200, 100);

			using (Graphics g = Graphics.FromImage(testUnit))
			{
				//g.FillRectangle(new SolidBrush(Color.Black), 
			}

			Bitmap watermark = new Bitmap(50, 50);
			using (Graphics g = Graphics.FromImage(watermark))
			{
			}

			testUnit.DrawWaterMark(watermark);
		}
		#endregion
	}
}
