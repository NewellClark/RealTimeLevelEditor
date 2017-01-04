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
	}
}
