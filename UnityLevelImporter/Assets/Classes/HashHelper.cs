using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityLevelImporter
{
	static class HashCode
	{
		private const int _initialPrime = 86028157;
		private const int _hashingPrime = 982451653;

		public static int Initialize()
		{
			return _initialPrime;
		}

		public static void Combine<T>(ref int currentHash, T combineWithHashCodeOf)
		{
			unchecked
			{
				currentHash = currentHash * _hashingPrime + combineWithHashCodeOf.GetHashCode();
			}
		}
	}
}
