﻿using Newtonsoft.Json;
using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class LevelChunkTests
	{
		[Fact]
		internal void AddOrUpdate_Tile_Works()
		{
			var tile = Helpers.GetTile(156, -300, "I'm a tile");
			var chunk = new LevelChunk<string>(
				new Rectangle(-500, -500, 1000, 1000),
				new Tile<string>[] {  });

			int countBefore = chunk.Tiles.Count();
			chunk.Tiles.AddOrUpdate(tile);
			int countAfter = chunk.Tiles.Count();
			var copy = chunk.Tiles
				.Where(t => t.Index == tile.Index)
				.SingleOrDefault();

			Assert.True(countAfter == countBefore + 1);
			Assert.True(copy.Index == tile.Index);
			Assert.True(copy.Data == tile.Data);
		}

		[Fact]
		internal void AddOrUpdate_Enumerable_Works()
		{
			var tiles = new Tile<string>[]
			{
				Helpers.GetTile(-10, 5, "Hello!"),
				Helpers.GetTile(-9, 4, "world"),
				Helpers.GetTile(-8, 6, "So cliche <.<")
			};
			var chunk = new LevelChunk<string>(
				new Rectangle(-12, -15, 50, 50),
				tiles);

			chunk.Tiles.AddOrUpdate(tiles);

			foreach (var tile in tiles)
			{
				Assert.True(chunk.Tiles.Select(t => t.Index).Contains(tile.Index));
				Assert.True(chunk.Tiles.Select(t => t.Data).Contains(tile.Data));
			}
			Assert.True(tiles.Count() == chunk.Tiles.Count());
		}

		[Fact]
		internal void Delete_Works()
		{
			var tile = Helpers.GetTile(55, 33, "Ima be deleted");
			var chunk = new LevelChunk<string>(
				new Rectangle(-10000, -10000, 20000, 20000),
				new Tile<string>[] { });

			chunk.Tiles.AddOrUpdate(tile);

			Assert.True(chunk.Tiles.Select(t => t.Index).Contains(tile.Index));

			chunk.Tiles.Delete(tile.Index);

			Assert.False(chunk.Tiles.Select(t => t.Index).Contains(tile.Index));
		}
		[Fact]
		internal void Delete_Enumerable_works()
		{
			var tiles = Helpers.GetTestTiles();
			var chunk = new LevelChunk<string>(
				new Rectangle(-50000, -50000, 100000, 100000),
				tiles);
			chunk.Tiles.Delete(tiles.Select(t => t.Index));

			Assert.True(chunk.Tiles.Count() == 0);
		}

		[Fact]
		internal void AddOrUpdate_ThrowsWhenOutOfRange()
		{
			var chunk = new LevelChunk<string>(
				new Rectangle(-5, -5, 10, 10), 
				new Tile<string>[] { });

			var index = new TileIndex(0, chunk.Region.Right + 1);
			var extremeRight = new Tile<string>(index, "Conservative");

			Assert.Throws<ArgumentOutOfRangeException>(
				() => chunk.Tiles.AddOrUpdate(extremeRight));
		}

		[Fact]
		internal void Delete_ThrowsWhenOutOfRange()
		{
			var chunk = new LevelChunk<string>(
				new Rectangle(-5, -5, 10, 10),
				new Tile<string>[] { });

			var tile = new Tile<string>(new TileIndex(), "Delete meh");
			var outOfRange = new TileIndex(chunk.Region.Right + 1, 0);

			chunk.Tiles.AddOrUpdate(tile);

			Assert.Throws<ArgumentOutOfRangeException>(
				() => chunk.Tiles.Delete(outOfRange));
		}

		[Fact]
		internal void Contains_ReportsPositive()
		{
			var chunk = new LevelChunk<string>(
				new Rectangle(-50000, -50000, 100000, 100000));
			var tile = Helpers.GetTile(55, 61, "Going on in");
			chunk.Tiles.AddOrUpdate(tile);

			Assert.True(chunk.Tiles.Contains(tile.Index));
		}

		[Fact]
		internal void Indexer_Works()
		{
			var chunk = new LevelChunk<string>(
				new Rectangle(-200, -200, 400, 400));
			var tile = Helpers.GetTile(101, -3, "Indexer test");

			chunk.Tiles.AddOrUpdate(tile);

			Assert.True(chunk.Tiles[tile.Index] == tile);
		}

		[Fact]
		internal void Indexer_ThrowsWhenIndexOutOfRange()
		{
			var chunk = new LevelChunk<string>(
				new Rectangle(-10, -10, 20, 20),
				new Tile<string>[] { });
			var tile = Helpers.GetTile(3, chunk.Region.Top - 1, "Over the top");

			Assert.Throws<IndexOutOfRangeException>(
				() => chunk.Tiles[tile.Index]);
		}
	}
}
