using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public partial class LevelChunk<T>
	{
		[JsonConstructor]
		internal LevelChunk(Rectangle region, IEnumerable<Tile<T>> tiles)
		{
			Tiles = new TileCollection(region, tiles);
		}

		internal LevelChunk(Rectangle region) 
			: this(region, new Tile<T>[] { }) { }

		public Rectangle Region
		{
			get { return Tiles.Region; }
		}

		public TileCollection Tiles { get; }
	}

	//internal class LevelChunk<T>
	//{
	//	[JsonConstructor]
	//	internal LevelChunk(Rectangle region)
	//	{
	//		Region = region;
	//		Tiles = new TileCollection(Region);
	//	}

	//	[OnDeserialized]
	//	private void SetInnerCollectionRegion(StreamingContext context)
	//	{
	//		Tiles.Region = Region;
	//	}

	//	public Rectangle Region { get; }

	//	#region OptionOne_DirectlyInClass
	//	///// <summary>
	//	///// Updates the tile at the index of the specified tile so that its Data is
	//	///// equal to the specified tile's data. 
	//	///// If there is no tile at the specified index, then a new tile is created and added, with its
	//	///// tile data set to equal that of tile.
	//	///// </summary>
	//	///// <param name="tile"></param>
	//	///// <exception cref="ArgumentOutOfRangeException">tile.TileIndex is outside the current chunk.</exception>
	//	//public void AddOrUpdate(Tile<T> tile)
	//	//{
	//	//	throw new NotImplementedException();
	//	//}

	//	///// <summary>
	//	///// Updates all the tiles at the indicated indeces, or adds them if they are
	//	///// not already in the chunk.
	//	///// </summary>
	//	///// <param name="tiles"></param>
	//	///// <exception cref="ArgumentOutOfRangeException">The TileIndex property of one of the specified 
	//	///// tiles is outside the bounds of the current chunk.</exception>
	//	//public void AddOrUpdate(IEnumerable<Tile<T>> tiles)
	//	//{
	//	//	foreach (var tile in tiles)
	//	//		AddOrUpdate(tile);
	//	//}

	//	///// <summary>
	//	///// Deletes the tile at the specified TileIndex.
	//	///// </summary>
	//	///// <param name="toDelete"></param>
	//	///// <exception cref="ArgumentOutOfRangeException">The specified TileIndex is outside the 
	//	///// current chunk.</exception>
	//	//public void Delete(TileIndex toDelete)
	//	//{
	//	//	throw new NotImplementedException();
	//	//}

	//	///// <summary>
	//	///// Deletes all tiles at the specified indeces.
	//	///// </summary>
	//	///// <param name="indeces"></param>
	//	///// <exception cref="ArgumentOutOfRangeException">One of the specified TileIndex objects is
	//	///// outside the current chunk.</exception>
	//	//public void Delete(IEnumerable<TileIndex> indeces)
	//	//{
	//	//	foreach (var index in indeces)
	//	//	{
	//	//		Delete(index);
	//	//	}
	//	//}

	//	//[JsonProperty]
	//	//public IEnumerable<Tile<T>> Tiles
	//	//{
	//	//	get { throw new NotImplementedException(); }
	//	//	private set { throw new NotImplementedException(); }
	//	//}

	//	//public IEnumerable<TileIndex> Indeces
	//	//{
	//	//	get { throw new NotImplementedException(); }
	//	//}


	//	//[JsonIgnore]
	//	//private Dictionary<TileIndex, T> _tileLookup;
	//	#endregion

	//	#region OptionTwo_TileCollectionClass
	//	[JsonIgnore]
	//	public TileCollection Tiles { get; private set; }

	//	[JsonProperty]
	//	private IEnumerable<Tile<T>> tiles
	//	{
	//		get { return Tiles; }
	//		set { Tiles = new TileCollection(value); }
	//	}

	//	public class TileCollection : IEnumerable<Tile<T>>
	//	{
	//		[JsonConstructor]
	//		internal TileCollection(IEnumerable<Tile<T>> tiles)
	//		{
	//			_test = new List<Tile<T>>(tiles);
	//		}
	//		internal TileCollection() { }
	//		internal TileCollection(Rectangle region) : this()
	//		{
	//			Region = region;
	//		}

	//		/// <summary>
	//		/// 
	//		/// </summary>
	//		/// <param name="tile"></param>
	//		/// <exception cref="ArgumentOutOfRangeException"></exception>
	//		public void AddOrUpdate(Tile<T> tile)
	//		{
	//			throw new NotImplementedException();
	//		}

	//		/// <summary>
	//		/// 
	//		/// </summary>
	//		/// <param name="tiles"></param>
	//		/// <exception cref="ArgumentOutOfRangeException"></exception>
	//		public void AddOrUpdate(IEnumerable<Tile<T>> tiles)
	//		{
	//			foreach (var tile in tiles)
	//			{
	//				AddOrUpdate(tile);
	//			}
	//		}

	//		/// <summary>
	//		/// 
	//		/// </summary>
	//		/// <param name="index"></param>
	//		/// <exception cref="ArgumentOutOfRangeException"></exception>
	//		public void Delete(TileIndex index)
	//		{
	//			throw new NotImplementedException();
	//		}

	//		/// <summary>
	//		/// 
	//		/// </summary>
	//		/// <param name="indeces"></param>
	//		/// <exception cref="ArgumentOutOfRangeException"></exception>
	//		public void Delete(IEnumerable<TileIndex> indeces)
	//		{
	//			foreach (var index in indeces)
	//			{
	//				Delete(index);
	//			}
	//		}

	//		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	//		public IEnumerator<Tile<T>> GetEnumerator()
	//		{
	//			return _test.GetEnumerator();
	//		}

	//		internal Rectangle Region { get; set; }

	//		private List<Tile<T>> _test = new List<Tile<T>>();
	//	}
	//	#endregion
	//}

}
