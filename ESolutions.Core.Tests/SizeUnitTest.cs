using System;
using System.Drawing;
using ESolutions.Core.Drawing;
using Xunit;

namespace ESolutions.Core.Test
{
	public class SizeUnitTest
	{
		#region TestGetRatioSquare
		[Fact]
		public void TestGetRatioSquare()
		{
			Size testUnit = new Size(100, 100);
			Double actual = testUnit.GetRatio();

			Assert.Equal(1.0, actual);
		}
		#endregion

		#region TestGetRatioPortrait
		[Fact]
		public void TestGetRatioPortrait()
		{
			Size testUnit = new Size(100, 200);
			Double actual = testUnit.GetRatio();

			Assert.Equal(0.5, actual);
		}
		#endregion

		#region TestGetRatioLandscape
		[Fact]
		public void TestGetRatioLandscape()
		{
			Size testUnit = new Size(200, 100);
			Double actual = testUnit.GetRatio();

			Assert.Equal(2.0, actual);
		}
		#endregion

		#region TestIsLandscape
		[Fact]
		public void TestIsLandscapeOk()
		{
			Size testUnit = new Size(200, 100);
			Assert.True(testUnit.IsLandscape());
		}
		#endregion

		#region TestIsLandscapeFalse
		[Fact]
		public void TestIsLandscapeFalse()
		{
			Size testUnit = new Size(100, 100);
			Assert.False(testUnit.IsLandscape());
		}
		#endregion

		#region TestIsPortraitOk
		[Fact]
		public void TestIsPortraitOk()
		{
			Size testUnit = new Size(100, 200);
			Assert.True(testUnit.IsPortrait());
		}
		#endregion

		#region TestIsPortraitFalse
		[Fact]
		public void TestIsPortraitFalse()
		{
			Size testUnit = new Size(100, 100);
			Assert.False(testUnit.IsPortrait());
		}
		#endregion

		#region TestIsQuadraticOk
		[Fact]
		public void TestIsQuadraticOk()
		{
			Size testUnit = new Size(100, 100);
			Assert.True(testUnit.IsQuadratic());
		}
		#endregion

		#region TestIsQuadraticFalse
		[Fact]
		public void TestIsQuadraticFalse()
		{
			Size testUnit1 = new Size(100, 101);
			Size testUnit2 = new Size(101, 100);

			Assert.False(testUnit1.IsQuadratic());
			Assert.False(testUnit2.IsQuadratic());
		}
		#endregion

		#region TestResizeWidth
		[Fact]
		public void TestResizeWidth()
		{
			Size testUnit = new Size(200, 100);
			Size actual = testUnit.ResizeWidth(100);

			Assert.Equal(100, actual.Width);
			Assert.Equal(50, actual.Height);
		}
		#endregion

		#region TestResizeHeight
		[Fact]
		public void TestResizeHeight()
		{
			Size testUnit = new Size(200, 100);
			Size actual = testUnit.ResizeHeight(200);

			Assert.Equal(400, actual.Width);
			Assert.Equal(200, actual.Height);
		}
		#endregion

		#region TestToRectangle
		[Fact]
		public void TestToRectangle()
		{
			Size size = new Size(80, 90);
			Rectangle actual = size.ToRectangle();

			Assert.Equal(0, actual.X);
			Assert.Equal(0, actual.Y);
			Assert.Equal(80, actual.Width);
			Assert.Equal(90, actual.Height);
		}
		#endregion
	}
}
