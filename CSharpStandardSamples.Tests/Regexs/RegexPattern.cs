using FluentAssertions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpStandardSamples.Tests.Regexs
{
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.text.regularexpressions.regex
    public class RegexPattern
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
            // 任意文字が0回以上
            var reg0 = new Regex(@"back.*");
            reg0.IsMatch("backup").Should().BeTrue();
            reg0.IsMatch("back").Should().BeTrue();

            // 任意文字が1回以上
            var reg1 = new Regex(@"back.+");
            reg1.IsMatch("background").Should().BeTrue();
            reg1.IsMatch("back").Should().BeFalse();

            // 任意文字が0か1回
            var reg2 = new Regex(@"-?\d+");
            reg2.IsMatch("123").Should().BeTrue();
            reg2.IsMatch("-123").Should().BeTrue();
            reg2.IsMatch("-a").Should().BeFalse();
        }

        [Fact]
        public void RepeatN()
        {
            // 直前文字の桁数の指定
            var reg0 = new Regex(@"\d{3}");
            reg0.IsMatch("12").Should().BeFalse();
            reg0.IsMatch("123").Should().BeTrue();

            reg0.IsMatch("1234").Should().BeTrue();     // ヒットしなさそうでする
            reg0.Matches("1234").Should().NotBeEmpty().And.HaveCount(1);
            reg0.Matches("1234").First().Value.Should().Be("123");

            // 直前文字の最小桁数の指定
            var reg1 = new Regex(@"\d{3,}");
            reg1.IsMatch("12").Should().BeFalse();
            reg1.IsMatch("123").Should().BeTrue();

            reg1.IsMatch("1234").Should().BeTrue();     // ヒットしなさそうでする
            reg1.Matches("1234").Should().NotBeEmpty().And.HaveCount(1);
            reg1.Matches("1234").First().Value.Should().Be("1234");
        }

        [Fact]
        public void RepeatNM()
        {
            // 直前文字の最小桁数と最大桁数を指定(最長一致)
            var reg2 = new Regex(@"\d{3,5}");
            reg2.IsMatch("12").Should().BeFalse();
            reg2.IsMatch("123").Should().BeTrue();
            reg2.IsMatch("12345").Should().BeTrue();

            reg2.IsMatch("123456").Should().BeTrue();   // ヒットしなさそうでする
            reg2.Matches("123456").Should().NotBeEmpty().And.HaveCount(1);
            reg2.Matches("123456").First().Value.Should().Be("12345");

            // 直前文字の最小桁数と最大桁数を指定(最短一致)
            var reg3 = new Regex(@"\d{3,5}?");
            reg3.IsMatch("12").Should().BeFalse();
            reg3.IsMatch("123").Should().BeTrue();
            reg3.IsMatch("12345").Should().BeTrue();

            reg3.IsMatch("123456").Should().BeTrue();   // ヒットしなさそうでする
            reg3.Matches("123456").Should().NotBeEmpty().And.HaveCount(2);
            reg3.Matches("123456").Select(x => x.Value).Should().ContainEquivalentOf("123", "456");
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
            reg0.IsMatch("\t").Should().BeTrue();
            reg0.IsMatch(Environment.NewLine).Should().BeTrue();
            reg0.IsMatch("\n").Should().BeTrue();
            reg0.IsMatch("\r").Should().BeTrue();
            reg0.IsMatch("\r\n").Should().BeTrue();
            reg0.IsMatch("abcd").Should().BeFalse();

            // Not space
            var reg1 = new Regex(@"\S+");
            reg1.IsMatch("X").Should().BeTrue();
            reg1.IsMatch("  　").Should().BeFalse();
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

            // Not number
            var reg1 = new Regex(@"令和\D+年");
            reg1.IsMatch("令和元年").Should().BeTrue();
            reg1.IsMatch("令和2年").Should().BeFalse();
            reg1.IsMatch("令和２年").Should().BeFalse();
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
        public void Kutouten()
        {
            var reg0 = new Regex(@"\p{P}");     // 1文字の句読点

            var words = new[]
            {
                ",", ".", "!", "、", "。", "「", "」", "，", "．", "『", "【",
            };

            foreach (var p in words)
            {
                reg0.IsMatch(p).Should().BeTrue();
            }
        }

        [Fact]
        public void Length()
        {
            var text = "The quick brown fox jumps over the lazy dog.";

            var matches = Regex.Matches(text, @"\b\w{3}\b");    // 3文字
            var words = matches.OfType<Match>().Select(m => m.Value).ToArray();

            words[0].Should().Be("The");
            words[1].Should().Be("fox");
            words[2].Should().Be("the");
            words[3].Should().Be("dog");
        }

    }
}
