using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public interface IChunkRepository<T> : IDisposable
	{
		/// <summary>
		/// Loads the chunk with the specified index from the repository.
		/// </summary>
		/// <param name="chunkIndex"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">There is no chunk with the specified
		/// index in the repository. Call Exists() before trying to load a chunk.</exception>
		Tile<LevelChunk<T>> Load(TileIndex chunkIndex);

		/// <summary>
		/// Saves the specified LevelChunk to the repository.
		/// </summary>
		/// <param name="chunk"></param>
		void Save(Tile<LevelChunk<T>> chunk);

		/// <summary>
		/// Indicates whether there is a chunk with the specified index in the repository.
		/// </summary>
		/// <param name="chunkIndex"></param>
		/// <returns></returns>
		bool Contains(TileIndex chunkIndex);

		/// <summary>
		/// Gets the chunk-indeces of all the chunks that are in the repository.
		/// </summary>
		IEnumerable<TileIndex> Indeces { get; }

		/// <summary>
		/// Deletes the chunk with the specified chunkIndex if it exists.
		/// </summary>
		/// <param name="chunkIndex"></param>
		/// <returns>True if there was a chunk with the specified index in the repository.</returns>
		bool Delete(TileIndex chunkIndex);
	}

	public static class ChunkRepositoryExtensions
	{

	}
}
