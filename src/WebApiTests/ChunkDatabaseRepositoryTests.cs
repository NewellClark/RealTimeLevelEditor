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
		protected override Level<T> CreateDefault<T>()
		{
			var repo = new ChunkDatabaseRepository<T>(
				_defaultLevelId, Common.GetDbContext());

			return new Level<T>(repo, _defaultSize);
		}

		protected readonly Guid _defaultLevelId = default(Guid);
		protected readonly Size _defaultSize = new Size(40, 40);
	}
}
