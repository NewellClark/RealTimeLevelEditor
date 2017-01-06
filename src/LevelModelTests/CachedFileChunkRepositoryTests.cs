using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeLevelEditor;

namespace LevelModelTests
{
	public class CachedFileChunkRepositoryTests : FileChunkRepositoryTests
	{
		protected override Level<T> CreateDefault<T>()
		{
			return new Level<T>(
				new CachedChunkRepository<T>(
					new FileChunkRepository<T>(GetTestDirectory())),
				_chunkSize);
		}
	}
}
