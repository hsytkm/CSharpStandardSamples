using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.Unmanages
{
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
    public struct UnmanagedMemory : IEquatable<UnmanagedMemory>, IDisposable
    {
        public IntPtr IntPtr { get; private set; }
        public int Length { get; private set; }

        public UnmanagedMemory(int length)
        {
            (IntPtr, Length) = AllocZeroMem(length);
        }

        public UnmanagedMemory(IntPtr intPtr, int length) =>
            (IntPtr, Length) = (intPtr, length);

        private static (IntPtr intPtr, int length) AllocZeroMem(int requestLength)
        {
            var intPtr = Marshal.AllocCoTaskMem(requestLength);
            FillZero(intPtr, requestLength);
            return (intPtr, requestLength);
        }

        private static void FillZero(IntPtr intPtr, int length)
        {
            var rest = length;
            while (rest >= sizeof(ulong))
            {
                Marshal.WriteInt64(intPtr, rest - sizeof(ulong), 0);
                rest -= sizeof(ulong);
            }
            while (rest >= 1)
            {
                Marshal.WriteByte(intPtr, rest - 1, 0);
                rest--;
            }
        }

        public bool Equals(UnmanagedMemory other)
        {
            return this.IntPtr == other.IntPtr && this.Length == other.Length;
        }

        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
        public void Dispose()
        {
            if (IntPtr != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(IntPtr);
                (IntPtr, Length) = (IntPtr.Zero, 0);
            }
        }

    }
}
