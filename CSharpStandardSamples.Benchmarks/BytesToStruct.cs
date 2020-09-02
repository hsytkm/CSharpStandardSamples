using BenchmarkDotNet.Attributes;
using CSharpStandardSamples.Core.Structs;
using System;
using System.Linq;

namespace CSharpStandardSamples.Benchmarks
{
    /*  |------------------ |----------:|-----------:|---------:|
     *  |    ParseByMarshal | 463.68 ns | 144.499 ns | 7.920 ns |
     *  |   ParseByGCHandle | 389.87 ns |  70.058 ns | 3.840 ns |
     *  |   ParseBySpanCast |  26.56 ns |   7.473 ns | 0.410 ns |
     *  | ParseByUnsafeRead |  24.33 ns |   2.451 ns | 0.134 ns |
     *  |   ParseByUnsafeAs |  26.68 ns |  11.403 ns | 0.625 ns |
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

        [Benchmark]
        public void ParseByUnsafeRead()
        {
            var s = StructExtension.ParseByUnsafeRead<MyStruct>(_sourceBytes);
        }

        [Benchmark]
        public void ParseByUnsafeAs()
        {
            var s = StructExtension.ParseByUnsafeAs<MyStruct>(_sourceBytes);
        }

    }
}
