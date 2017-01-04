using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiscHelpers
{
	public static class Hashing
	{
		/// <summary>
		/// Combines the current hashcode with the hashcode of the specified argument.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="hashCode"></param>
		/// <param name="arg">The object who's hashcode should be combined with the current hashcode.</param>
		/// <returns>Current hashcode combined with the hashcode of arg in a safe manner that is
		/// unlikely to result in collisions for clustered input.</returns>
		public static int CombineHashCodes<T>(this int hashCode, T arg)
		{
			unchecked
			{
				return 486187739 * hashCode + arg.GetHashCode();
			}
		}

		/// <summary>
		/// Gets a prime number that can be used as a starting point when combining hashcodes,
		/// i.e. do Hashing.StartingPrime.CombineHashCodes(field1).CombineHashCodes(field2)...
		/// </summary>
		public static int StartingPrime => 31;
	}
}
