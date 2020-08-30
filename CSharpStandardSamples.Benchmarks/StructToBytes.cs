using BenchmarkDotNet.Attributes;
using CSharpStandardSamples.Core.Structs;
using System;

namespace CSharpStandardSamples.Benchmarks
{
    /*  |             Method |      Mean |     Error |    StdDev |
     *  |------------------- |----------:|----------:|----------:|
     *  |  GetBytesByMarshal | 356.57 ns | 235.65 ns | 12.917 ns |
     *  | GetBytesByGCHandle | 272.35 ns | 131.16 ns |  7.189 ns |
     *  | GetBytesBySpanCast |  76.69 ns |  38.05 ns |  2.086 ns |
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

    }
}
