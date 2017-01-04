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
		internal void Json_EqualityPreservedAfterJsonSerialization()
		{
			var tile1 = new TileIndex(-098742, -397087071);
			string json = JsonConvert.SerializeObject(tile1);
			var tile2 = JsonConvert.DeserializeObject<TileIndex>(json);
			Assert.True(tile1 == tile2);
		}

		//[Fact]
		//internal void Ctor_ThrowsOnWrongCollectionSize()
		//{
		//	var coordinates = new long[] { 774, 442, -113334 };
		//	Assert.Throws<ArgumentException>(() =>
		//	{
		//		var result = new TileIndex(coordinates);
		//	});
		//}

		//[Fact]
		//internal void Ctor_CorrectCoordinateOrderXY()
		//{
		//	long x = 52;
		//	long y = 100;
		//	var tile1 = new TileIndex(x, y);
		//	var tile2 = new TileIndex(new long[] { x, y });
		//	Assert.True(tile1 == tile2);
		//}

		//[Fact]
		//internal void GetEnumerator_CorrectCoordinateOrderXY()
		//{
		//	long x = 221;
		//	long y = 4593;
		//	var tile = new TileIndex(x, y);
		//	var array = tile.ToArray();
		//	Assert.True(array[0] == x);
		//	Assert.True(array[1] == y);
		//}
	}
}
