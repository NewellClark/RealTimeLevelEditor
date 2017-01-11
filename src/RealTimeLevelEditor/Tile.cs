using MiscHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// A single tile within a level.
	/// </summary>
	/// <typeparam name="T">Data to store with a tile. Note that this object must
	/// be Json-serializable using Newtonsoft's JSON serialization library.</typeparam>
	public class Tile<T>
	{
		public Tile(TileIndex index, T data)
		{
			Index = index;
			Data = data;
		}
		public Tile(TileIndex index)
			: this(index, default(T)) { }
		[JsonConstructor]
		private Tile() { }

		[JsonProperty]
		public TileIndex Index { get; private set; }

		public T Data { get; set; }

		/// <summary>
		/// Overridden to use the hashcode of Index.
		/// </summary>
		/// <returns></returns>
		//public override int GetHashCode()
		//{
		//	return Index.GetHashCode();
		//}
	}
}
