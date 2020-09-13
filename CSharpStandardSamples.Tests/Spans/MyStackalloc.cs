using CSharpStandardSamples.Core.Spans;
using FluentAssertions;
using System;
using System.Buffers;
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
            if (size > 0xff) throw new ArgumentException();

            // 要求サイズに応じて確保元をを切り替える
            Span<byte> bytes = size <= 32 ? stackalloc byte[size] : new byte[size];

            for (var i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = (byte)(i % 0xff);
            }

            var answer = Enumerable.Range(0, size).Select(x => x % 0xff);
            bytes.ToArray().Should().BeEquivalentTo(answer);
        }

        /*  Pooling large arrays with ArrayPool
         *  https://adamsitnik.com/Array-Pool/
         *      that it has a default max array length, equal to 2^20 (1024*1024 = 1 048 576).
         *   
         *  LitJWTに見るモダンなC#のbyte[]とSpan操作法
         *  http://neue.cc/2019/05/27_578.html
         *      ようするに、今どきnew byte[]なんてしたら殺されるぞ！
         */
        [Theory]
        [InlineData(32)]
        [InlineData(1024)]
        public void SwitchAllocate2_1(int size)
        {
            var allocSizeMax = 128 * 1024;  // 1MByte最大っぽいので控えめに128KByte
            if (size > allocSizeMax) throw new ArgumentException();

            var rentBytes = ArrayPool<byte>.Shared.Rent(size);
            try
            {
                Span<byte> bytes = rentBytes.AsSpan();

                for (var i = 0; i < bytes.Length; ++i)
                {
                    bytes[i] = (byte)(i % 0xff);
                }

                var answer = Enumerable.Range(0, size).Select(x => x % 0xff);
                bytes.ToArray().Should().BeEquivalentTo(answer);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rentBytes);
            }
        }

        [Theory]
        [InlineData(32)]
        [InlineData(1024)]
        public void SwitchAllocate2_2(int size)
        {
            var allocSizeMax = 128 * 1024;  // 1MByte最大っぽいので控えめに128KByte
            if (size > allocSizeMax) throw new ArgumentException();

            // 自作クラスで ArrayPool を管理
            using var bytes = new PooledArray<byte>(size);
            for (var i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = (byte)(i % 0xff);
            }

            var answer = Enumerable.Range(0, size).Select(x => x % 0xff);
            bytes.Array.Should().BeEquivalentTo(answer);
        }

    }
}
