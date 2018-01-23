using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.Data
{
    internal class FileAccess : IFileAccess
    {
        public bool Exists(string path) => File.Exists(path);

        public void Delete(string filePath) => File.Delete(filePath);

        public void Serialize<T>(T serializableObject, string filePath)
        {
            IFormatter iFormatter = new BinaryFormatter();

            using (Stream stream = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write, FileShare.None))
            {
                iFormatter.Serialize(stream, serializableObject);
            }
        }
        public T Deserialize<T>(string filePath)
        {
            IFormatter iFormatter = new BinaryFormatter();

            using (Stream stream = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read, FileShare.Read))
            {
                return (T)iFormatter.Deserialize(stream);
            }
        }
    }
}
