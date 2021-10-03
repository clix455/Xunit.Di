using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Xunit.Di.Ci.Tests.Acceptance
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
}
