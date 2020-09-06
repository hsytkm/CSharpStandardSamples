using FluentAssertions;
using System;
using Xunit;
using MyConvert = CSharpStandardSamples.Core.Systems.ConvertExtension;

namespace CSharpStandardSamples.Tests.Systems
{
    public class ConvertStringDec
    {
        [Fact]
        public void Ok()
        {
            MyConvert.GetValue<int>("0").Should().Be(0);
            MyConvert.GetValue<int>("123").Should().Be(123);
            MyConvert.GetValue<int>("-123").Should().Be(-123);
            MyConvert.GetValue<int>("-0").Should().Be(0);
        }

        [Fact]
        public void FormatException()
        {
            // Exception発生しなければテストNGになる
            //Func<int> funcOk = () => MyConvert.GetValue<int>("0");
            //funcOk.Should().Throw<FormatException>();

            Func<int> func0 = () => MyConvert.GetValue<int>("１２３");
            func0.Should().Throw<FormatException>();

            Func<int> func1 = () => MyConvert.GetValue<int>("－123");
            func1.Should().Throw<FormatException>();

            Func<int> func2 = () => MyConvert.GetValue<int>("ff");
            func2.Should().Throw<FormatException>();
        }

        [Fact]
        public void GetValueMinMax()
        {
            MyConvert.GetValue<sbyte>(sbyte.MinValue.ToString()).Should().Be(sbyte.MinValue);
            MyConvert.GetValue<sbyte>(sbyte.MaxValue.ToString()).Should().Be(sbyte.MaxValue);
            MyConvert.GetValue<byte>(byte.MinValue.ToString()).Should().Be(byte.MinValue);
            MyConvert.GetValue<byte>(byte.MaxValue.ToString()).Should().Be(byte.MaxValue);

            MyConvert.GetValue<short>(short.MinValue.ToString()).Should().Be(short.MinValue);
            MyConvert.GetValue<short>(short.MaxValue.ToString()).Should().Be(short.MaxValue);
            MyConvert.GetValue<int>(int.MinValue.ToString()).Should().Be(int.MinValue);
            MyConvert.GetValue<int>(int.MaxValue.ToString()).Should().Be(int.MaxValue);
            MyConvert.GetValue<long>(long.MinValue.ToString()).Should().Be(long.MinValue);
            MyConvert.GetValue<long>(long.MaxValue.ToString()).Should().Be(long.MaxValue);

            MyConvert.GetValue<ushort>(ushort.MinValue.ToString()).Should().Be(ushort.MinValue);
            MyConvert.GetValue<ushort>(ushort.MaxValue.ToString()).Should().Be(ushort.MaxValue);
            MyConvert.GetValue<uint>(uint.MinValue.ToString()).Should().Be(uint.MinValue);
            MyConvert.GetValue<uint>(uint.MaxValue.ToString()).Should().Be(uint.MaxValue);
            MyConvert.GetValue<ulong>(ulong.MinValue.ToString()).Should().Be(ulong.MinValue);
            MyConvert.GetValue<ulong>(ulong.MaxValue.ToString()).Should().Be(ulong.MaxValue);

            MyConvert.GetValue<float>(float.MinValue.ToString()).Should().Be(float.MinValue);
            MyConvert.GetValue<float>(float.MaxValue.ToString()).Should().Be(float.MaxValue);
            MyConvert.GetValue<double>(double.MinValue.ToString()).Should().Be(double.MinValue);
            MyConvert.GetValue<double>(double.MaxValue.ToString()).Should().Be(double.MaxValue);
        }

        [Fact]
        public void OverflowExceptionSigned()
        {
            // Exception発生しなければテストNGになる
            //Func<int> funcOk = () => MyConvert.GetValue<int>("0");
            //funcOk.Should().Throw<OverflowException>();

            Func<short> func0 = () => MyConvert.GetValue<short>(int.MinValue.ToString());
            func0.Should().Throw<OverflowException>();

            Func<short> func1 = () => MyConvert.GetValue<short>(int.MaxValue.ToString());
            func1.Should().Throw<OverflowException>();

            Func<int> func2 = () => MyConvert.GetValue<int>(long.MinValue.ToString());
            func2.Should().Throw<OverflowException>();

            Func<int> func3 = () => MyConvert.GetValue<int>(long.MaxValue.ToString());
            func3.Should().Throw<OverflowException>();
        }

        [Fact]
        public void OverflowExceptionUnsigned()
        {
            Func<ushort> func0 = () => MyConvert.GetValue<ushort>("-1");
            func0.Should().Throw<OverflowException>();

            Func<ushort> func1 = () => MyConvert.GetValue<ushort>(uint.MaxValue.ToString());
            func1.Should().Throw<OverflowException>();

            Func<uint> func2 = () => MyConvert.GetValue<uint>("-1");
            func2.Should().Throw<OverflowException>();

            Func<uint> func3 = () => MyConvert.GetValue<uint>(ulong.MaxValue.ToString());
            func3.Should().Throw<OverflowException>();
        }

    }
}
