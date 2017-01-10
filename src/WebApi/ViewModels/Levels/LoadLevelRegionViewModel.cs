using Newtonsoft.Json;
using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModels.Levels
{
	public class LoadLevelRegionViewModel
	{
		[Required]
		public Guid LevelId { get; set; }

		[Required]
		public long Left { get; set; }
		
		[Required]
		public long Top { get; set; }

		[Required]
		public long Width { get; set; }

		[Required]
		public long Height { get; set; }

		[JsonIgnore]
		public Rectangle Region => new Rectangle(Left, Top, Width, Height);
	}
}
