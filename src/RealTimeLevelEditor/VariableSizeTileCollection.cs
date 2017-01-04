using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public class VariableSizeTileCollection<T> : TileCollection<T>
	{
		public VariableSizeTileCollection(IEnumerable<Tile<T>> tiles)
		{
			_tiles = new Dictionary<TileIndex, Tile<T>>();
			
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


		private Dictionary<TileIndex, Tile<T>> _tiles;
	}
}
