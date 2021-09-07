using Xunit;
using Xunit.Abstractions;

namespace Xunit.Di.Ci.Tests
{
    public class OutputHelperTests
    {
        private readonly ITestOutputHelper output;

        public OutputHelperTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Output_WriteTo_TestOutput()
        {
            var temp = "my class!";
            output.WriteLine("This is output from {0}", temp);
        }
    }
}
