using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// A TileCollection with a fixed bounding region that throws if any 
	/// attempt is made to access a tile outside its bounding region.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class FixedSizeTileCollection<T> : TileCollection<T>
	{
		public FixedSizeTileCollection(Rectangle region, IEnumerable<Tile<T>> tiles)
		{
			Region = region;
			_tiles = new Dictionary<TileIndex, Tile<T>>();
			AddOrUpdate(tiles);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tile"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public override void AddOrUpdate(Tile<T> tile)
		{
			if (!Region.Contains(tile.Index))
				throw new ArgumentOutOfRangeException(
					$"{nameof(tile)}.{nameof(tile.Index)}");

			_tiles[tile.Index] = tile;
		}

		public override void Delete(TileIndex tile)
		{
			if (!Region.Contains(tile))
				throw new ArgumentOutOfRangeException(
					$"{tile} is outside of rectangular region {Region}.");
			_tiles.Remove(tile);
		}

		public override bool Contains(TileIndex index)
		{
			return _tiles.ContainsKey(index);
		}

		/// <summary>
		/// Gets the tile at the specified index. 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		/// <exception cref="IndexOutOfRangeException">The specified TileIndex is not
		/// within the chunk's Region.</exception>
		public override Tile<T> this[TileIndex index]
		{
			get
			{
				if (!Contains(index))
					throw new IndexOutOfRangeException(
						$"{index} is not inside rectangular region {Region}.");
				return _tiles[index];
			}
		}

		public Tile<T> this[long x, long y]
		{
			get { return this[new TileIndex(x, y)]; }
		}

		public override void DeleteAll()
		{
			_tiles.Clear();
		}

		public override int Count => _tiles.Count;

		public Rectangle Region { get; }

		private Dictionary<TileIndex, Tile<T>> _tiles;

		public override IEnumerator<Tile<T>> GetEnumerator()
		{
			foreach (var value in _tiles.Values)
				yield return value;
		}
	}
}
