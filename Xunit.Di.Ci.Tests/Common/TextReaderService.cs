using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Xunit.Di.Ci.Tests
{
    public class TextReaderService : IDisposable
    {
        private readonly ILogger<TextReaderService> _logger;
        private bool disposedValue;
        private readonly TextReader _reader;

        public TextReader Reader
        {
            get
            {
                _logger.LogInformation("Get reader from text reader service.");
                return _reader;
            }
        }

        public TextReaderService(ILogger<TextReaderService> logger)
        {
            _logger = logger;
            _reader = new StringReader(nameof(TextReaderService));
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
