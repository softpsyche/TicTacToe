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
        /// Returns a random item from the given list using the provided rng
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static T RandomFromListOrDefault<T>(this IEnumerable<T> items, IRandom rng)
        {
            if (items?.Any() == false)
            {
                return default(T);
            }

            return items.ToList()[rng.Next(items.Count())];
        }
    }
}
