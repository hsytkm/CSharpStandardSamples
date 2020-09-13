using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Exceptions
{
    /*  例外処理 https://ufcpp.net/study/csharp/oo_exception.html
     *  標準で用意されている例外クラス
     */
    public class ArgumentExceptions
    {
        [Fact]
        public void ArgumentException()
        {
            static string M(string s)
            {
                throw new ArgumentException();
            }
            Action act0 = () => M(null);

            act0.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ArgumentNullException()
        {
            static string M(string s)
            {
                if (s is null) throw new ArgumentNullException();
                return s;
            }
            Action act0 = () => M(null);
            Action act1 = () => M("");

            act0.Should().Throw<ArgumentNullException>();
            act1.Should().NotThrow<ArgumentNullException>();
        }

        [Fact]
        public void ArgumentOutOfRangeException()
        {
            static byte M(int i)
            {
                if (i > byte.MaxValue) throw new ArgumentOutOfRangeException();
                return (byte)i;
            }
            Action act0 = () => M(999);
            Action act1 = () => M(255);

            act0.Should().Throw<ArgumentOutOfRangeException>();
            act1.Should().NotThrow<ArgumentOutOfRangeException>();
        }

    }
}
