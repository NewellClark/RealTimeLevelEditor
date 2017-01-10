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

		[Fact]
		internal void AddOrUpdate_UpdatesExisting()
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

		[Fact]
		internal void Delete_RemovesExisting()
		{
			var level = CreateDefault<string>();
			var tiles = GetSampleIndeces()
				.Select(x => new Tile<string>(x, "Words"));
			level.AddOrUpdate(tiles);

			level.Delete(tiles.Select(x => x.Index));

			Assert.True(level.Count == 0);
		}

		[Fact]
		internal void Count_Works()
		{
			var level = CreateDefault<string>();
			var tiles = GetSampleIndeces()
				.Select(x => new Tile<string>(x, "Tile Text"));

			level.AddOrUpdate(tiles);
			Assert.Equal(tiles.Count(), level.Count);
		}

		[Fact]
		internal void GetEnumerator_Works()
		{
			var level = CreateDefault<string>();
			var tiles = GetSampleIndeces()
				.Select(x => new Tile<string>(x, "Tongue twister"));
			level.AddOrUpdate(tiles);

			var list = level.ToList();
			Assert.True(list.Count == tiles.Count());
		}

		/// <summary>
		/// Creates a default level that is appropriate for testing purposes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected virtual Level<T> CreateDefault<T>()
		{
			return new Level<T>(
				new MockRepository<T>(), 
				_chunkSize);
		}

		protected IEnumerable<TileIndex> GetSampleIndeces()
		{
			return Helpers.Sample(_sampleRegion, _sampleResolution);
		}

		private const long _sampleResolution = 9;
		private readonly Rectangle _sampleRegion =
			new Rectangle(-200, -160, 400, 320);
		protected readonly Size _chunkSize =
			new Size(21, 34);
	}
}
