using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.RandomNumberGeneration
{
    internal static class IRandomExtensions
    {
        /// <summary>
        /// Returns a random integer from the list supplied
        /// </summary>
        /// <param name="rng">A random number generator</param>
        /// <param name="values">A list of values to consider</param>
        /// <returns>A value from the list supplied</returns>
        public static Int32 Next(this IRandom rng, params Int32[] values)
        {
            return values[rng.Next(0, values.Length)];
        }

        /// <summary>
        /// Returns a distinct unique random set of numbers between the min and max value. The numbers returned are derived randomnly but will not repeat.
        /// The distinct set of numbers will NOT exceed the maxcount
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="minRange"></param>
        /// <param name="maxRange"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        public static List<Int32> NextDistinctSet(this IRandom rng, Int32 minRange, Int32 maxRange, Int32 maxCount)
        {
            if (maxRange - minRange < maxCount)
            {
                //throw new ArgumentException("Invalid totalUnique passed in");
                maxCount = maxRange - minRange;
            }

            List<Int32> valuesList = new List<int>();

            for (Int32 i = 0; i < maxCount; i++)
            {
                valuesList.Add(rng.Next(minRange, minRange));
            }

            while (valuesList.Distinct().Count() != maxCount)
            {
                valuesList.Add(rng.Next(minRange, maxRange));
            }

            return valuesList.Distinct().ToList();
        }

        public static T RandomFromList<T>(this IEnumerable<T> items, IRandom rng)
        {
            if (items?.Any() == false)
            {
                return default(T);
            }

            return items.ToList()[rng.Next(items.Count())];
        }
    }
}
