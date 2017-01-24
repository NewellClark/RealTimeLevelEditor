using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace UnityLevelImporter
{
	[Serializable]
	struct Rectangle : IEquatable<Rectangle>
	{
		public Rectangle(int left, int top, int width, int height)
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		[JsonProperty]
		public int Left { get; private set; }

		[JsonProperty]
		public int Top { get; private set; }

		[JsonProperty]
		public int Width { get; private set; }

		[JsonProperty]
		public int Height { get; private set; }

		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.Left == right.Left &&
				left.Top == right.Top &&
				left.Width == right.Width &&
				left.Height == right.Height;
		}

		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !(left == right);
		}

		public bool Equals(Rectangle other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			return this == obj as Rectangle?;
		}

		public override int GetHashCode()
		{
			int result = HashCode.Initialize();
			HashCode.Combine(ref result, Left);
			HashCode.Combine(ref result, Top);
			HashCode.Combine(ref result, Width);
			HashCode.Combine(ref result, Height);

			return result;
		}

		public override string ToString()
		{
			return "{ " + Left + ", " + Top + ", " + Width + ", " + Height + " }";
		}
	}
}
