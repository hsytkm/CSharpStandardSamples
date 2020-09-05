using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    // https://docs.microsoft.com/ja-jp/dotnet/api/system.text.regularexpressions.regex
    public class RegexMatches
    {
        class CheckInLog
        {
            public int Revision { get; }
            public string Title { get; }
            public CheckInLog(IList<string> texts)
            {
                if (texts.Count < 2) throw new ArgumentOutOfRangeException();
                Title = texts[0].Replace(Environment.NewLine, " ");
                Revision = Convert.ToInt32(texts[1]);
            }
        }

        private readonly string _sourceItemsText =
            @"Title: Standard
Revision: 101

Title:NotIncludeSpace
Revision:102

// muda mozi

Title:

InsertNewLine

Revision:
103

Title:    
HeadSpaceAndNewLine
Revision:    
104

Titleeee: NotHit -> Skip
Revisionnnn: 0



Title: AfterMultiNewLines
Revision: 105
Title: AfterNoneNewLine
Revision: 106

Title:Between
NewLine
Revision:107

Title:T
a
t
e
Revision:108";

        private readonly int _validItemCount = 8;
        private readonly MatchCollection _matches;

        public RegexMatches()
        {
            _matches = Regex.Matches(_sourceItemsText,
                @"Title:\s*(?<title>.+?)\r\s+Revision:\s*(?<rev>[0-9]+)",
                RegexOptions.Singleline);   // Singleline: ピリオドに改行も含めて判定
        }

        [Fact]
        public void Success()
        {
            _matches.Should().NotBeNull();
            _matches.Should().NotBeEmpty();
            _matches.Select(m => m.Success).Should().AllBeEquivalentTo(true);
            _matches.Should().NotBeEmpty().And.HaveCount(_validItemCount);
        }

        [Fact]
        public void Values()
        {
            var matches = _matches;
            matches.Should().NotBeEmpty();

            var checkInLogs = new List<CheckInLog>();
            foreach (Match match in matches)
            {
                var logTexts = new List<string>();

                // Groups[0]にはヒットした原文が丸ごと入ってる
                foreach (var group in match.Groups.Cast<Group>().Skip(1))
                {
                    logTexts.Add(group.Value);
                }
                checkInLogs.Add(new CheckInLog(logTexts));
            }
            checkInLogs.Should().NotBeEmpty().And.HaveCount(matches.Count);

            // test
            for (var i = 0; i < checkInLogs.Count; ++i)
            {
                var log = checkInLogs[i];
                log.Revision.Should().Be(101 + i);
                log.Title.Should().NotBeEmpty();
                log.Title.Should().NotContain(Environment.NewLine);
            }
        }

    }
}
