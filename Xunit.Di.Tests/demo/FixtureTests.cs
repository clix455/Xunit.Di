using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Xunit.Di.Tests
{
    public class FixtureTests : IClassFixture<TextReaderFixture>
    {
        private readonly TextReaderFixture _textReader;

        public FixtureTests(TextReaderFixture textReader)
        {
            _textReader = textReader;
        }

        [Fact]
        public async Task Fixture_Instantiated_and_CanRead()
        {
            var value = await _textReader.Reader.ReadToEndAsync();
            Assert.NotNull(value);
            Assert.NotEmpty(value);
        }
    }

    public class TextReaderFixture : IDisposable
    {
        public TextReader Reader { get; }

        public TextReaderFixture()
        {
            Reader =  new StringReader(nameof(TextReaderFixture));
        }

         public void Dispose()
         {
             Reader.Dispose();
         }
    }
}
