using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ESolutions.Drawing;

namespace ESolutions.Test
{
	[TestClass]
	public class SizeUnitTest
	{
		#region TestGetRatioSquare
		[TestMethod]
		public void TestGetRatioSquare()
		{
			Size testUnit = new Size(100, 100);
			Double actual = testUnit.GetRatio();

			Assert.AreEqual(1.0, actual);
		}
		#endregion

		#region TestGetRatioPortrait
		[TestMethod]
		public void TestGetRatioPortrait()
		{
			Size testUnit = new Size(100, 200);
			Double actual = testUnit.GetRatio();

			Assert.AreEqual(0.5, actual);
		}
		#endregion

		#region TestGetRatioLandscape
		[TestMethod]
		public void TestGetRatioLandscape()
		{
			Size testUnit = new Size(200, 100);
			Double actual = testUnit.GetRatio();

			Assert.AreEqual(2.0, actual);
		}
		#endregion

		#region TestIsLandscape
		[TestMethod]
		public void TestIsLandscapeOk()
		{
			Size testUnit = new Size(200, 100);
			Assert.IsTrue(testUnit.IsLandscape());
		}
		#endregion

		#region TestIsLandscapeFalse
		[TestMethod]
		public void TestIsLandscapeFalse()
		{
			Size testUnit = new Size(100, 100);
			Assert.IsFalse(testUnit.IsLandscape());
		}
		#endregion

		#region TestIsPortraitOk
		[TestMethod]
		public void TestIsPortraitOk()
		{
			Size testUnit = new Size(100, 200);
			Assert.IsTrue(testUnit.IsPortrait());
		}
		#endregion

		#region TestIsPortraitFalse
		[TestMethod]
		public void TestIsPortraitFalse()
		{
			Size testUnit = new Size(100, 100);
			Assert.IsFalse(testUnit.IsPortrait());
		}
		#endregion

		#region TestIsQuadraticOk
		[TestMethod]
		public void TestIsQuadraticOk()
		{
			Size testUnit = new Size(100, 100);
			Assert.IsTrue(testUnit.IsQuadratic());
		}
		#endregion

		#region TestIsQuadraticFalse
		[TestMethod]
		public void TestIsQuadraticFalse()
		{
			Size testUnit1 = new Size(100, 101);
			Size testUnit2 = new Size(101, 100);

			Assert.IsFalse(testUnit1.IsQuadratic());
			Assert.IsFalse(testUnit2.IsQuadratic());
		}
		#endregion

		#region TestResizeWidth
		[TestMethod]
		public void TestResizeWidth()
		{
			Size testUnit = new Size(200, 100);
			Size actual = testUnit.ResizeWidth(100);

			Assert.AreEqual(100, actual.Width);
			Assert.AreEqual(50, actual.Height);
		}
		#endregion

		#region TestResizeHeight
		[TestMethod]
		public void TestResizeHeight()
		{
			Size testUnit = new Size(200, 100);
			Size actual = testUnit.ResizeHeight(200);

			Assert.AreEqual(400, actual.Width);
			Assert.AreEqual(200, actual.Height);
		}
		#endregion

		#region TestToRectangle
		[TestMethod]
		public void TestToRectangle()
		{
			Size size = new Size(80, 90);
			Rectangle actual = size.ToRectangle();

			Assert.AreEqual(0, actual.X);
			Assert.AreEqual(0, actual.Y);
			Assert.AreEqual(80, actual.Width);
			Assert.AreEqual(90, actual.Height);
		}
		#endregion
	}
}
