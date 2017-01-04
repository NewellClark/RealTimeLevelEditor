using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public abstract class TileCollection<T> : IEnumerable<Tile<T>>
	{
		public abstract void AddOrUpdate(Tile<T> tile);

		public abstract void Delete(TileIndex index);

		public abstract bool Contains(TileIndex index);

		public abstract Tile<T> this[TileIndex index] { get; }

		public abstract IEnumerator<Tile<T>> GetEnumerator();

		public virtual void AddOrUpdate(IEnumerable<Tile<T>> tiles)
		{
			foreach (var tile in tiles)
				AddOrUpdate(tile);
		}

		/// <summary>
		/// Deletes all tiles that are located at any of the supplied indeces.
		/// </summary>
		/// <param name="indeces"></param>
		public virtual void Delete(IEnumerable<TileIndex> indeces)
		{
			foreach (var index in indeces)
				Delete(index);
		}

		public virtual void DeleteAll()
		{
			Delete(this.Select(x => x.Index).ToArray());
		}

		public virtual int Count => this.Count();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
