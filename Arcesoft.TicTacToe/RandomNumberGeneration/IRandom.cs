using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.RandomNumberGeneration
{
	internal interface IRandom
	{
		/// <summary>Returns a nonnegative random number less than the specified maximum.</summary>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
		/// <returns>
		///     A 32-bit signed integer greater than or equal to zero, and less than maxValue;
		///     that is, the range of return values ordinarily includes zero but not maxValue.
		///     However, if maxValue equals zero, maxValue is returned.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		Int32 Next(Int32 maxValue);

		/// <summary>Returns a nonnegative random number less than the specified maximum.</summary>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero.</param>
		/// <param name="maxValue">The inclusive lower bound of the random number returned.</param>
		/// <returns>
		///     A 32-bit signed integer greater than or equal to zero, and less than maxValue;
		///     that is, the range of return values ordinarily includes zero but not maxValue.
		///     However, if maxValue equals zero, maxValue is returned.
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		Int32 Next(Int32 minValue, Int32 maxValue);

		Double NextDouble();
	}
}
