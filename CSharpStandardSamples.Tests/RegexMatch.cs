using CSharpStandardSamples.Core.Regexs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.text.regularexpressions.regex
    public class RegexMatch
    {
        private readonly string _sourceName = "Jotaro";
        private readonly int _sourceAge = 17;
        private readonly int _sourceId = 60000;  // 0xea60
        private string SourceText => $"Name:{_sourceName} Age={_sourceAge}   Id=0x{_sourceId:X8} ";

        private readonly Match _match;

        public RegexMatch()
        {
            _match = Regex.Match(SourceText,
                @"^Name:(?<name>.+)\s+Age=(?<age>[0-9]+)\s+Id=0x(?<id>[0-9a-fA-F]+)\s*");
        }

        [Fact]
        public void MatchSuccess()
        {
            _match.Success.Should().BeTrue();
            _match.Groups.Should().NotBeEmpty();
        }

        [Fact]
        public void GetValuesFromName()
        {
            var match = _match;
            match.Success.Should().BeTrue();

            match.Groups["name"].Value.Should().Be(_sourceName);
            Convert.ToInt32(match.Groups["age"].Value).Should().Be(_sourceAge);
            Convert.ToInt32(match.Groups["id"].Value, 16).Should().Be(_sourceId);

            // Invalid name is string.Empty.
            match.Groups["not_exist_key"].Value.Should().BeEmpty();
        }

        [Fact]
        public void GetValuesFromIndex()
        {
            var match = _match;
            match.Success.Should().BeTrue();

            match.Groups.Should().NotBeEmpty().And.HaveCount(4);

            match.Groups[0].Value.Should().Be(SourceText);
            match.Groups[1].Value.Should().Be(_sourceName);
            Convert.ToInt32(match.Groups[2].Value).Should().Be(_sourceAge);
            Convert.ToInt32(match.Groups[3].Value, 16).Should().Be(_sourceId);

            // Invalid index is string.Empty.
            match.Groups[-1].Value.Should().BeEmpty();
            match.Groups[100].Value.Should().BeEmpty();
        }

        [Fact]
        public void MatchExtension()
        {
            var match = _match;
            match.Success.Should().BeTrue();

            // すっきり書けるけど実装はダサい(ボックス化)
            match.GetValue<int>(2).Should().Be(_sourceAge);
            match.GetValue<int>("age").Should().Be(_sourceAge);
            match.GetHexValue<int>(3).Should().Be(_sourceId);
            match.GetHexValue<int>("id").Should().Be(_sourceId);
        }

        [Fact]
        public void KeyNotFound()
        {
            var match = _match;
            match.Success.Should().BeTrue();

            Func<int> func0 = () => match.GetValue<int>(-100);
            func0.Should().Throw<KeyNotFoundException>();

            Func<int> func1 = () => match.GetValue<int>("not_exist_key");
            func1.Should().Throw<KeyNotFoundException>();
        }

    }
}
