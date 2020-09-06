using FluentAssertions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpStandardSamples.Tests.Regexs
{
    // https://docs.microsoft.com/ja-jp/dotnet/standard/base-types/regular-expression-options
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.text.regularexpressions.regexoptions
    public class RegexOption
    {
        [Fact]
        public void IgnoreCase()
        {
            var reg0 = new Regex(@"aBc");
            reg0.IsMatch("aBc").Should().BeTrue();
            reg0.IsMatch("abc").Should().BeFalse();
            reg0.IsMatch("ABC").Should().BeFalse();

            var reg1 = new Regex(@"aBc", RegexOptions.IgnoreCase);
            reg1.IsMatch("aBc").Should().BeTrue();
            reg1.IsMatch("abc").Should().BeTrue();
            reg1.IsMatch("ABC").Should().BeTrue();
            reg1.IsMatch("aaa").Should().BeFalse();
        }

        [Fact]
        public void RightToLeft()
        {
            var source = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog", "." };
            var input = string.Join(" ", source);

            var threeLengths = source.Where(t => t.Length == 3);
            var pattern = @"\b\w{3}\b";     // 3文字

            var values0 = Regex.Matches(input, pattern)
                .Cast<Match>()
                .Select(m => m.Value);
            values0.Should().BeEquivalentTo(threeLengths);

            var values1 = Regex.Matches(input, pattern, RegexOptions.RightToLeft)
                .Cast<Match>()
                .Select(m => m.Value);
            values1.Should().BeEquivalentTo(threeLengths.Reverse());
        }

        [Fact]
        public void Singleline()
        {
            // 入力文字列が単一行で構成されているかのように処理される。
            //   具体的には、ピリオド (.) 言語要素の動作を変更して、
            //   改行文字 (\n または \u000A) を除く任意の文字ではなく、
            //   改行文字を含む任意の 1 文字と一致するようにします。

            string pattern = @"^.+";
            string input = "one" + Environment.NewLine + "two";

            var values0 = Regex.Matches(input, pattern)
                .Cast<Match>()
                .Select(m => m.Value);
            values0.Should().NotBeEmpty().And.HaveCount(1);
            values0.First().Should().NotContain("two");

            var values1 = Regex.Matches(input, pattern, RegexOptions.Singleline)
                .Cast<Match>()
                .Select(m => m.Value);
            values1.Should().NotBeEmpty().And.HaveCount(1);
            values1.First().Should().Contain("two");
        }

    }
}
