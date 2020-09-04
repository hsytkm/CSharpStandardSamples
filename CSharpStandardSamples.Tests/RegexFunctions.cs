using FluentAssertions;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.text.regularexpressions.regex
    public class RegexFunctions
    {
        [Fact]
        public void Replace()
        {
            var text0 = "abc123def";
            var regex = new Regex("[0-9]+");
            regex.Replace(text0, "X").Should().Be("abcXdef");

            Regex.Replace("abc", "(a)(b)(c)", "$3$2$1")
                .Should().Be("cba");

            Regex.Replace("123abcDEF", "([0-9]+)([a-z]+)([A-Z]+)", "$3$2$1")
                .Should().Be("DEFabc123");
        }

        [Fact]
        public void Split()
        {
            var text = "abc,def.ghi!あいう、アイウ。かな「漢字」カナ，１２３．123";
            var words = Regex.Split(text, @"\p{P}");    // 句読点

            words.Should().NotBeEmpty().And.HaveCount(10);

            words[0].Should().Be("abc");
            words[3].Should().Be("あいう");
            words[^1].Should().Be("123");   // .Last()
        }

    }
}
