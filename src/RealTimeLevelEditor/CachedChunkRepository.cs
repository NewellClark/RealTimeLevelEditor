using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Wraps an IChunkRepository`T. Caches loaded chunks for faster lookups.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CachedChunkRepository<T> : IChunkRepository<T>
	{
		public CachedChunkRepository(IChunkRepository<T> inner)
		{
			_inner = inner;
			_cache = new VariableSizeTileCollection<LevelChunk<T>>();
			_indeces = new Lazy<HashSet<TileIndex>>(HandleIndecesInitializing);
			_readOnlyIndeces = new Lazy<IEnumerable<TileIndex>>(
				() => _indeces.Value.AsEnumerable());
		}

		public Tile<LevelChunk<T>> Load(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			if (!_cache.Contains(chunkIndex))
			{
				var result = _inner.Load(chunkIndex);
				_cache.AddOrUpdate(result);
				return result;
			}

			return _cache[chunkIndex];
		}

		public void Save(Tile<LevelChunk<T>> chunk)
		{
			ThrowIfDisposed();

			_indeces.Value.Add(chunk.Index);
			_cache.AddOrUpdate(chunk);
			_inner.Save(chunk);
		}

		public bool Contains(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			return _indeces.Value.Contains(chunkIndex);
		}

		public bool Delete(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			if (_indeces.Value.Remove(chunkIndex))
			{
				_cache.Delete(chunkIndex);
				_inner.Delete(chunkIndex);
				return true;
			}

			return false;
		}

		public IEnumerable<TileIndex> Indeces
		{
			get
			{
				ThrowIfDisposed();
				return _indeces.Value;
			}
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;
			_isDisposed = true;
			_inner?.Dispose();
		}


		private HashSet<TileIndex> HandleIndecesInitializing()
		{
			return new HashSet<TileIndex>(_inner.Indeces);
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(
					$@"{nameof(CachedChunkRepository<T>)} containing {_inner?.GetType().Name}");
		}

		private Lazy<HashSet<TileIndex>> _indeces;
		private Lazy<IEnumerable<TileIndex>> _readOnlyIndeces;
		private VariableSizeTileCollection<LevelChunk<T>> _cache;
		private IChunkRepository<T> _inner;
		private bool _isDisposed;
	}
}
