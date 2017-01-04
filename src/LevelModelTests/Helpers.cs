using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelModelTests
{
	internal static class Helpers
	{
		public static IEnumerable<Tile<string>> GetTestTiles()
		{
			return new Tile<string>[]
			{
				GetTile(446, -332, "Hello world!"),
				GetTile(-323, -442, "Goodbye World!"),
				GetTile(4, 88, nameof(IEnumerable<string>)),
				GetTile(100, 99, "Cheese"),
				GetTile(-3, 7, "Boisonberry")
			};
		}

		public static Tile<string> GetTile(long x, long y, string data)
		{
			return new Tile<string>(new TileIndex(x, y), data);
		}

		/// <summary>
		/// Gets a test chunk that contains a single tile with the specified data value and 
		/// a non-default region.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <returns></returns>
		public static LevelChunk<T> GetChunk<T>(T data)
		{
			var region = new Rectangle(-45, 787, 511, 344);
			var tile = new Tile<T>(new TileIndex(8001, -2), data);
			return new LevelChunk<T>(region, new Tile<T>[] { tile });
		}

		public static LevelChunk<T> GetChunk<T>(IEnumerable<Tile<T>> tiles)
		{
			var region = new Rectangle(13327L, -4443332L, 10000L, 5600L);
			return new LevelChunk<T>(region, tiles);
		}

		public static LevelChunk<T> GetEmptyChunk<T>()
		{
			var region = new Rectangle(16663, -3324, 45, 32);
			return new LevelChunk<T>(region, new Tile<T>[] { });
		}
	}
}
