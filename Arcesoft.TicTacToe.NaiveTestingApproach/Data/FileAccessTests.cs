using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe.CommonTestingApproach.Data
{
    /// <summary>
    /// NOTE: these tests are questionable in so much as they are unit tests because they cross a boundary (albeit a relatively minor one)
    /// by accessing the file system. They also inherently test things we should not care to test (like that the filestream and the serializer work
    /// which we should trust to work since they are MS classes already under test somewhere else). 
    /// </summary>
    [TestClass]
    public class FileAccessTests
    {
        private TicTacToe.Data.FileAccess FileAccess { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            FileAccess = new TicTacToe.Data.FileAccess();
        }

        [TestMethod]
        public void ShouldDetermineIfFileExists()
        {
            var tempFile = Path.GetTempFileName();

            var result = FileAccess.Exists(tempFile);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldDelete()
        {
            var tempFile = Path.GetTempFileName();

            FileAccess.Delete(tempFile);

            FileAccess.Exists(tempFile).Should().BeFalse();
        }

        [TestMethod]
        public void ShouldSerializeAndDeserializeBinary()
        {
            var tempFile = Path.GetTempFileName();

            string expected = "Hello world!";

            FileAccess.SerializeBinary(expected, tempFile);

            var result = FileAccess.DeserializeBinary<string>(tempFile);

            result.Should().Be(expected);
        }
    }
}
