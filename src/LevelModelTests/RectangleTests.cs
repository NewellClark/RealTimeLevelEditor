using Newtonsoft.Json;
using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class RectangleTests
	{
		[Fact]
		internal void Ctor_InitializesPropertiesFromParams()
		{
			long left = 50;
			long top = -22;
			long width = 66;
			long height = 167;

			var rect = new Rectangle(left, top, width, height);

			Assert.True(rect.Left == left);
			Assert.True(rect.Top == top);
			Assert.True(rect.Width == width);
			Assert.True(rect.Height == height);
		}

		[Fact]
		internal void Ctor_ThrowsOnNegativeWidth()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var result = new Rectangle(908764, 8746545, -1, 09876324);
			});
		}

		[Fact]
		internal void Ctor_ThrowsOnNegativeHeight()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var result = new Rectangle(098674, 1987697, 876867868, -1);
			});
		}

		[Fact]
		internal void Right_EqualsLeftPlusWidth()
		{
			long left = -1065;
			long width = 944;
			var rect = new Rectangle(left, 27, width, 331);

			Assert.True(rect.Right == left + width);
		}

		[Fact]
		internal void Bottom_EqualsTopPlusHeight()
		{
			long top = 740;
			long height = 88110;
			var rect = new Rectangle(-12345, top, 432182, height);

			Assert.True(rect.Bottom == top + height);
		}

		[Fact]
		internal void Area_EqualsWidthTimesHeight()
		{
			long width = 09874;
			long height = 1346;
			var rect = new Rectangle(-13412, 324, width, height);

			Assert.Equal(rect.Area, width * height);
		}

		[Fact]
		internal void Area_EnclosedTilesCount_AreEqual()
		{
			var rect = new Rectangle(-24, -7993, 5032, 249);

			Assert.True(rect.EnclosedTiles.Count() == rect.Area);
		}

		[Fact]
		internal void Size_Works()
		{
			var rect = new Rectangle(25, -21, 57, 49);
			Assert.True(rect.Width == rect.Size.X);
			Assert.True(rect.Height == rect.Size.Y);
		}

		[Fact]
		internal void EqualsOperator_DetectsEquality()
		{
			long left = -13245;
			long top = -842;
			long width = 66109;
			long height = 993323;

			var rect1 = new Rectangle(left, top, width, height);
			var rect2 = new Rectangle(left, top, width, height);

			Assert.True(rect1 == rect2);
		}

		[Fact]
		internal void EqualsOperator_NullNull()
		{
			Rectangle? rect1 = null;
			Rectangle? rect2 = null;

			Assert.True(rect1 == rect2);
			Assert.False(rect1 != rect2);
		}

		[Fact]
		internal void EqualsOperator_NullNonNull()
		{
			Rectangle rect1 = new Rectangle(-77223, 9442242, 333333332, 777445);
			Rectangle? rect2 = null;

			Assert.True(rect1 != rect2);
			Assert.False(rect1 == rect2);
		}

		[Fact]
		internal void Equals_NonNullNull()
		{
			Rectangle rect1 = new Rectangle(432145, 018796, 23141, 089760132);
			Rectangle? rect2 = null;

			Assert.False(rect1.Equals(rect2));
		}

		[Fact]
		internal void Contains_LeftBoundCheck()
		{
			var rect = GetContainmentTestObject();
			var inside = new TileIndex(rect.Left + 1, 0);
			var onLine = new TileIndex(rect.Left, 0);
			var outside = new TileIndex(rect.Left - 1, 0);

			Assert.True(rect.Contains(inside));
			Assert.True(rect.Contains(onLine));
			Assert.False(rect.Contains(outside));
		}

		/// <summary>
		/// A tile with an x-coordinate that is equal to Rectangle.Right will
		/// be just outside the rectangle.
		/// </summary>
		[Fact]
		internal void Contains_RightBoundCheck()
		{
			var rect = GetContainmentTestObject();
			var inside = new TileIndex(rect.Right - 1, 0);
			//var onLine = new TileIndex(rect.Right, 0);
			var outside = new TileIndex(rect.Right, 0);

			Assert.True(rect.Contains(inside));
			//Assert.True(rect.Contains(onLine));
			Assert.False(rect.Contains(outside));
		}

		[Fact]
		internal void Contains_TopBoundCheck()
		{
			var rect = GetContainmentTestObject();
			var inside = new TileIndex(0, rect.Top + 1);
			var onLine = new TileIndex(0, rect.Top);
			var outside = new TileIndex(0, rect.Top - 1);

			Assert.True(rect.Contains(inside));
			Assert.True(rect.Contains(onLine));
			Assert.False(rect.Contains(outside));
		}

		/// <summary>
		/// A tile with a y-coordinate that is equal to Rectangle.Bottom will
		/// be just outside the rectangle.
		/// </summary>
		[Fact]
		internal void Contains_BottomBoundCheck()
		{
			var rect = GetContainmentTestObject();
			var inside = new TileIndex(0, rect.Bottom - 1);
			//var onLine = new TileIndex(0, rect.Bottom);
			var outside = new TileIndex(0, rect.Bottom);

			Assert.True(rect.Contains(inside));
			//Assert.True(rect.Contains(onLine));
			Assert.False(rect.Contains(outside));
		}


		private Rectangle GetContainmentTestObject()
		{
			return new Rectangle(-50, -30, 100, 60);
		}
	}
}
