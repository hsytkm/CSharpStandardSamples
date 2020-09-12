using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Linqs
{
    // http://pro.art55.jp/?eid=1303948
    public class LinqCastOfType
    {
        [Fact]
        public void Diff1()
        {
            var source = new object[] { "ABC", null, "DEF", null, "HIJ" };

            // Cast<T> できないので、Exception が発生する
            Func<int[]> func0 = () => source.Cast<int>().ToArray();
            func0.Should().Throw<InvalidCastException>();

            // OfType<T> の内部は is なので、Exception が発生しない
            source.OfType<int>().Should().BeEmpty();
        }

        [Fact]
        public void Diff2()
        {
            var source = new object[] { "ABC", null, "DEF", null, "HIJ" };

            // Cast<T> は null も Cast できているのでカウントされる
            source.Cast<string>().Should().NotBeEmpty().And.HaveCount(5);
            source.Cast<string>().Count(x => x is null).Should().Be(2);

            // OfType<T> の内部は is で、(null is T) は false となるのでカウントされない
            source.OfType<string>().Should().NotBeEmpty().And.HaveCount(3);
            source.OfType<string>().Count(x => x is null).Should().Be(0);
        }

    }
}
