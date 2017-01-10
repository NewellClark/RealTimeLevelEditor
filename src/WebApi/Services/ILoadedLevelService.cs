using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.ViewModels.Levels;

namespace WebApi.Services
{
	public interface ILoadedLevelService<T>
	{
		ILoadedLevel<T> Load(Guid id);
		ILoadedLevel<T> Create(string ownerId, string levelName);
		IEnumerable<LevelInfoViewModel> FindLevelsForUser(string ownerId);
		bool Exists(Guid id);
		bool Delete(Guid id);
	}
}
