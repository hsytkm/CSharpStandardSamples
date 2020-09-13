using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Exceptions
{
    /*  例外処理 https://ufcpp.net/study/csharp/oo_exception.html
     *  標準で用意されている例外クラス
     */
    public class ArithmeticExceptions
    {
        [Fact]
        public void OverflowException()
        {
            byte b = 0;
            Action act0 = () =>
            {
                checked
                {
                    for (var i = 0; i < 999; ++i)
                        b++;
                }
            };
            act0.Should().Throw<OverflowException>();
        }

        [Fact]
        public void DivideByZeroException()
        {
            int x0 = 0;
            Action act0 = () => Console.WriteLine(1 / x0);
            act0.Should().Throw<DivideByZeroException>();
        }


    }
}
