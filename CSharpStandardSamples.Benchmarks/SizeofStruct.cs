using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace CSharpStandardSamples.Benchmarks
{
    /*  |        Method |       Mean |     Error |    StdDev |
     *  |-------------- |-----------:|----------:|----------:|
     *  | MarshalSizeOf | 70.9359 ns | 1.4405 ns | 2.0659 ns |
     *  |  UnsafeSizeOf |  0.0193 ns | 0.0163 ns | 0.0145 ns |
     */

    //[DryJob]        // 動作確認用の実行
    //[ShortRunJob]   // 簡易測定
    public class SizeofStruct
    {
        [StructLayout(LayoutKind.Sequential, Size = 4)]
        readonly struct MyStruct4
        {
            public readonly Int32 data;
        }

        [StructLayout(LayoutKind.Sequential, Size = 64)]
        readonly struct MyStruct64
        {
            public readonly Int32 data;
        }

        public SizeofStruct() { }

        [Benchmark]
        public void MarshalSizeOf()
        {
            var size0 = Marshal.SizeOf<MyStruct4>();
            var size1 = Marshal.SizeOf<MyStruct64>();
        }

        [Benchmark]
        public void UnsafeSizeOf()
        {
            // ソースコード
            // https://github.com/dotnet/corefx/blob/64c6d9fe5409be14bdc3609d73ffb3fea1f35797/src/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il#L154
            var size0 = Unsafe.SizeOf<MyStruct4>();
            var size1 = Unsafe.SizeOf<MyStruct64>();
        }


    }
}
