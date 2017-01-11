using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApiTests
{
	public class LevelDatabaseRepositoryTests
	{
		[Fact]
		internal void Create_PersistsCreatedLevel()
		{
			using (var db = new DbProvider())
			{
				var repo = new LoadedLevelService<string>(db.DbContext);
				var level = repo.Create(_testOwnerId, GetTestName());

				var inDatabase = db.DbContext.Levels
					.Where(x => x.Id == level.Info.LevelId)
					.SingleOrDefault();

				Assert.True(level.Info.LevelId == inDatabase.Id);
				Assert.True(level.Info.Name == inDatabase.Name);
			}
		}

		[Fact]
		internal void Load_LoadsExistingLevel()
		{
			using (var db = new DbProvider())
			{
				var repo = new LoadedLevelService<string>(db.DbContext);
				var level = repo.Create(_testOwnerId, GetTestName());
				var loaded = repo.Load(level.Info.LevelId);

				Assert.Equal(level.Info.LevelId, loaded.Info.LevelId);
				Assert.Equal(level.Info.Name, loaded.Info.Name);
			}
		}

		[Fact]
		internal void Load_ThrowsWhenLevelDoesntExist()
		{
			using (var db = new DbProvider())
			{
				var repo = new LoadedLevelService<string>(db.DbContext);
				var id = Guid.NewGuid();
				repo.Delete(id);

				Assert.Throws<ArgumentException>(
					() => repo.Load(id));
			}
		}

		[Fact]
		internal void Delete_RemovesDeletedLevel()
		{
			using (var db = new DbProvider())
			{
				var repo = new LoadedLevelService<string>(db.DbContext);
				var level = repo.Create(_testOwnerId, GetTestName());
				var levelDbModel = db.DbContext.Levels
					.Where(x => x.Id == level.Info.LevelId)
					.SingleOrDefault();

				repo.Delete(level.Info.LevelId);

				Assert.DoesNotContain(levelDbModel, db.DbContext.Levels);
			}
		}


		private string GetTestName([CallerMemberName]string testMemberName = "")
		{
			return $"{nameof(LevelDatabaseRepositoryTests)}.{testMemberName}_{DateTime.Now}";
		}
		
		/// <summary>
		/// Provides an <c>ApplicationDbContext</c> for use in tests.
		/// When disposed, deletes all levels that weren't in the database when it was created.
		/// </summary>
		private class DbProvider : IDisposable
		{
			public DbProvider()
			{
				DbContext = Common.GetDbContext();
				var preExistingLevels = DbContext.Levels
					.Select(x => x.Id)
					.ToArray();
				_preExistingLevels = new HashSet<Guid>(preExistingLevels);
			}

			public ApplicationDbContext DbContext { get; }

			public void Dispose()
			{
				var levelsToDelete = DbContext?.Levels?
					.Where(x => !_preExistingLevels.Contains(x.Id));
				foreach (var level in levelsToDelete)
				{
					DbContext?.Levels?.Remove(level);
				}
				DbContext?.SaveChanges();
			}

			private HashSet<Guid> _preExistingLevels;
		}

		private const string _testOwnerId = "3ab63d0e-f044-45da-894d-eccffc984e06";
	}
}
