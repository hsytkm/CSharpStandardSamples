using CSharpStandardSamples.Core.Unmanages;
using FluentAssertions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Xunit;
using MyMemCopy = CSharpStandardSamples.Core.Unmanages.MemCopyUnmanagedToManagedExtension;

namespace CSharpStandardSamples.Tests.Unmanages
{
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
    public class MemCopyUnmanagedToManaged : IDisposable
    {
        private const int ALLOCATE_SIZE = 1 * 1024;    // 1KByte

        private readonly UnmanagedMemory _srcMemory;
        private readonly byte[] _destArray;

        public MemCopyUnmanagedToManaged()
        {
            static void SetData(ref UnmanagedMemory memory)
            {
                var intPtr = memory.IntPtr;
                for (var i = 0; i < memory.Length; ++i)
                    Marshal.WriteByte(intPtr + i, (byte)(i & 0xff));
            }

            _srcMemory = new UnmanagedMemory(ALLOCATE_SIZE);
            SetData(ref _srcMemory);

            _destArray = new byte[ALLOCATE_SIZE];
        }

        private static byte[] IntPtrToByteArray(IntPtr srcPtr, int length)
        {
            var destArray = new byte[length];
            MyMemCopy.CopyByMarshal(destArray, srcPtr);
            return destArray;
        }

        [Fact]
        public void CopyByMarshal()
        {
            var srcPtr = _srcMemory.IntPtr;
            var srcLength = _srcMemory.Length;

            _destArray.Should().NotBeEmpty().And.OnlyContain(x => x == 0, "memory is filled zero.");

            // byte[] <- IntPtr
            MyMemCopy.CopyByMarshal(_destArray, srcPtr);

            // 内部で同じ関数を使っており、テストになっていない気もするが、まぁいいか
            var srcArray = IntPtrToByteArray(srcPtr, srcLength);
            _destArray.Should().NotBeEmpty().And.Equal(srcArray);
        }

        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
        public void Dispose()
        {
            _srcMemory.Dispose();
        }
    }
}
