using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeLevelEditor;
using System.IO;

namespace LevelModelTests
{
	public class CachedFileChunkRepositoryTests : LevelTests
	{
		protected override Level<T> CreateDefault<T>(Size chunkSize)
		{
			return new Level<T>(
				new CachedChunkRepository<T>(
					new FileChunkRepository<T>(GetTestDirectory())),
				chunkSize);
		}

		protected string GetTestDirectory()
		{
			string root = Path.GetTempPath();
			Guid directory = Guid.NewGuid();
			string path = Path.Combine(root, Guid.NewGuid().ToString());

			return path;
		}
	}
}
