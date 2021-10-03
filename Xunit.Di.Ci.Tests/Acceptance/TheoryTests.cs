using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xunit.Di.Ci.Tests.Acceptance
{
    public class TheoryTests
    {
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(9)]
        public static void IsOdd(int number)
        {
            Assert.True(number % 2 == 1);
        }
    }
}
