using BenchmarkDotNet.Attributes;
using CSharpStandardSamples.Core.Structs;
using System;
using System.Linq;

namespace CSharpStandardSamples.Benchmarks
{
    /*  |          Method |      Mean |      Error |    StdDev |
     *  |---------------- |----------:|-----------:|----------:|
     *  |  ParseByMarshal | 431.25 ns | 218.834 ns | 11.995 ns |
     *  | ParseByGCHandle | 364.79 ns |  24.069 ns |  1.319 ns |
     *  | ParseBySpanCast |  25.97 ns |   4.086 ns |  0.224 ns |
     */

    //[DryJob]        // 動作確認用の実行
    [ShortRunJob]   // 簡易測定
    public class BytesToStruct
    {
        private const int _sourceSize = 256;
        private readonly byte[] _sourceBytes;

        private unsafe struct MyStruct
        {
            public fixed byte Bytes[_sourceSize];
        }

        public BytesToStruct()
        {
            _sourceBytes = Enumerable.Range(0, _sourceSize)
                .Select(x => (byte)(x % 0xff)).ToArray();
        }

        [Benchmark]
        public void ParseByMarshal()
        {
            var s = StructExtension.ParseByMarshal<MyStruct>(_sourceBytes);
        }

        [Benchmark]
        public void ParseByGCHandle()
        {
            var s = StructExtension.ParseByGCHandle<MyStruct>(_sourceBytes);
        }

        [Benchmark]
        public void ParseBySpanCast()
        {
            var s = StructExtension.ParseBySpanCast<MyStruct>(_sourceBytes);
        }

    }
}
