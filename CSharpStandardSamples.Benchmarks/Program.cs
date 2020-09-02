using BenchmarkDotNet.Running;
using System;

namespace CSharpStandardSamples.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BytesToStruct>();
            //BenchmarkRunner.Run<StructToBytes>();
            //BenchmarkRunner.Run<SizeofStruct>();
        }
    }
}
