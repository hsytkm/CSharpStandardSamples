using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace CSharpStandardSamples.Tests.Spans
{
    public class MyStackalloc
    {
        [Fact]
        public void Rule()
        {
            // stackallocが使える型はアンマネージ型のみ
            Span<int> ok = stackalloc int[8];

            // Span<string> は OK だけど、 stackalloc string[] はビルドエラー
            //Span<string> ng = stackalloc string[8];
        }

        [Fact]
        public void NewStackalloc()
        {
            // stackalloc 初期化子
            Span<int> span = stackalloc[] { 1, 2, 3 };
            var array = new int[] { 1, 2, 3 };

            span.ToArray().Should().BeEquivalentTo(array);
        }

        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(64)]
        public void SwitchAllocate(int size)
        {
            if (size > byte.MaxValue) throw new ArgumentException();

            // 要求サイズに応じて確保元をを切り替える
            Span<byte> span = size <= 32 ? stackalloc byte[size] : new byte[size];

            for (var i = 0; i < span.Length; ++i)
            {
                span[i] = (byte)i;
            }

            span.ToArray().Should().BeEquivalentTo(Enumerable.Range(0, size));
        }


    }
}
