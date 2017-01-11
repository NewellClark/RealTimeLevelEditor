using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
	public class LevelDbEntry
	{
		public Guid Id { get; set; }

		public string OwnerId { get; set; }

		public virtual ApplicationUser Owner { get; set; }

		public DateTime DateCreated { get; set; }

		/// <summary>
		/// Descriptive name given by the user.
		/// </summary>
		public string Name { get; set; }

		public long ChunkWidth { get; set; }

		public long ChunkHeight { get; set; }

		/// <summary>
		/// Collection containing the indeces of all chunks that exist in the level, serialized
		/// to JSON.
		/// </summary>
		public string ChunkIndecesJson { get; set; }

		public Size ChunkSize
		{
			get { return new Size(ChunkWidth, ChunkHeight); }
		}
	}
}
