using CSharpStandardSamples.Core.Unmanage;
using FluentAssertions;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using MyMemCopy = CSharpStandardSamples.Core.Unmanage.MemCopyManagedToUnmanagedExtension;

namespace CSharpStandardSamples.Tests
{
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<•Û—¯’†>")]
    public class MemCopyManagedToUnmanaged : IDisposable
    {
        private const int ALLOCATE_SIZE = 1 * 1024;    // 1KByte

        private readonly byte[] _srcArray;
        private readonly UnmanagedMemory _destMemory;
        
        public MemCopyManagedToUnmanaged()
        {
            static byte[] GetZeroPaddingByteArray(int length)
            {
                var array = new byte[length];
                for (int i = 0; i < length; ++i)
                {
                    array[i] = (byte)(i & 0xff);
                }
                return array;
            }

            _srcArray = GetZeroPaddingByteArray(ALLOCATE_SIZE);
            _destMemory = new UnmanagedMemory(ALLOCATE_SIZE);
        }

        private static byte[] IntPtrToByteArray(IntPtr srcPtr, int length)
        {
            var destArray = new byte[length];
            MemCopyUnmanagedToManagedExtension.CopyByMarshal(destArray, srcPtr);
            return destArray;
        }

        [Fact]
        public void CopyByMarshal()
        {
            var destPtr = _destMemory.IntPtr;
            var destLength = _destMemory.Length;

            IntPtrToByteArray(destPtr, destLength)
                .Should().NotBeEmpty().And.OnlyContain(x => x == 0, "memory is filled zero.");

            // IntPtr <- byte[]
            MyMemCopy.CopyByMarshal(destPtr, _srcArray);

            IntPtrToByteArray(destPtr, destLength)
                .Should().NotBeEmpty().And.Equal(_srcArray);
        }

        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<•Û—¯’†>")]
        public void Dispose()
        {
            _destMemory.Dispose();
        }
    }
}
