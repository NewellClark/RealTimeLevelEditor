using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public class VariableSizeTileCollection<T> : TileCollection<T>
	{
		public VariableSizeTileCollection() 
			: this(new Tile<T>[] { }) { }
		public VariableSizeTileCollection(IEnumerable<Tile<T>> tiles)
		{
			_tiles = new Dictionary<TileIndex, Tile<T>>();
			AddOrUpdateNonVirtual(tiles);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		/// <exception cref="KeyNotFoundException">No tile with the specified index
		/// in the current collection.</exception>
		public override Tile<T> this[TileIndex index]
		{
			get
			{
				return _tiles[index];
			}
		}

		/// <summary>
		/// If a tile with the specified index already exists, it is updated to the 
		/// specified value. If not, the specified tile is added to the collection.
		/// </summary>
		/// <param name="tile"></param>
		public override void AddOrUpdate(Tile<T> tile)
		{
			AddOrUpdateNonVirtual(tile);
		}

		/// <summary>
		/// Indicates whether there is a tile in the current collection with the 
		/// specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override bool Contains(TileIndex index)
		{
			return _tiles.ContainsKey(index);
		}

		/// <summary>
		/// If there is a tile with the specified index in the current collection,
		/// it is deleted. If there is no tile with the specified index, nothing happens.
		/// </summary>
		/// <param name="index"></param>
		public override void Delete(TileIndex index)
		{
			_tiles.Remove(index);
		}

		public override IEnumerator<Tile<T>> GetEnumerator()
		{
			foreach (var tile in _tiles.Values)
			{
				yield return tile;
			}
		}

		/// <summary>
		/// It's bad practice to call a virtual method in a constructor, so 
		/// putting the body of AddOrUpdate() in here. 
		/// </summary>
		/// <param name="tile"></param>
		private void AddOrUpdateNonVirtual(Tile<T> tile)
		{
			_tiles[tile.Index] = tile;
		}
		private void AddOrUpdateNonVirtual(IEnumerable<Tile<T>> tiles)
		{
			foreach (var tile in tiles)
				AddOrUpdateNonVirtual(tile);
		}

		private Dictionary<TileIndex, Tile<T>> _tiles;
	}
}
