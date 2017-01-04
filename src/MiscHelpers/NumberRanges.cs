using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiscHelpers
{
	public static class NumberRanges
	{
		/// <summary>
		/// Returns a series of integers, starting at min and ending at max.
		/// </summary>
		/// <param name="min">Lower-bound, inclusive.</param>
		/// <param name="max">Upper-bound, inclusive.</param>
		/// <returns></returns>
		public static IEnumerable<int> IntRange(int min, int max)
		{
			for (int i = min; i <= max; i++)
				yield return i;
		}
	}
}
