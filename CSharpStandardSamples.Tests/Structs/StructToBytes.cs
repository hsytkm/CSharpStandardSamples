using CSharpStandardSamples.Core.Structs;
using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace CSharpStandardSamples.Tests.Structs
{
    public class StructToBytes
    {
        [StructLayout(LayoutKind.Sequential)]
        readonly struct MyStruct
        {
            public readonly Int32 data;
            public MyStruct(Int32 d) => data = d;
        }

        [Fact]
        public void Equal()
        {
            var value = 0x_1234_5678;
            var sourceBytes = BitConverter.GetBytes(value);

            var myStruct = new MyStruct(value);
            var convertBytes = StructExtension.GetBytes(myStruct);

            convertBytes.Should().NotBeEmpty().And.Equal(sourceBytes);
        }

    }
}
