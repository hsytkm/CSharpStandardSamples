using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Spans
{
    public class MyReadOnlySpan
    {
        [Fact]
        public void SliceString()
        {
            var s = "abcあいう";

            // Span<T>.Slice だとコピーが発生せずGood
            var span0 = s.AsSpan().Slice(2, 3);  // 2文字目から3文字分
            span0.ToArray().Should().BeEquivalentTo("cあい");

            Action act0 = () => s.AsSpan().Slice(100, 1);
            act0.Should().Throw<ArgumentOutOfRangeException>();
        }

    }
}
