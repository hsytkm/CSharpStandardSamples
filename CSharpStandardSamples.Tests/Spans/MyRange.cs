using FluentAssertions;
using System;
using Xunit;

namespace CSharpStandardSamples.Tests.Spans
{
    public class MyRange
    {
        [Fact]
        public void Samples()
        {
            var source = new[] { 0, 1, 2, 3, 4 };

            // 先頭 ～ 2つ目の手前
            source[0..2].Should().BeEquivalentTo(0, 1);

            // 先頭から1つ目 ～ 3つ目の手前
            source[1..3].Should().BeEquivalentTo(1, 2);

            // 先頭から1つ目 ～ 末尾の1つ手前
            source[1..^1].Should().BeEquivalentTo(1, 2, 3);

            // 先頭から1つ目 ～ 末尾
            source[1..].Should().BeEquivalentTo(1, 2, 3, 4);

            // 先頭から1つ目 ～ 末尾
            source[..^1].Should().BeEquivalentTo(0, 1, 2, 3);

            // 先頭 ～ 最終(全体)
            source[..].Should().BeEquivalentTo(source);
        }

        [Fact]
        public void Means()
        {
            var source = new[] { 0, 1, 2, 3, 4 };

            // Reversal
            Func<int[]> func0 = () => source[2..1];
            func0.Should().Throw<ArgumentOutOfRangeException>();

            // Head is over size
            Func<int[]> func1 = () => source[999..];
            func1.Should().Throw<ArgumentOutOfRangeException>();

            // Tail is over size
            Func<int[]> func2 = () => source[..999];
            func2.Should().Throw<ArgumentOutOfRangeException>();

            // Minus
            Func<int[]> func3 = () => source[-1..];
            func3.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Anothers()
        {
            var source = new[] { 0, 1, 2, 3, 4 };

            var r0 = new Range(1, 2);
            source[r0].Should().BeEquivalentTo(source[1..2]);

            var r1 = new Range(0, new Index(3, fromEnd: true));
            source[r1].Should().BeEquivalentTo(source[0..^3]);

            var r2 = Range.StartAt(2);
            source[r2].Should().BeEquivalentTo(source[2..]);

            var r3 = Range.EndAt(3);
            source[r3].Should().BeEquivalentTo(source[..3]);

            var r4 = Range.All;
            source[r4].Should().BeEquivalentTo(source[..]);
        }

    }
}
