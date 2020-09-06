using CSharpStandardSamples.Core.Structs;
using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace CSharpStandardSamples.Tests.Structs
{
    public class StructTryParse
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 4)]
        readonly struct SingleFieldStruct
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
        readonly struct UnionStruct
        {
            [FieldOffset(0)] public readonly Int32 data32;

            [FieldOffset(0)] public readonly Int16 data16_0;
            [FieldOffset(2)] public readonly Int16 data16_1;

            [FieldOffset(0)] public readonly byte data8_0;
            [FieldOffset(1)] public readonly byte data8_1;
            [FieldOffset(2)] public readonly byte data8_2;
            [FieldOffset(3)] public readonly byte data8_3;
        }

        [Fact]
        public void Simple()
        {
            var source = 0x_1234_5678;
            var bs = BitConverter.GetBytes(source);

            var result0 = StructExtension.TryParse<SingleFieldStruct>(bs, out var struct0);
            result0.Should().BeTrue();
            struct0.data.Should().Be(source);
        }

        [Fact]
        public void CheckSize()
        {
            var source = 0x_1234_5678;
            var bs = BitConverter.GetBytes(source);

            // byte array is just
            var result0 = StructExtension.TryParse<SingleFieldStruct>(bs, out var struct0);
            result0.Should().BeTrue();
            struct0.data.Should().Be(source);

            // byte array is short
            var shortBytes = BitConverter.GetBytes((short)123);
            shortBytes.Should().HaveCount(sizeof(short));
            var result1 = StructExtension.TryParse<SingleFieldStruct>(shortBytes, out var _);
            result1.Should().BeFalse();

            // byte array is long
            var longBytes = BitConverter.GetBytes(123L);
            longBytes.Should().HaveCount(sizeof(long));
            var result2 = StructExtension.TryParse<SingleFieldStruct>(longBytes, out var _);
            result2.Should().BeFalse();
        }

        [Fact]
        public void Union()
        {
            var source = 0x_1234_5678;
            var bs = BitConverter.GetBytes(source);

            var result = StructExtension.TryParse<UnionStruct>(bs, out var s);
            result.Should().BeTrue();

            s.data32.Should().Be(source);

            var short0 = (short)(source & 0xffff);
            var short1 = (short)((source >> 16) & 0xffff);
            s.data16_0.Should().Be(short0);
            s.data16_1.Should().Be(short1);

            var byte0 = (byte)(source & 0xff);
            var byte1 = (byte)((source >> 8) & 0xff);
            var byte2 = (byte)((source >> 16) & 0xff);
            var byte3 = (byte)((source >> 24) & 0xff);
            s.data8_0.Should().Be(byte0);
            s.data8_1.Should().Be(byte1);
            s.data8_2.Should().Be(byte2);
            s.data8_3.Should().Be(byte3);

            s.data8_0.Should().Be(bs[0]);
            s.data8_1.Should().Be(bs[1]);
            s.data8_2.Should().Be(bs[2]);
            s.data8_3.Should().Be(bs[3]);
        }

    }
}
