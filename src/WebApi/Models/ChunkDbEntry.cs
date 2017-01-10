using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealTimeLevelEditor;

namespace WebApi.Models
{
	public class ChunkDbEntry
	{
		/// <summary>
		/// The x-coordinate of the chunk's ChunkIndex.
		/// </summary>
		public long X { get; set; }

		/// <summary>
		/// The y-coordinate of the chunk's ChunkIndex.
		/// </summary>
		public long Y { get; set; }

		/// <summary>
		/// The ID of the level that the chunk belongs to.
		/// </summary>
		public Guid LevelId { get; set; }

		/// <summary>
		/// The <c>LevelChunk</c> object that makes up the chunk, serialized to JSON.
		/// </summary>
		public string JsonData { get; set; }

		public TileIndex ChunkIndex
		{
			get { return new TileIndex(X, Y); }
		}
	}
}
