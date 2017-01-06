using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LevelModelTests
{
	public partial class LevelTests
	{
		private class MockRepository<T> : IChunkRepository<T>
		{
			private VariableSizeTileCollection<LevelChunk<T>> _backing =
				new VariableSizeTileCollection<LevelChunk<T>>();

			public IEnumerable<TileIndex> Indeces
			{
				get
				{
					foreach (var chunk in _backing)
					{
						yield return chunk.Index;
					}
				}
			}

			public bool Contains(TileIndex chunkIndex)
			{
				return _backing.Contains(chunkIndex);
			}

			public bool Delete(TileIndex chunkIndex)
			{
				bool result = _backing.Contains(chunkIndex);
				_backing.Delete(chunkIndex);
				return result;
			}

			public Tile<LevelChunk<T>> Load(TileIndex chunkIndex)
			{
				if (!_backing.Contains(chunkIndex))
					throw new ArgumentException(
						$"No chunk with index {chunkIndex}");

				return _backing[chunkIndex];
			}

			public void Save(Tile<LevelChunk<T>> chunk)
			{
				_backing.AddOrUpdate(chunk);
			}
		}
	}
}
