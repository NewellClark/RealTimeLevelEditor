using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LevelModelTests
{
	public class SizeTests
	{
		[Fact]
		internal void SizeSizeMultiplication_Works()
		{
			Size lhs = new Size(55, 92);
			Size rhs = new Size(12, 148);

			Size product = new Size(
				lhs.X * rhs.X,
				lhs.Y * rhs.Y);

			Assert.True(product == lhs * rhs);
		}

		[Fact]
		internal void SizeScalarMultiplication_Works()
		{
			Size size = new Size(123, 667);
			long scalar = 178;

			Size product = new Size(
				size.X * scalar,
				size.Y * scalar);

			Assert.True(product == size * scalar);
		}

		[Fact]
		internal void SizeScalarMultiplication_Associative()
		{
			Size size = new Size(0897, 1203987L);
			long scalar = 0189374;

			Assert.True(size * scalar == scalar * size);
		}
	}
}
