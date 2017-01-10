using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ViewModels.Levels;

namespace WebApi.Models
{
	public interface ILoadedLevel<T>
	{
		Level<T> Level { get; }
		LevelInfoViewModel Info { get; }
	}
}
