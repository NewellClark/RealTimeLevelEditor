using Newtonsoft.Json;
using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class JsonTests
	{
		[Fact]
		internal void Rectangle_Serialize()
		{
			var rect = new Rectangle(0987987, -3241324, 522338887, 234);
			string json = JsonConvert.SerializeObject(rect);
			var copy = JsonConvert.DeserializeObject<Rectangle>(json);

			Assert.True(rect == copy);
		}

		[Fact]
		internal void TileIndex_Serialize()
		{
			var tile1 = new TileIndex(-098742, -397087071);
			string json = JsonConvert.SerializeObject(tile1);
			var tile2 = JsonConvert.DeserializeObject<TileIndex>(json);
			Assert.True(tile1 == tile2);
		}

		[Fact]
		internal void Tile_Serialize()
		{
			var tile = new Tile<string>(new TileIndex(135, -332335), "Hello world!");
			string json = JsonConvert.SerializeObject(tile);
			var copy = JsonConvert.DeserializeObject<Tile<string>>(json);

			bool result = tile.Index == copy.Index &&
				tile.Data == copy.Data;

			Assert.True(result);
		}

		[Fact]
		internal void LevelChunk_Serialize()
		{
			var chunk = new LevelChunk<string>(
				new Rectangle(45, -900, 256, 128), 
				Helpers.GetTestTiles());
			var json = JsonConvert.SerializeObject(chunk);
			var copy = JsonConvert.DeserializeObject<LevelChunk<string>>(json);

			Assert.True(ChunksEqualByValue(chunk, copy));
		}


		private bool TilesEqual<T>(Tile<T> lhs, Tile<T> rhs)
		{
			return lhs.Index == rhs.Index &&
				lhs.Data.Equals(rhs.Data);
		}

		private bool ChunksEqualByValue(LevelChunk<string> lhs, LevelChunk<string> rhs)
		{
			return lhs.Region == rhs.Region &&
				Helpers.SeriesHaveSameElementsAndSizes(
					lhs.Tiles, rhs.Tiles, (l, r) => l == r);
		}

	}
}
