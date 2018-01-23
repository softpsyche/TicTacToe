﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> items,Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
		{
			HashSet<T> set = new HashSet<T>();

			foreach (var item in items)
			{
				set.Add(item);
			}

			return set;
		}
	}
	
	
}
