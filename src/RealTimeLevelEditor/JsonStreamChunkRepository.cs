using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Serializes and deserializes chunks to json using .NET streams.
	/// </summary>
	public partial class JsonStreamChunkRepository<T> : IChunkRepository<T>
	{
		public JsonStreamChunkRepository(IStreamProvider streamProvider)
		{
			throw new NotImplementedException();
		}
		public JsonStreamChunkRepository(
			ChunkStreamFactory readStreamFactory,
			ChunkStreamFactory writeStreamFactory) 
			: this(new DefaultStreamProvider(readStreamFactory, writeStreamFactory)) { }

		private IStreamProvider _streamProvider;

		IEnumerable<TileIndex> IChunkRepository<T>.Indeces
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		Tile<LevelChunk<T>> IChunkRepository<T>.Load(TileIndex chunkIndex)
		{
			throw new NotImplementedException();
		}

		void IChunkRepository<T>.Save(Tile<LevelChunk<T>> chunk)
		{
			throw new NotImplementedException();
		}

		bool IChunkRepository<T>.Contains(TileIndex chunkIndex)
		{
			throw new NotImplementedException();
		}

		bool IChunkRepository<T>.Delete(TileIndex chunkIndex)
		{
			throw new NotImplementedException();
		}
	}

	public interface IStreamProvider
	{
		Stream GetWriteStream(TileIndex chunkIndex);
		Stream GetReadStream(TileIndex chunkIndex);
	}
}
