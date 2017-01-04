using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public class Tile<T>
	{
		internal Tile(TileIndex index, T data)
		{
			Index = index;
			Data = data;
		}
		internal Tile(TileIndex index)
			: this(index, default(T)) { }

		public TileIndex Index { get; }
		public T Data { get; set; }
	}
}
