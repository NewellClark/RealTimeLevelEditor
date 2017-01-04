using MiscHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public struct Size
	{
		[JsonConstructor]
		public Size(long x, long y)
		{
			X = x;
			Y = y;
		}

		public long X { get; }
		public long Y { get; }

		public override string ToString()
		{
			return $"({X}, {Y})";
		}
		public override int GetHashCode()
		{
			return Hashing.StartingPrime
				.CombineHashCodes(X)
				.CombineHashCodes(Y);
		}
		public override bool Equals(object obj)
		{
			var casted = obj as Size?;
			return this == casted;
		}

		public static bool operator==(Size lhs, Size rhs)
		{
			return lhs.X == rhs.X &&
				lhs.Y == rhs.Y;
		}
		public static bool operator!=(Size lhs, Size rhs) => !(lhs == rhs);
	}
}
