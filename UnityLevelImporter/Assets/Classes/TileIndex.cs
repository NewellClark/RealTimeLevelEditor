using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace UnityLevelImporter
{
	[Serializable]
	struct TileIndex : IEquatable<TileIndex>
	{
		public TileIndex(int x, int y)
		{
			X = x;
			Y = y;
		}

		[JsonProperty]
		public int X { get; private set; }

		[JsonProperty]
		public int Y { get; private set; }

		public static bool operator ==(TileIndex left, TileIndex right)
		{
			return left.X == right.X &&
				left.Y == right.Y;
		}

		public static bool operator !=(TileIndex left, TileIndex right)
		{
			return !(left == right);
		}

		public bool Equals(TileIndex other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return this == obj as TileIndex?;
		}

		public override int GetHashCode()
		{
			int result = HashCode.Initialize();
			HashCode.Combine(ref result, X);
			HashCode.Combine(ref result, Y);

			return result;
		}

		public override string ToString()
		{
			return "(" + X + ", " + Y + ")";
		}
	}
}
