using FluentAssertions;
using System.Linq;
using Xunit;
using MyEnum = CSharpStandardSamples.Core.Systems.EnumExtension;

namespace CSharpStandardSamples.Tests.Systems
{
    enum JoJoHero
    {
        Jonathan = 1,
        Joseph,
        Jotaro,
        Josuke,
        Giorno,
        Jolyne,
        Johnny
    };

    enum JoJoStory
    {
        PhantomBlood = 1,
        BattleTendency,
        StardustCrusaders,
        DiamondIsUnbreakable,
        GoldenWind,
        StoneOcean,
        SteelBallRun,
        JoJolion
    };

    public class EnumTest
    {
        [Fact]
        public void TryParse()
        {
            var hero = JoJoHero.Jonathan;
            var obj0 = hero as object;

            var result0 = MyEnum.TryParse<JoJoHero>(obj0, out var convert0);

            result0.Should().BeTrue();
            convert0.Should().Be(hero);

            // Parse other type
            var story = JoJoStory.StardustCrusaders;
            var obj1 = story as object;

            var result1 = MyEnum.TryParse<JoJoHero>(obj1, out var _);

            result1.Should().BeFalse();
        }

        [Fact]
        public void TryGetEnumIndex()
        {
            var hero = JoJoHero.Joseph;
            var obj0 = hero as object;

            var result0 = MyEnum.TryGetEnumIndex(obj0, out var index0);

            result0.Should().BeTrue();
            index0.Should().Be((int)hero);
        }

        [Fact]
        public void GetValues()
        {
            var heros = MyEnum.GetValues<JoJoHero>();
            heros.Count().Should().Be(7);

            heros.Count(e => !(e.ToString().Contains("Jo"))).Should().Be(1);    // 1=Giorno
        }

    }
}
