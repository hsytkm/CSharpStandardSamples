using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
    public class TextFileReadWrite : IDisposable
    {
        private readonly string _tempFilePath;

        // テキストに書き出した文字列と同内容(検証用)
        private readonly IList<string> _textLines = new List<string>();

        public TextFileReadWrite()
        {
            _tempFilePath = Path.GetTempFileName();
            using var writer = new StreamWriter(_tempFilePath);

            var lines = Enumerable.Range(0, 100).Select(x => $"message{x}");
            foreach (var line in lines)
            {
                writer.WriteLine(line);
                _textLines.Add(line);
            }
        }

        [Fact]
        public void StreamReader_ReadLine()
        {
            var path = _tempFilePath;
            File.Exists(path).Should().BeTrue();

            int counter = 0;
            using var reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                line.Should().Be(_textLines[counter++]);
            }
        }

        [Fact]
        public void File_ReadLines()
        {
            var path = _tempFilePath;
            File.Exists(path).Should().BeTrue();

            int counter = 0;
            foreach (var line in File.ReadLines(path))
            {
                line.Should().Be(_textLines[counter++]);
            }
        }

        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
                File.Delete(_tempFilePath);
        }
    }
}
