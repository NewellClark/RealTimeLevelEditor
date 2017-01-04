using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public class Level<T> : TileCollection<T>
	{
		public Level(TileCollection<LevelChunk<T>> chunkRepository)
		{
			throw new NotImplementedException();
		}
		public override Tile<T> this[TileIndex index]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void AddOrUpdate(Tile<T> tile)
		{
			throw new NotImplementedException();
		}

		public override bool Contains(TileIndex index)
		{
			throw new NotImplementedException();
		}

		public override void Delete(TileIndex index)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<Tile<T>> GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
