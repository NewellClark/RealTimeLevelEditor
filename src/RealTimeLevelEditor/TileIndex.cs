using MiscHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Stores the absolute coordinates of a tile within a level.
	/// All equality methods and operators have been overridden to take all
	/// coordinates into account. 
	/// GetHashCode() has also been overridden to allow efficient hashing. 
	/// </summary>
	public struct TileIndex
	{
		public TileIndex(long x, long y)
		{
			X = x;
			Y = y;
		}
		
		[JsonProperty]
		public long X { get; private set; }

		[JsonProperty]
		public long Y { get; private set; }

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

		public static bool operator==(TileIndex lhs, TileIndex rhs)
		{
			return lhs.X == rhs.X &&
				lhs.Y == rhs.Y;
		}
		public static bool operator!=(TileIndex lhs, TileIndex rhs) => !(lhs == rhs);

		public TileIndex ToChunkIndex(Size chunkSize)
		{
			return new TileIndex(
				DivideRoundingDown(X, chunkSize.X),
				DivideRoundingDown(Y, chunkSize.Y));
		}

		private static long DivideRoundingDown(long dividend, long divisor)
		{
			if (divisor == 0)
				throw new DivideByZeroException();

			long roundedTowardsZero = dividend / divisor;
			bool exact = dividend % divisor == 0;
			if (exact)
				return roundedTowardsZero;
			bool wasRoundedDown = (dividend > 0) == (divisor > 0);
			if (wasRoundedDown)
				return roundedTowardsZero;
			return roundedTowardsZero - 1;
		}

		public override string ToString()
		{
			return $"{_openingBracket}{X}{_componentSeparator}{Y}{_closingBracket}";
		}
		/// <summary>
		/// Attempts to parse the specified text into a TileIndex object.
		/// If the text cannot be parsed, null will be returned.
		/// Works on text produced via ToString().
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static TileIndex? TryParse(string text)
		{
			string stripped = Regex.Replace(text, @"\s+", "");
			Match match = _parsePattern.Value.Match(text);
			if (!match.Success)
				return null;

			long x, y;
			if (long.TryParse(match.Groups[nameof(TileIndex.X)].Value, out x) &&
				long.TryParse(match.Groups[nameof(TileIndex.Y)].Value, out y))
			{
				return new TileIndex(x, y);
			}

			return null;
		}

		private const string _openingBracket = "(";
		private const string _closingBracket = ")";
		private const string _componentSeparator = ",";

		private static Regex CreateParseRegex(
			string openBracket,
			string componentSeparator,
			string closeBracket)
		{
			string start = Regex.Escape(openBracket);
			string x = $@"(?<{nameof(TileIndex.X)}>-?\d+)";
			string split = Regex.Escape(componentSeparator);
			string y = $@"(?<{nameof(TileIndex.Y)}>-?\d+)";
			string end = Regex.Escape(closeBracket);

			string pattern = $@"^{start}{x}{split}{y}{end}$";
			return new Regex(pattern);
		}

		private static Lazy<Regex> _parsePattern = new Lazy<Regex>(
			() => CreateParseRegex(_openingBracket, _componentSeparator, _closingBracket),
			true);
	}
}
