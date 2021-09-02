using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Xunit.Di.Ci.Tests
{
    public class ServiceDependencyTests
    {
        private readonly TextReaderService _textReader;

        public ServiceDependencyTests(TextReaderService textReader)
        {
            this._textReader = textReader;
        }

        [Fact]
        public async Task Dependency_Instantiated_and_CanRead()
        {
            var value = await _textReader.Reader.ReadToEndAsync();
            Assert.NotNull(value);
            Assert.NotEmpty(value);
        }
    }

    public class TextReaderService : IDisposable
    {
        private bool disposedValue;

        public TextReader Reader { get; }

        public TextReaderService()
        {
            Reader = new StringReader(nameof(TextReaderService));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Reader.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FakeService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
