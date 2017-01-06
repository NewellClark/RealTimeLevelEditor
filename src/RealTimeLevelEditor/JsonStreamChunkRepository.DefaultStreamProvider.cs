using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Represents a method that gets a stream for accessing a chunk.
	/// The stream must be in the correct position when it is returned.
	/// </summary>
	/// <param name="chunkIndex"></param>
	/// <returns></returns>
	public delegate Stream ChunkStreamFactory(TileIndex chunkIndex);

	[Obsolete("Not using this. Not a good abstraction. No way to find out if chunks exist.")]
	public partial class JsonStreamChunkRepository<T>
	{
		private class DefaultStreamProvider : IStreamProvider
		{
			public DefaultStreamProvider(
				ChunkStreamFactory readStreamFactory,
				ChunkStreamFactory writeStreamFactory)
			{
				_readStreamFactory = readStreamFactory;
				_writeStreamFactory = writeStreamFactory;
			}

			public Stream GetReadStream(TileIndex chunkIndex)
			{
				return _readStreamFactory(chunkIndex);
			}

			public Stream GetWriteStream(TileIndex chunkIndex)
			{
				return _writeStreamFactory(chunkIndex);
			}

			private ChunkStreamFactory _readStreamFactory;
			private ChunkStreamFactory _writeStreamFactory;
		}
	}
}
