using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Data
{
    internal interface IFileAccess
    {
        bool Exists(string path);

        void Delete(string filePath);

        void SerializeBinary<T>(T obj, string filePath);

        T DeserializeBinary<T>(string filePath);
    }
}
