using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Exceptions
{
    /*  例外処理 https://ufcpp.net/study/csharp/oo_exception.html
     *  標準で用意されている例外クラス
     */
    public class SystemExceptions
    {
        [Fact]
        public void InvalidCastException()
        {
            // "This conversion is not supported. No value is returned."
            Action act0 = () => Convert.ToChar(true);
            act0.Should().Throw<InvalidCastException>();
        }

        [Fact]
        public void IndexOutOfRangeException()
        {
            var bytes = new byte[5];
            Action act0 = () => Console.WriteLine(bytes[999]);
            act0.Should().Throw<IndexOutOfRangeException>();
        }

    }
}
