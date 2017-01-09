using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
	public interface ILoadedLevel<T>
	{
		Level<T> Level { get; }
		Guid Id { get; }
		string Name { get; }
	}
}
