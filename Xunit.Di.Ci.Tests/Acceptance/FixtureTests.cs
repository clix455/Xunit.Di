using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Xunit.Di.Ci.Tests.Acceptance
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
            Assert.Equal(TextReaderFixture.Message, value); 
         } 
    }

    [CollectionDefinition(TextReaderFixture.DefaultCollection)]
    public class CollectionFixture : ICollectionFixture<TextReaderFixture>{}
    
    [Collection(TextReaderFixture.DefaultCollection)]
    public class CollectionMemberA
    {
        private readonly TextReaderFixture _textReader;

        public CollectionMemberA(TextReaderFixture textReader)
        {
            _textReader = textReader;
        }

        [Fact]
        public void Fixture_Instantiated_and_CanRead()
        {
            int peeked = _textReader.Reader.Peek();
            Assert.Equal((int)TextReaderFixture.Message[0], peeked);
        }
    }   
    
    [Collection(TextReaderFixture.DefaultCollection)]
    public class CollectionMemberB
    {
        private readonly TextReaderFixture _textReader;

        public CollectionMemberB(TextReaderFixture textReader)
        {
            _textReader = textReader;
        }

        [Fact]
        public void Fixture_Instantiated_and_CanRead()
        {
            int peeked = _textReader.Reader.Peek();
            Assert.Equal((int)TextReaderFixture.Message[0], peeked);
        }
    }

    public class TextReaderFixture : IDisposable
    {
        internal const string Message = "Readme";
        internal const string DefaultCollection = "DefaultCollection";
        public TextReader Reader { get; }

        public TextReaderFixture()
        {
            Reader =  new StringReader(Message);
        }

         public void Dispose()
         {
             Reader.Dispose();
         }
    }
}
