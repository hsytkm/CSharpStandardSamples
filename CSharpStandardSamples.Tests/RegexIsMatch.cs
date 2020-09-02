using FluentAssertions;
using System;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.text.regularexpressions.regex
    public class RegexIsMatch
    {
        [Fact]
        public void Or()
        {
            // OR
            var reg0 = new Regex(@"[Tt]est");
            reg0.IsMatch("Test").Should().BeTrue();
            reg0.IsMatch("XXXtestXXX").Should().BeTrue();
            reg0.IsMatch("tEST").Should().BeFalse();

            // OR
            var reg1 = new Regex(@"Button|ボタン");
            reg1.IsMatch("Button").Should().BeTrue();
            reg1.IsMatch("Buttonボタン").Should().BeTrue();
            reg1.IsMatch("ぼたん").Should().BeFalse();
        }

        [Fact]
        public void Repeat()
        {
            // 任意が0文字以上
            var reg0 = new Regex(@"back.*");
            reg0.IsMatch("backup").Should().BeTrue();
            reg0.IsMatch("back").Should().BeTrue();

            // 任意が1文字以上
            var reg1 = new Regex(@"back.+");
            reg1.IsMatch("background").Should().BeTrue();
            reg1.IsMatch("back").Should().BeFalse();
        }

        [Fact]
        public void HeadTail()
        {
            // 行頭
            var reg0 = new Regex(@"^cd");
            reg0.IsMatch("cdcase").Should().BeTrue();
            reg0.IsMatch("abcd").Should().BeFalse();

            // 行末
            var reg1 = new Regex(@"de$");
            reg1.IsMatch("code").Should().BeTrue();
            reg1.IsMatch("decoder").Should().BeFalse();
        }

        [Fact]
        public void Space()
        {
            var reg0 = new Regex(@"\s+");
            reg0.IsMatch("I do.").Should().BeTrue();
            reg0.IsMatch("全角　スペースも　ＯＫ").Should().BeTrue();
            reg0.IsMatch("abcd").Should().BeFalse();
        }

        [Fact]
        public void Range()
        {
            var reg0 = new Regex(@"[0-9]+sec");
            reg0.IsMatch("1sec").Should().BeTrue();
            reg0.IsMatch("987654321sec").Should().BeTrue();
            reg0.IsMatch("sec").Should().BeFalse();
            reg0.IsMatch("123").Should().BeFalse();

            var reg1 = new Regex(@"[a-zA-Z]+");
            reg1.IsMatch("aBcDEFghI").Should().BeTrue();
            reg1.IsMatch("A123C").Should().BeTrue();
            reg1.IsMatch("123").Should().BeFalse();
        }

        [Fact]
        public void Numeric()
        {
            var reg0 = new Regex(@"令和\d+年");
            reg0.IsMatch("令和12年").Should().BeTrue();
            reg0.IsMatch("令和３年").Should().BeTrue();     // 全角もOK
            reg0.IsMatch("令和元年").Should().BeFalse();
        }

        [Fact]
        public void Japanese()
        {
            var reg0 = new Regex(@"\p{IsHiragana}");
            reg0.IsMatch("あいうえお").Should().BeTrue();
            reg0.IsMatch("わをん").Should().BeTrue();
            reg0.IsMatch("aiueo").Should().BeFalse();

            var reg1 = new Regex(@"\p{IsKatakana}");
            reg1.IsMatch("アイウエオ").Should().BeTrue();
            reg1.IsMatch("ワヲン").Should().BeTrue();
            reg1.IsMatch("aiueo").Should().BeFalse();
        }

        [Fact]
        public void Match()
        {
            var srcName = "Jotaro";
            var srcAge = 17;
            var srcId = 60000;  // 0xea60
            var source = $"Name:{srcName} Age={srcAge}   Id=0x{srcId:X8} ";

            var reg0 = new Regex(@"^Name:(?<name>.+)\s+Age=(?<age>[0-9]+)\s+Id=0x(?<id>[0-9a-fA-F]+)\s*");

            var match = reg0.Match(source);
            match.Success.Should().BeTrue();

            if (match.Success)
            {
                var regName = match.Groups["name"].Value;
                regName.Should().Be(srcName);

                var regAge = Convert.ToInt32(match.Groups["age"].Value);
                regAge.Should().Be(srcAge);

                var regId = Convert.ToInt32(match.Groups["id"].Value, 16);
                regId.Should().Be(srcId);
            }
        }

    }
}
