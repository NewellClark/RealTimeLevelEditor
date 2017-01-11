﻿using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services
{
	public class LevelDatabaseRepository<T>
	{
		public LevelDatabaseRepository(ApplicationDbContext db)
		{
			_db = db;
			_defaultChunkSize = new Size(100, 100);
		}

		/// <summary>
		/// Loads the level with the specified <paramref name="id"/>. Throws if level does not exist.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Object containing a <c>Level`T</c> for the specified level, as well as other
		/// information about the level.</returns>
		/// <exception cref="ArgumentException">No level with specified <paramref name="id"/> exists.</exception>
		public ILoadedLevel<T> Load(Guid id)
		{
			var data = _db.Levels
				.Where(x => x.Id == id)
				.SingleOrDefault();
			if (data == null)
				throw new ArgumentException(
					$"There is no level in the database with an ID of {id}.");

			return new LoadedLevel()
			{
				Level = new Level<T>(
					new ChunkDatabaseRepository<T>(id, _db),
					data.ChunkSize),
				Id = id,
				Name = data.Name
			};
		}

		public ILoadedLevel<T> Create(string levelName)
		{
			var data = new LevelDbEntry();
			data.Name = levelName;
			_db.Add(data);
			_db.SaveChanges();

			return new LoadedLevel()
			{
				Level = new Level<T>(
					new ChunkDatabaseRepository<T>(data.Id, _db),
					_defaultChunkSize),
				Id = data.Id,
				Name = data.Name
			};
		}

		public bool Delete(Guid id)
		{
			var data = _db.Levels
				.Where(x => x.Id == id)
				.SingleOrDefault();
			if (data == null)
				return false;

			var chunkRepo = new ChunkDatabaseRepository<T>(id, _db);
			var toDelete = chunkRepo.Indeces.ToArray();
			foreach (var index in toDelete)
			{
				chunkRepo.Delete(index);
			}
			_db.Levels.Remove(data);
			_db.SaveChanges();

			return true;
		}

		public bool Exists(Guid levelId)
		{
			var data = _db.Levels
				.Where(x => x.Id == levelId)
				.SingleOrDefault();
			return data != null;
		}

		private class LoadedLevel : ILoadedLevel<T>
		{
			public Level<T> Level { get; set; }
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		private ApplicationDbContext _db;
		private readonly Size _defaultChunkSize;
	}
}