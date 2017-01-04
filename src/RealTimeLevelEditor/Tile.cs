﻿using MiscHelpers;
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
		internal Tile(TileIndex index, T data)
		{
			Index = index;
			Data = data;
		}
		internal Tile(TileIndex index)
			: this(index, default(T)) { }

		public TileIndex Index { get; }
		public T Data { get; set; }

		/// <summary>
		/// Overridden to use the hashcode of Index.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return Index.GetHashCode();
		}
	}
}