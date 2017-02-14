using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeLevelEditor;
using WebApi.Data;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using WebApi.Models;
using System.Diagnostics;
using MiscHelpers;

namespace WebApi.Services
{
	public sealed class ChunkDatabaseRepository<T> : IChunkRepository<T>, IDisposable
	{
		public ChunkDatabaseRepository(
			Guid levelId,
			ApplicationDbContext db)
		{
			_db = db;
			_levelId = levelId;
		}

		public IEnumerable<TileIndex> Indeces
		{
			get
			{
				ThrowIfDisposed();

				return GetIndecesItterator();
			}
		}

		private IEnumerable<TileIndex> GetIndecesItterator()
		{
			foreach (var data in _db.Chunks.Where(x => x.LevelId == _levelId))
				yield return data.ChunkIndex;
		}

		public bool Contains(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			return LoadChunkDataFromDatabase(chunkIndex) != null;
		}

		public bool Delete(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			var data = LoadChunkDataFromDatabase(chunkIndex);
			if (data == null)
				return false;
			_db.Remove(data);
			_db.SaveChanges();
			return true;
		}

		public Tile<LevelChunk<T>> Load(TileIndex chunkIndex)
		{
			ThrowIfDisposed();

			ChunkDbEntry data = LoadChunkDataFromDatabase(chunkIndex);
			if (data == null)
				throw new ArgumentException(
					$"Level {_levelId} has no chunk with index {chunkIndex}.");
			var chunk = JsonConvert.DeserializeObject<Tile<LevelChunk<T>>>(data.JsonData);
			return chunk;
		}

		public void Save(Tile<LevelChunk<T>> chunk)
		{
			ThrowIfDisposed();

			var data = LoadChunkDataFromDatabase(chunk.Index);
			if (data == null)
			{
				data = CreateNewChunkDbEntry(chunk);
				_db.Add(data);
				_db.SaveChanges();
				return;
			}
			else
			{
				data.JsonData = JsonConvert.SerializeObject(chunk);
			}
			Debug.Assert(data.LevelId == _levelId);
			Debug.Assert(data.ChunkIndex == chunk.Index);
			_db.Update(data);
			_db.SaveChanges();
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;

			_db?.Dispose();
			_isDisposed = true;
		}

		private ApplicationDbContext _db;
		private Guid _levelId;
		private bool _isDisposed;

		private ChunkDbEntry LoadChunkDataFromDatabase(TileIndex chunkIndex)
		{
			//	Throws when MultipleActiveResults (in connection string) is false.
			var data = _db.Chunks
				//.ToList()
				.Where(x => new ChunkKeyMembers(x) == new ChunkKeyMembers(_levelId, chunkIndex))
				//.ToList()
				.SingleOrDefault();
			return data;
		}

		private ChunkDbEntry CreateNewChunkDbEntry(Tile<LevelChunk<T>> chunk)
		{
			return new ChunkDbEntry()
			{
				LevelId = _levelId,
				X = chunk.Index.X,
				Y = chunk.Index.Y,
				JsonData = JsonConvert.SerializeObject(chunk, Formatting.Indented)
			};
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(nameof(ChunkDatabaseRepository<T>));
		}

		private class ChunkKeyMembers
		{
			public ChunkKeyMembers(Guid levelId, long x, long y)
			{
				LevelId = levelId;
				X = x;
				Y = y;
			}
			public ChunkKeyMembers(ChunkDbEntry chunk) 
				: this(chunk.LevelId, chunk.X, chunk.Y) { }
			public ChunkKeyMembers(Guid id, TileIndex chunkIndex)
				: this(id, chunkIndex.X, chunkIndex.Y) { }

			public Guid LevelId { get; set; }

			public long X { get; set; }

			public long Y { get; set; }

			public static bool operator==(ChunkKeyMembers lhs, ChunkKeyMembers rhs)
			{
				return lhs?.LevelId == rhs?.LevelId &&
					lhs?.X == rhs?.X &&
					lhs?.Y == rhs?.Y;
			}

			public static bool operator!=(ChunkKeyMembers lhs, ChunkKeyMembers rhs)
			{
				return !(lhs == rhs);
			}

			public override bool Equals(object obj)
			{
				return this == (obj as ChunkKeyMembers);
			}

			public override int GetHashCode()
			{
				return Hashing.StartingPrime
					.CombineHashCodes(LevelId)
					.CombineHashCodes(X)
					.CombineHashCodes(Y);
			}
		}
	}
}
