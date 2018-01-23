using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Arcesoft.TicTacToe
{
	public static class ObjectExtensions
	{
        public static T ToEnumeration<T>(this object obj)
        {
            return (T)(Enum.Parse(typeof(T), obj.ToString()));
        }
	}
}
