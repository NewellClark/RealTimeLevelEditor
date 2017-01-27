using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UnityLevelImporter;
using System.IO;

namespace UnityLevelImporterTests
{
	public abstract class LevelImporterTests
	{
		protected abstract LevelImporter GetLevelImporter(ImportedLevel levelChunks);

		[Fact]
		public void LoadLevel_LoadsLevelPreservingContents()
		{
			var original = CreateSampleLevel();
			var importer = GetLevelImporter(original);

			var deserialized = importer.LoadLevel();

			Assert.True(AreLevelsEqual(original, deserialized), "Deserialized chunks are not the same as serialized chunks.");
		}

		protected static bool AreChunksEqualByValue(LevelChunk left, LevelChunk right)
		{
			if (left.Region != right.Region)
				return false;
			var leftTiles = new HashSet<Tile>(left.Tiles, new TileByValueEqualityComparer());
			if (right.Tiles.Length != leftTiles.Count)
				return false;
			foreach (var tile in right.Tiles)
				if (!leftTiles.Contains(tile))
					return false;

			return true;
		}

		protected static bool AreTilesEqualByValue(Tile left, Tile right)
		{
			return _tileComparer.Equals(left, right);
		}

		protected static bool AreLevelsEqual(ImportedLevel left, ImportedLevel right)
		{
			return AreLevelsEqual(left.Chunks, right.Chunks);
		}
		protected static bool AreLevelsEqual(IEnumerable<LevelChunk> left, IEnumerable<LevelChunk> right)
		{
			var leftSet = new HashSet<LevelChunk>(left, new ChunksByContentsEqualityComparer());
			if (leftSet.Count != right.Count())
				return false;
			foreach (var chunk in right)
				if (!leftSet.Contains(chunk))
					return false;

			return true;
		}

		private class ChunksByContentsEqualityComparer : IEqualityComparer<LevelChunk>
		{
			public bool Equals(LevelChunk x, LevelChunk y)
			{
				return AreChunksEqualByValue(x, y);
			}

			public int GetHashCode(LevelChunk obj)
			{
				return obj.Region.GetHashCode();
			}
		}

		private class TileByValueEqualityComparer : EqualityComparer<Tile>
		{
			public override bool Equals(Tile left, Tile right)
			{
				return left?.Index == right?.Index &&
					left?.Data == right?.Data;
			}

			public override int GetHashCode(Tile obj)
			{
				int result = HashCode.Initialize();
				HashCode.Combine(ref result, obj.Index);
				HashCode.Combine(ref result, obj.Data);

				return result;
			}
		}

		private static TileByValueEqualityComparer _tileComparer = new TileByValueEqualityComparer();

		private IEnumerable<LevelChunk> CreateSampleChunks()
		{
			var results = new List<LevelChunk>();
			var builder1 = new LevelChunkBuilder();
			builder1.Region = new Rectangle(10, 5, 15, 10);
			builder1.AddTile(new Tile(new TileIndex(12, 8), "red"));
			builder1.AddTile(new Tile(new TileIndex(20, 13), "green"));
			results.Add(builder1.ToLevelChunk());

			var builder2 = new LevelChunkBuilder();
			builder2.Region = new Rectangle(25, 5, 15, 10);
			builder2.AddTile(new Tile(new TileIndex(29, 5), "blue"));
			builder2.AddTile(new Tile(new TileIndex(39, 14), "green"));
			results.Add(builder2.ToLevelChunk());

			return results.ToArray();
		}
		private ImportedLevel CreateSampleLevel()
		{
			var result = new ImportedLevel();
			result.Chunks = CreateSampleChunks().ToArray();

			return result;
		}

	}
}
