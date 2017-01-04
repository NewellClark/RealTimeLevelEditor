using MiscHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Stores the absolute location of a tile within a level.
	/// </summary>
	public struct TileIndex
	{
		[JsonConstructor]
		public TileIndex(long x, long y) 
		{
			X = x;
			Y = y;
		}
	
		public long X { get; }
		public long Y { get; }

		public override int GetHashCode()
		{
			return Hashing.StartingPrime
				.CombineHashCodes(X)
				.CombineHashCodes(Y);
		}
		public override bool Equals(object obj)
		{
			var casted = obj as TileIndex?;
			return casted == this;
		}
		public override string ToString()
		{
			return $"({X}, {Y})";
		}

		public static bool operator==(TileIndex lhs, TileIndex rhs)
		{
			return lhs.X == rhs.X &&
				lhs.Y == rhs.Y;
		}
		public static bool operator!=(TileIndex lhs, TileIndex rhs) => !(lhs == rhs);
	}
}
