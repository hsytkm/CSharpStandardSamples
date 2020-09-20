using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

namespace CSharpStandardSamples.Tests.Attributes
{
    /// <summary>作者情報を残すための属性</summary>
    [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    class StructSizeAttribute : Attribute
    {
        private int Length { get; }
        private int Size { get; }
        public StructSizeAttribute(int length, int size) => (Length, Size) = (length, size);

        public static (int Length, int Size)? GetLengthSize(MemberInfo info)
        {
            var attribute = GetCustomAttributes(info, typeof(StructSizeAttribute))
                .OfType<StructSizeAttribute>()
                .FirstOrDefault();
            return (attribute is null) ? default : (attribute.Length, attribute.Size);
        }
        public static int? GetLength(MemberInfo info) => GetLengthSize(info)?.Length ?? null;
        public static int? GetSize(MemberInfo info) => GetLengthSize(info)?.Size ?? null;
    }

    public class Attribute2
    {
#pragma warning disable 0649
        [StructSize(4, 4 * sizeof(Int16))]
        private struct Short4
        {
            public short x0, x1, x2, x3;
        }

        [StructSize(8, 8 * sizeof(Int32))]
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        private struct Int8
        {
            public int x0, x1, x2;    // 実サイズはテキトー
        }
#pragma warning restore 0649

        [Fact]
        public void GetStructSizeAttribute()
        {
            var type0 = typeof(Short4);
            StructSizeAttribute.GetLength(type0).Should().Be(4);
            StructSizeAttribute.GetSize(type0).Should().Be(4 * sizeof(Int16));

            var type1 = typeof(Int8);
            StructSizeAttribute.GetLength(type1).Should().Be(8);
            StructSizeAttribute.GetSize(type1).Should().Be(8 * sizeof(Int32));
        }

    }
}
