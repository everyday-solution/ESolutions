using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using ESolutions.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESolutions.Test
{
	[TestClass]
	public class ImageExtenderTests
	{
		[TestMethod]
		public void TestThatLandscapeFormatedImagesAreResized()
		{
			Bitmap original = new Bitmap(2000, 1000);
			Bitmap actual = original.Resize(500);

			Assert.AreEqual(500, actual.Width);
			Assert.AreEqual(250, actual.Height);
		}

		[TestMethod]
		public void TestThatPortraitFormatedImagesAreResized()
		{
			Bitmap original = new Bitmap(1000, 2000);
			Bitmap actual = original.Resize(500);

			Assert.AreEqual(250, actual.Width);
			Assert.AreEqual(500, actual.Height);
		}

		[TestMethod]
		public void TestThatQubicFormatedImagesAreResized()
		{
			Bitmap original = new Bitmap(1000, 1000);
			Bitmap actual = original.Resize(500);

			Assert.AreEqual(500, actual.Width);
			Assert.AreEqual(500, actual.Height);
		}
	}
}
