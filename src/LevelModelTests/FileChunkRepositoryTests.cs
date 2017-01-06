using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class FileChunkRepositoryTests : LevelTests
	{
		protected override Level<T> CreateDefault<T>()
		{
			var repo = new FileChunkRepository<T>(GetTestDirectory());
			return new Level<T>(repo, _chunkSize);
		}

		private string GetTestDirectory()
		{
			string root = Path.GetTempPath();
			Guid directory = Guid.NewGuid();
			string path = Path.Combine(root, Guid.NewGuid().ToString());

			return path;
		}
	}
}
