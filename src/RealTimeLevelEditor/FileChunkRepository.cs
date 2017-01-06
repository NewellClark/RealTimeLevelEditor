using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public partial class FileChunkRepository<T> : IChunkRepository<T>
	{
		public FileChunkRepository(string directoryPath)
		{
			_directory = new DirectoryInfo(directoryPath);
			if (!_directory.Exists)
				_directory.Create();
		}

		
		private string GetFilePathForChunkIndex(TileIndex chunkIndex)
		{
			string root = Path.Combine(
				_directory.FullName,
				chunkIndex.ToString());
			return $"{root}.{_chunkExtension}";
		}

		private TileIndex? TryGetChunkIndexFromFilePath(string filePath)
		{
			if (!Path.HasExtension(filePath))
				return null;

			return TileIndex.TryParse(Path.GetFileNameWithoutExtension(filePath));
		}

		private DirectoryInfo _directory;
		private const string _chunkExtension = "chunk";
	}
}
