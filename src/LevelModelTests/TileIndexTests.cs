using Newtonsoft.Json;
using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class TileIndexTests
	{
		[Fact]
		internal void EqualsOperator_ReturnsTrueWhenEqual()
		{
			long x = 12345;
			long y = 98764234;
			var tile1 = new TileIndex(x, y);
			var tile2 = new TileIndex(x, y);

			Assert.True(tile1 == tile2);
			Assert.False(tile1 != tile2);
		}

		[Fact]
		internal void EqualsOperator_ReturnsFalseWhenNotEqual()
		{
			long x = -32512;
			long y = 987987;
			var tile1 = new TileIndex(x, y);
			var tile2 = new TileIndex(x + 1, y);
			var tile3 = new TileIndex(x, y + 1);

			Assert.False(tile1 == tile2);
			Assert.False(tile1 == tile3);
		}

		[Fact]
		internal void TryParse_ParsesOutputFromToString()
		{
			foreach (var index in GetSquareRange(-100000, 100000, 997))
			{
				string text = index.ToString();
				var parsed = TileIndex.TryParse(text);
				Assert.True(index == parsed);
			}
		}

		[Fact]
		internal void ToChunkIndex_PositivePositive()
		{
			var tile = new TileIndex(10, 10);
			var size = new Size(20, 20);
			var chunk = tile.ToChunkIndex(size);

			Assert.True(chunk == new TileIndex(0, 0));
		}

		[Fact]
		internal void ToChunkIndex_Positive_OnLeftEdge()
		{
			var tile = new TileIndex(0, 5);
			var size = new Size(10, 10);
			var chunk = tile.ToChunkIndex(size);

			var expected = new TileIndex(0, 0);

			Assert.True(chunk == expected);
		}

		[Fact]
		internal void ToChunkIndex_Positive_OnRightEdge()
		{
			var tile = new TileIndex(10, 0);
			var size = new Size(10, 10);
			var chunk = tile.ToChunkIndex(size);
			var expected = new TileIndex(1, 0);

			Assert.True(chunk == expected);
		}

		[Fact]
		internal void ToChunkIndex_Negative_Middle()
		{
			var tile = new TileIndex(-10, -10);
			var size = new Size(20, 20);
			var chunk = tile.ToChunkIndex(size);
			var expected = new TileIndex(-1, -1);

			Assert.True(chunk == expected);
		}

		[Fact]
		internal void ToChunkIndex_Negative_LeftEdge()
		{
			var tile = new TileIndex(-20, -10);
			var size = new Size(20, 20);
			var chunk = tile.ToChunkIndex(size);

			Assert.Equal(new TileIndex(-1, -1), chunk);
		}

		[Fact]
		internal void ToChunkIndex_Negative_RightEdge()
		{
			var tile = new TileIndex(0, -10);
			var size = new Size(20, 20);
			var chunk = tile.ToChunkIndex(size);

			Assert.True(chunk == new TileIndex(0, -1));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Min is greator than max.</exception>
		private IEnumerable<TileIndex> GetSquareRange(long min, long max, long increment = 1)
		{
			if (min > max)
			{
				throw new ArgumentException(
					$@"{nameof(min)} can't be greator than {nameof(max)}.");
			}
			for (long x = min; x <= max; x += increment)
			{
				for (long y = min; y <= max; y += increment)
				{
					yield return new TileIndex(x, y);
				}
			}
		}
	}
}
