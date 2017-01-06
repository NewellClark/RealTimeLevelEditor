using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class VariableSizeTileCollectionTests
	{
		[Fact]
		internal void AddOrUpdate_Works()
		{
			var tile = Helpers.GetTile(50, 99, "Gibberish");
			var set = new VariableSizeTileCollection<string>();

			set.AddOrUpdate(tile);

			Assert.True(set.Contains(tile.Index));
		}

		[Fact]
		internal void Delete_Works()
		{
			var tile = Helpers.GetTile(1234, -324245, "Random numbers and stuf");
			var set = new VariableSizeTileCollection<string>();
			set.AddOrUpdate(tile);

			set.Delete(tile.Index);

			Assert.True(set.Count == 0);
		}

		[Fact]
		internal void Contains_Works()
		{
			var tile = Helpers.GetTile(12345, -33225, $"Name here plz");
			var set = new VariableSizeTileCollection<string>();

			Assert.False(set.Contains(tile.Index));

			set.AddOrUpdate(tile);

			Assert.True(set.Contains(tile.Index));
		}

		[Fact]
		internal void Indexer_Works()
		{
			var tile = Helpers.GetTile(-0987976, -23748, "Content");
			var set = new VariableSizeTileCollection<string>();

			set.AddOrUpdate(tile);

			Assert.True(tile.Data == set[tile.Index].Data);
		}

		[Fact]
		internal void AddOrUpdate_Updates()
		{
			var index = new TileIndex(552, 44332);
			var original = Helpers.GetTile(index.X, index.Y, "Original");
			var set = new VariableSizeTileCollection<string>();
			set.AddOrUpdate(original);

			var replacement = Helpers.GetTile(index.X, index.Y, "Replacement");
			set.AddOrUpdate(replacement);

			Assert.True(set.Count == 1);
			Assert.True(set[index].Data == replacement.Data);
		}
	}
}
