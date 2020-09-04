using FluentAssertions;
using System;
using Xunit;
using MyConvert = CSharpStandardSamples.Core.Systems.ConvertExtension;

namespace CSharpStandardSamples.Tests
{
    public class ConvertStringHex
    {
        [Fact]
        public void Ok()
        {
            MyConvert.GetValueFromHex<int>("0").Should().Be(0);
            MyConvert.GetValueFromHex<int>("123").Should().Be(0x123);
            MyConvert.GetValueFromHex<int>("ff").Should().Be(0xff);
            MyConvert.GetValueFromHex<int>("FF").Should().Be(0xff);
            MyConvert.GetValueFromHex<int>("0xff").Should().Be(0xff);
            MyConvert.GetValueFromHex<int>("0XFF").Should().Be(0xff);
            MyConvert.GetValueFromHex<int>("0ff").Should().Be(0xff);
            MyConvert.GetValueFromHex<int>("0x0ff").Should().Be(0xff);
        }

        [Fact]
        public void FormatException()
        {
            // Exception発生しなければテストNGになる
            //Func<int> funcOk = () => MyConvert.GetValue<int>("0");
            //funcOk.Should().Throw<FormatException>();

            Func<int> func0 = () => MyConvert.GetValueFromHex<int>("ffh");
            func0.Should().Throw<FormatException>();

            Func<int> func1 = () => MyConvert.GetValueFromHex<int>("０");
            func1.Should().Throw<FormatException>();

            Func<int> func2 = () => MyConvert.GetValueFromHex<int>("ｆｆ");
            func2.Should().Throw<FormatException>();
        }

        [Fact]
        public void ArgumentException()
        {
            Func<int> func0 = () => MyConvert.GetValueFromHex<int>("-1");
            func0.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetValueMinMax()
        {
            MyConvert.GetValueFromHex<short>(short.MinValue.ToString("X")).Should().Be(short.MinValue);
            MyConvert.GetValueFromHex<short>(short.MaxValue.ToString("X")).Should().Be(short.MaxValue);
            MyConvert.GetValueFromHex<int>(int.MinValue.ToString("X")).Should().Be(int.MinValue);
            MyConvert.GetValueFromHex<int>(int.MaxValue.ToString("X")).Should().Be(int.MaxValue);
            MyConvert.GetValueFromHex<long>(long.MinValue.ToString("X")).Should().Be(long.MinValue);
            MyConvert.GetValueFromHex<long>(long.MaxValue.ToString("X")).Should().Be(long.MaxValue);

            MyConvert.GetValueFromHex<ushort>(ushort.MinValue.ToString("X")).Should().Be(ushort.MinValue);
            MyConvert.GetValueFromHex<ushort>(ushort.MaxValue.ToString("X")).Should().Be(ushort.MaxValue);
            MyConvert.GetValueFromHex<uint>(uint.MinValue.ToString("X")).Should().Be(uint.MinValue);
            MyConvert.GetValueFromHex<uint>(uint.MaxValue.ToString("X")).Should().Be(uint.MaxValue);
            MyConvert.GetValueFromHex<ulong>(ulong.MinValue.ToString("X")).Should().Be(ulong.MinValue);
            MyConvert.GetValueFromHex<ulong>(ulong.MaxValue.ToString("X")).Should().Be(ulong.MaxValue);
        }

        [Fact]
        public void OverflowException()
        {
            Func<short> func0 = () => MyConvert.GetValueFromHex<short>(int.MinValue.ToString("X"));
            func0.Should().Throw<OverflowException>();

            Func<short> func1 = () => MyConvert.GetValueFromHex<short>(int.MaxValue.ToString("X"));
            func1.Should().Throw<OverflowException>();

            Func<int> func2 = () => MyConvert.GetValueFromHex<int>(long.MinValue.ToString("X"));
            func2.Should().Throw<OverflowException>();

            Func<int> func3 = () => MyConvert.GetValueFromHex<int>(long.MaxValue.ToString("X"));
            func3.Should().Throw<OverflowException>();
        }

    }
}
