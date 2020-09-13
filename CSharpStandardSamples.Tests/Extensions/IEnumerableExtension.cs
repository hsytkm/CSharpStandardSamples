using CSharpStandardSamples.Core.Extensions;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Extensions
{
    public class IEnumerableExtension
    {
        [Fact]
        public void ForEach()
        {
            var source = Enumerable.Range(0, 5);
            var answer = source.Sum();

            int sum = 0;
            source.ForEach(x => sum += x);

            sum.Should().Be(answer);
        }

    }
}
