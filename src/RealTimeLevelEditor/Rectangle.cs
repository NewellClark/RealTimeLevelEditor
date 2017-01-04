using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiscHelpers;

namespace RealTimeLevelEditor
{

	public struct Rectangle
	{
		public Rectangle(long left, long top, long width, long height)
		{
			if (width < 0)
				throw new ArgumentOutOfRangeException(nameof(width));
			if (height < 0)
				throw new ArgumentOutOfRangeException(nameof(height));

			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		public long Left { get; }
		public long Top { get; }
		public long Width { get; }
		public long Height { get; }

		public long Right => Left + Width;
		public long Bottom => Top + Height;
		public long Area => Width * Height;

		public override int GetHashCode()
		{
			return Hashing.StartingPrime
				.CombineHashCodes(Left)
				.CombineHashCodes(Top)
				.CombineHashCodes(Width)
				.CombineHashCodes(Height);
		}
		public override bool Equals(object obj)
		{
			var casted = obj as Rectangle?;
			return casted == this;
		}

		/// <summary>
		/// Checks for value equality using Left, Top, Width, and Height
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator==(Rectangle lhs, Rectangle rhs)
		{
			return lhs.Left == rhs.Left &&
				lhs.Top == rhs.Top &&
				lhs.Width == rhs.Width &&
				lhs.Height == rhs.Height;
		}
		public static bool operator!=(Rectangle lhs, Rectangle rhs) => !(lhs == rhs);
	}
}
