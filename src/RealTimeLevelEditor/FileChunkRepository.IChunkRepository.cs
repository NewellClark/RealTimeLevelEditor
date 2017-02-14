using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Saves chunks to files.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public partial class FileChunkRepository<T> : IChunkRepository<T>
	{
		public IEnumerable<TileIndex> Indeces
		{
			get
			{
				ThrowIfDisposed();

				var files = _directory.EnumerateFiles();
				foreach (var file in files)
				{
					var result = TryGetChunkIndexFromFilePath(file.FullName);
					if (result == null)
						continue;
					yield return result.Value;
				}
			}
		}

		public Tile<LevelChunk<T>> Load(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			string path = GetFilePathForChunkIndex(chunkIndex);

			using (Stream stream = File.OpenRead(path))
			using (TextReader reader = new StreamReader(stream))
			using (JsonReader json = new JsonTextReader(reader))
			{
				var serializer = new JsonSerializer();
				var chunk = serializer.Deserialize<LevelChunk<T>>(json);
				return new Tile<LevelChunk<T>>(chunkIndex, chunk);
			}
		}

		public void Save(Tile<LevelChunk<T>> chunk)
		{
			ThrowIfDisposed();

			string path = GetFilePathForChunkIndex(chunk.Index);

			using (FileStream stream = File.Create(path))
			using (TextWriter textWriter = new StreamWriter(stream))
			using (JsonTextWriter jsonWriter = new JsonTextWriter(textWriter))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(jsonWriter, chunk.Data);
			}
		}

		public bool Contains(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			return Indeces.Contains(chunkIndex);
		}

		public bool Delete(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			if (!Contains(chunkIndex))
				return false;

			string path = GetFilePathForChunkIndex(chunkIndex);
			File.Delete(path);

			return true;
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;
			_isDisposed = true;
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(nameof(FileChunkRepository<T>));

		}
		private bool _isDisposed;
	}
}
