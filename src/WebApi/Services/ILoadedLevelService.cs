using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
	public interface ILoadedLevelService<T>
	{
		ILoadedLevel<T> Load(Guid id);
		ILoadedLevel<T> Create(string levelName);
		bool Exists(Guid id);
		bool Delete(Guid id);
	}
}
