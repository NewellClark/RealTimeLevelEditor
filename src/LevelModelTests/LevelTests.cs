using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public partial class LevelTests
	{
		[Fact]
		internal void AddOrUpdate_AddsNewTiles()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>();
				var tiles = (from index in GetSampleIndeces()
							 select new Tile<string>(index, index.ToString())).ToList();

				level.AddOrUpdate(tiles);

				Assert.Equal(tiles.Count, level.Count);
				foreach (var tile in tiles)
				{
					Assert.True(level.Contains(tile.Index),
						"Level did not contain tile we just added");
					var partner = level[tile.Index];
					Assert.Equal(tile.Index, partner.Index);
					Assert.Equal(tile.Data, partner.Data);
				}
			}
		}

		[Fact]
		internal void AddOrUpdate_UpdatesExisting()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				string before = "Before";
				string after = "After";
				var level = CreateDefault<string>();
				var tiles = GetSampleIndeces()
					.Select(x => new Tile<string>(x, before));
				level.AddOrUpdate(tiles);

				var modified = tiles
					.Select(x => new Tile<string>(x.Index, after));

				level.AddOrUpdate(modified);

				foreach (var tile in level)
					Assert.True(tile.Data == after); 
			}
		}

		[Fact]
		internal void Delete_RemovesExisting()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>();
				var tiles = GetSampleIndeces()
					.Select(x => new Tile<string>(x, "Words"));
				level.AddOrUpdate(tiles);

				level.Delete(tiles.Select(x => x.Index));

				Assert.True(level.Count == 0);
			}
		}

		[Fact]
		internal void Count_Works()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>();
				var tiles = GetSampleIndeces()
					.Select(x => new Tile<string>(x, "Tile Text"));

				level.AddOrUpdate(tiles);
				Assert.Equal(tiles.Count(), level.Count); 
			}
		}

		[Fact]
		internal void GetEnumerator_Works()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>();
				var tiles = GetSampleIndeces()
					.Select(x => new Tile<string>(x, "Tongue twister"));
				level.AddOrUpdate(tiles);

				var list = level.ToList();
				Assert.True(list.Count == tiles.Count()); 
			}
		}

		[Fact]
		internal void GetChunksInRegion_LoadsAllChunks()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				long chunkWidth = 100;
				long regionWidthInChunks = 10;
				long regionWidthInTiles = regionWidthInChunks * chunkWidth;
				var level = CreateDefault<string>(chunkWidth, chunkWidth);
				var testRegion = new Rectangle(
						regionWidthInTiles / 2,
						regionWidthInTiles / 2,
						regionWidthInTiles,
						regionWidthInTiles);
				var expected = Helpers.Sample(testRegion, chunkWidth / 2)
					.Select(x => new Tile<string>(x));
				level.AddOrUpdate(expected);

				var chunksInRegion = level.GetChunksInRegion(testRegion);

				Assert.Equal(chunksInRegion.Count(), regionWidthInChunks * regionWidthInChunks); 
			}
		}

		[Fact]
		internal void GetTilesInRegion_ReturnsAllTilesInRegion()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>(10, 10);
				var indeces = new TileIndex[]
				{
					new TileIndex(-8, -7),
					new TileIndex(-8, 7),
					new TileIndex(8, 7),
					new TileIndex(8, -7)
				};
				var expected = indeces
					.Select(x => new Tile<string>(x));

				level.AddOrUpdate(expected);

				var actual = level.GetTilesInRegion(
					new Rectangle(-10, -10, 20, 20));

				Assert.Equal(expected.Count(), actual.Count());
			}
		}

		[Fact]
		internal void DeleteRegion_DeletesAllTilesInRegion()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>(10, 10);
				var indeces = new TileIndex[]
				{
					new TileIndex(-5, -5),
					new TileIndex(-5, 5),
					new TileIndex(5, 5),
					new TileIndex(5, -5)
				};

				var toBeDeleted = indeces
					.Select(x => new Tile<string>(x));
				level.AddOrUpdate(toBeDeleted);

				level.Delete(new Rectangle(-10, -10, 20, 20));

				Assert.Empty(level);
			}
		}

		[Fact]
		internal void DeleteRegion_DoesNotDeleteTilesOutsideRegion()
		{
			using (var cleanup = new CleanupProvider(Cleanup))
			{
				var level = CreateDefault<string>(10, 10);
				var toDelete = new TileIndex[]
				{
					new TileIndex(-5, -5),
					new TileIndex(-5, 5),
					new TileIndex(5, 5),
					new TileIndex(5, -5)
				}
				.Select(x => new Tile<string>(x, Guid.NewGuid().ToString()));
				var toNotDelete = new TileIndex[]
				{
					new TileIndex(-7, -7),
					new TileIndex(-7, 7),
					new TileIndex(7, 7),
					new TileIndex(7, -7)
				}
				.Select(x => new Tile<string>(x, Guid.NewGuid().ToString()));

				level.AddOrUpdate(toDelete);
				level.AddOrUpdate(toNotDelete);

				level.Delete(new Rectangle(-6, -6, 12, 12));

				Assert.Equal(toNotDelete.Count(), level.Count);
			}
		}


		/// <summary>
		/// Will get executed after every test. Use it to delete persisted test data.
		/// </summary>
		protected virtual void Cleanup()
		{

		}
		/// <summary>
		/// Creates a default level that is appropriate for testing purposes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected virtual Level<T> CreateDefault<T>(Size chunkSize)
		{
			return new Level<T>(
				new MockRepository<T>(), 
				chunkSize);
		}

		protected Level<T> CreateDefault<T>()
		{
			return CreateDefault<T>(_chunkSize);
		}

		protected Level<T> CreateDefault<T>(long x, long y)
		{
			return CreateDefault<T>(new Size(x, y));
		}

		protected IEnumerable<TileIndex> GetSampleIndeces()
		{
			return Helpers.Sample(_sampleRegion, _sampleResolution);
		}


		private class CleanupProvider : IDisposable
		{
			public CleanupProvider(Action cleanupMethod)
			{
				_cleanupMethod = cleanupMethod;
			}

			public void Dispose()
			{
				_cleanupMethod();
			}

			private Action _cleanupMethod;
		}

		private const long _sampleResolution = 9;
		private readonly Rectangle _sampleRegion =
			new Rectangle(-200, -160, 400, 320);
		private readonly Size _chunkSize =
			new Size(21, 34);
	}
}
