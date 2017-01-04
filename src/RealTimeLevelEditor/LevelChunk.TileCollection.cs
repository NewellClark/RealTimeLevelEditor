using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace RealTimeLevelEditor
{
	public partial class LevelChunk<T>
	{
		public class TileCollection : IEnumerable<Tile<T>>
		{
			internal TileCollection(Rectangle region, IEnumerable<Tile<T>> tiles)
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
			public void AddOrUpdate(Tile<T> tile)
			{
				if (!Region.Contains(tile.Index))
					throw new ArgumentOutOfRangeException(
						$"{nameof(tile)}.{nameof(tile.Index)}");

				_tiles[tile.Index] = tile;
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="tiles"></param>
			/// <exception cref="ArgumentOutOfRangeException"></exception>
			public void AddOrUpdate(IEnumerable<Tile<T>> tiles)
			{
				foreach (var tile in tiles)
				{
					AddOrUpdate(tile);
				}
			}

			public void Delete(TileIndex tile)
			{
				if (!Region.Contains(tile))
					throw new ArgumentOutOfRangeException(
						$"{tile} is outside of rectangular region {Region}.");
				_tiles.Remove(tile);
			}

			public void Delete(IEnumerable<TileIndex> tiles)
			{
				foreach (var tile in tiles)
				{
					Delete(tile);
				}
			}

			public void DeleteAll()
			{
				_tiles.Clear();
			}

			public bool Contains(TileIndex index)
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
			public Tile<T> this[TileIndex index]
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

			internal Rectangle Region { get; }

			private Dictionary<TileIndex, Tile<T>> _tiles;

			public IEnumerator<Tile<T>> GetEnumerator()
			{
				foreach (var value in _tiles.Values)
					yield return value;
			}
			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
