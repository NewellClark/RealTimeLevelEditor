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
	public class ChunkDatabaseRepository<T> : IChunkRepository<T>
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
				foreach (var data in _db.Chunks.Where(x => x.LevelId == _levelId))
					yield return data.ChunkIndex;
			}
		}

		public bool Contains(TileIndex chunkIndex)
		{
			return LoadChunkDataFromDatabase(chunkIndex) != null;
		}

		public bool Delete(TileIndex chunkIndex)
		{
			var data = LoadChunkDataFromDatabase(chunkIndex);
			if (data == null)
				return false;
			_db.Remove(data);
			_db.SaveChanges();
			return true;
		}

		public Tile<LevelChunk<T>> Load(TileIndex chunkIndex)
		{
			ChunkDbEntry data = LoadChunkDataFromDatabase(chunkIndex);
			if (data == null)
				throw new ArgumentException(
					$"Level {_levelId} has no chunk with index {chunkIndex}.");
			var chunk = JsonConvert.DeserializeObject<Tile<LevelChunk<T>>>(data.JsonData);
			return chunk;
		}

		public void Save(Tile<LevelChunk<T>> chunk)
		{
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

		private ApplicationDbContext _db;
		private Guid _levelId;

		private ChunkDbEntry LoadChunkDataFromDatabase(TileIndex chunkIndex)
		{
			var data = _db.Chunks
				.Where(x => new ChunkKeyMembers(x) == new ChunkKeyMembers(_levelId, chunkIndex))
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
