using BenchmarkDotNet.Attributes;
using CSharpStandardSamples.Core.Structs;
using System;

namespace CSharpStandardSamples.Benchmarks
{
    /*  |             Method |      Mean |      Error |    StdDev |
     *  |------------------- |----------:|-----------:|----------:|
     *  |  GetBytesByMarshal | 345.84 ns | 784.582 ns | 43.006 ns |
     *  | GetBytesByGCHandle | 251.49 ns | 141.881 ns |  7.777 ns |
     *  | GetBytesBySpanCast |  78.61 ns |   9.676 ns |  0.530 ns |
     *  |   GetBytesByUnsafe |  89.14 ns |  45.489 ns |  2.493 ns |
     */

    //[DryJob]        // 動作確認用の実行
    [ShortRunJob]   // 簡易測定
    public class StructToBytes
    {
        private const int _sourceSize = 256;
        private readonly MyStruct _myStruct;

        private unsafe struct MyStruct
        {
            public fixed byte Bytes[_sourceSize];
        }

        public StructToBytes()
        {
            _myStruct = new MyStruct();

            unsafe
            {
                for (var i = 0; i < _sourceSize; ++i)
                    _myStruct.Bytes[0] = (byte)(i % 0xff);
            }
        }

        [Benchmark]
        public void GetBytesByMarshal()
        {
            var bs = StructExtension.GetBytesByMarshal(_myStruct);
        }

        [Benchmark]
        public void GetBytesByGCHandle()
        {
            var bs = StructExtension.GetBytesByGCHandle(_myStruct);
        }

        [Benchmark]
        public void GetBytesBySpanCast()
        {
            var bs = StructExtension.GetBytesBySpanCast(_myStruct);
        }

        [Benchmark]
        public void GetBytesByUnsafe()
        {
            var bs = StructExtension.GetBytesByUnsafe(_myStruct);
        }

    }
}
