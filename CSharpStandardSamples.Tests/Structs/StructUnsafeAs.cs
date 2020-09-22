using FluentAssertions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

namespace CSharpStandardSamples.Tests.Structs
{
    public class StructUnsafeAs
    {
        [StructLayout(LayoutKind.Sequential, Size = 8)]
        readonly struct Fixed8
        {
            public readonly byte FixedElementField;
        }

        // 奇数サイズは組み込み型に存在しないので定義
        [StructLayout(LayoutKind.Sequential, Size = 3)]
        readonly struct Bytes3
        {
            public readonly byte x0, x1, x2;
        }

        readonly struct ReadOnlyUInt64
        {
            public readonly UInt64 x0;
            public static implicit operator UInt64(ReadOnlyUInt64 ro) => ro.x0;
        }

        [Fact]
        public void Simple()
        {
            Marshal.SizeOf<Fixed8>().Should().Be(8);
            var fixed8 = new Fixed8();
            //fixed8.FixedElementField = 0x00;  readonly なので代入不可

            // readonly が取れているので代入可能
            ref var x64 = ref Unsafe.As<Fixed8, ulong>(ref fixed8);
            x64 = 0x_0123_4567_89ab_cdef;

            Marshal.SizeOf<ReadOnlyUInt64>().Should().Be(8);
            ref var ro64 = ref Unsafe.As<Fixed8, ReadOnlyUInt64>(ref fixed8);
            //ro64.x0 = 0;  readonly なので代入不可
            ((UInt64)ro64).Should().Be(0x_0123_4567_89ab_cdef);

            ref var bytes4_0 = ref Unsafe.As<Fixed8, byte>(ref fixed8);
            bytes4_0.Should().Be(0xef);

            ref var ushorts2_0 = ref Unsafe.As<Fixed8, ushort>(ref fixed8);
            ushorts2_0.Should().Be(0xcdef);
            ref var ushorts2_1 = ref Unsafe.Add(ref ushorts2_0, 1);
            ushorts2_1.Should().Be(0x89ab);

            Marshal.SizeOf<Bytes3>().Should().Be(3);
            ref var bytes3_0 = ref Unsafe.As<Fixed8, Bytes3>(ref fixed8);
            bytes3_0.x0.Should().Be(0xef);
            bytes3_0.x1.Should().Be(0xcd);
            bytes3_0.x2.Should().Be(0xab);

            ref var bytes3_1 = ref Unsafe.Add(ref bytes3_0, 1);
            bytes3_1.x0.Should().Be(0x89);
            bytes3_1.x1.Should().Be(0x67);
            bytes3_1.x2.Should().Be(0x45);
        }
    }
}
