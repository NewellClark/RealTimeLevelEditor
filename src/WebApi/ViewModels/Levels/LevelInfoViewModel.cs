using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.ViewModels.Levels
{
	public class LevelInfoViewModel
	{
		public LevelInfoViewModel(LevelDbEntry level)
		{
			LevelId = level.Id;
			OwnerId = level.OwnerId;
			DateCreated = level.DateCreated;
			Name = level.Name;
			ChunkWidth = level.ChunkWidth;
			ChunkHeight = level.ChunkHeight;
		}

		public Guid LevelId { get; set; }
		public string OwnerId { get; set; }
		public DateTime DateCreated { get; set; }
		public string Name { get; set; }
		public long ChunkWidth { get; set; }
		public long ChunkHeight { get; set; }
	}
}
