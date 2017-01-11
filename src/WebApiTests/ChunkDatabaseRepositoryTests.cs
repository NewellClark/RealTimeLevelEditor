using LevelModelTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using RealTimeLevelEditor;
using WebApi.Services;

namespace WebApiTests
{
	public class ChunkDatabaseRepositoryTests : LevelTests
	{
		protected override Level<T> CreateDefault<T>(Size chunkSize)
		{
			var repo = new ChunkDatabaseRepository<T>(
				_defaultLevelId, Common.GetDbContext());

			return new Level<T>(repo, chunkSize);
		}

		protected override void Cleanup()
		{
			base.Cleanup();

			var db = Common.GetDbContext();
			var toDelete = db.Chunks
				.Where(x => x.LevelId == _defaultLevelId);
			db.RemoveRange(toDelete);
			db.SaveChanges();
		}

		protected readonly Guid _defaultLevelId = Guid.NewGuid();
		//protected readonly Size _defaultSize = new Size(40, 40);
	}
}
