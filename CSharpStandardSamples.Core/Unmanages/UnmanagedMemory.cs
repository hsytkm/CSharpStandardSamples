using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.Unmanages
{
    /// <summary>
    /// アンマネージドなメモリの管理クラス（Disposeで返却する）
    /// </summary>
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<保留中>")]
    public struct UnmanagedMemory : IEquatable<UnmanagedMemory>, IDisposable
    {
        public IntPtr IntPtr { get; private set; }
        public int Length { get; private set; }

        public UnmanagedMemory(int length) => (IntPtr, Length) = AllocMem(length);

        //public UnmanagedMemory(IntPtr intPtr, int length) => (IntPtr, Length) = (intPtr, length);

        private static (IntPtr intPtr, int length) AllocMem(int requestLength, bool isFillZero = true)
        {
            if (requestLength <= 0) throw new ArgumentOutOfRangeException();

            var intPtr = Marshal.AllocCoTaskMem(requestLength);
            if (isFillZero) FillZero(intPtr, requestLength);
            return (intPtr, requestLength);

            static void FillZero(IntPtr intPtr, int length)
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
