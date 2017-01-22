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
		ILoadedLevel<T> Load(Guid levelId);
		ILoadedLevel<T> Create(string ownerId, Guid projectId, string levelName);
		IEnumerable<LevelInfoViewModel> FindLevelsForUser(string ownerId);
		bool Exists(Guid levelId);
		bool Delete(Guid levelId);
		LevelInfoViewModel GetInfo(Guid levelId);
	}
}
