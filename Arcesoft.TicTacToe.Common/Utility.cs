using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Arcesoft.TicTacToe
{
	public static class Utility
	{
		public static void Serialize<T>(T serializableObject,string filePath)
		{
			IFormatter iFormatter = new BinaryFormatter();

			using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				iFormatter.Serialize(stream, serializableObject);
			}
		}
		public static T Deserialize<T>(string filePath)
		{
			IFormatter iFormatter = new BinaryFormatter();

			using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return (T)iFormatter.Deserialize(stream);
			}
		}

        public static T ToEnumeration<T>(this object obj)
        {
            return (T)(Enum.Parse(typeof(T), obj.ToString()));
        }
	}
}
