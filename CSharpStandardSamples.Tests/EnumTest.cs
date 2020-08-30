using CSharpStandardSamples.Core.Enums;
using FluentAssertions;
using System;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    public class EnumTest
    {
        [Fact]
        public void TryParse()
        {
            var hero = JoJoHero.Giorno;
            var obj0 = hero as object;
            var result0 = EnumExtension.TryParse<JoJoHero>(obj0, out var convert0);
            result0.Should().BeTrue();
            convert0.Should().Be(hero);

            // Parse other type
            var story = JoJoStory.StardustCrusaders;
            var obj1 = story as object;
            var result1 = EnumExtension.TryParse<JoJoHero>(obj1, out var _);
            result1.Should().BeFalse();
        }

        [Fact]
        public void TryGetEnumIndex()
        {
            var hero = JoJoHero.Giorno;
            var obj0 = hero as object;
            var result0 = EnumExtension.TryGetEnumIndex(obj0, out var index0);
            result0.Should().BeTrue();
            index0.Should().Be((int)hero);
        }
        
    }
}
