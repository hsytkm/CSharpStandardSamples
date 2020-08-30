using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace CSharpStandardSamples.Tests
{
    #region LayoutAuto
    public partial class StructSizeOf
    {
        // Default layout is 'Auto'
        readonly struct StructNoOption
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Auto, Pack = 8)]
        readonly struct AutoPack8
        {
            public readonly byte A;
            public readonly long B;
            public readonly byte C;
        }

        [Fact]
        public void Auto()
        {
            Marshal.SizeOf<StructNoOption>().Should().Be(4);
            //Marshal.SizeOf<AutoPack8>().Should().Be(8); // cannot be marshaled as an unmanaged structure
        }
    }
    #endregion

    #region Sequential / Size
    public partial class StructSizeOf
    {
        [StructLayout(LayoutKind.Sequential, Size = 4)]
        readonly struct SequentialSize4
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Sequential, Size = 5)]
        readonly struct SequentialSize5
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Sequential, Size = 6)]
        readonly struct SequentialSize6But8
        {
            public readonly Int32 data0;
            public readonly Int32 data1;
        }

        [Fact]
        public void SequentialSize()
        {
            Marshal.SizeOf<SequentialSize4>().Should().Be(4);
            Marshal.SizeOf<SequentialSize5>().Should().Be(5);
            Marshal.SizeOf<SequentialSize6But8>().Should().Be(8);
        }
    }
    #endregion

    #region Sequential / Pack
    public partial class StructSizeOf
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        readonly struct SequentialPack4Size8
        {
            public readonly byte data0;
            public readonly Int32 data1;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        readonly struct SequentialPack8ButSize4
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 16)]
        readonly struct SequentialPack16ButSize4
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 16, Size = 32)]
        readonly struct SequentialPack16Size32
        {
            public readonly Int32 data0;
            public readonly Int32 data1;
        }

        [Fact]
        public void SequentialPack()
        {
            Marshal.SizeOf<SequentialPack4Size8>().Should().Be(8);
            Marshal.SizeOf<SequentialPack8ButSize4>().Should().Be(4);
            Marshal.SizeOf<SequentialPack16ButSize4>().Should().Be(4);
            Marshal.SizeOf<SequentialPack16Size32>().Should().Be(32);
        }
    }
    #endregion

    #region Explicit
    public partial class StructSizeOf
    {
        // ダメな例：bool に 0,1 以外の値が入る
        [StructLayout(LayoutKind.Explicit)]
        struct ExplicitSize1
        {
            [FieldOffset(0)] public bool Bool;
            [FieldOffset(0)] public byte Byte;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct ExplicitSize4
        {
            [FieldOffset(0)] public Int32 data0;

            [FieldOffset(0)] public byte byte0;
            [FieldOffset(1)] public byte byte1;
            [FieldOffset(2)] public byte byte2;
            [FieldOffset(3)] public byte byte3;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct ExplicitSize16
        {
            [FieldOffset(1)] public byte A;
            [FieldOffset(4)] public long B;
            [FieldOffset(15)] public byte C;
        }

        [Fact]
        public void Explicit()
        {
            sizeof(bool).Should().Be(1);
            sizeof(byte).Should().Be(1);
            //Marshal.SizeOf<ExplicitSize1>().Should().Be(1);
            Marshal.SizeOf<ExplicitSize1>().Should().Be(4);  // Why not one?

            Marshal.SizeOf<ExplicitSize4>().Should().Be(4);
            Marshal.SizeOf<ExplicitSize16>().Should().Be(16);
        }

    }
    #endregion

}
