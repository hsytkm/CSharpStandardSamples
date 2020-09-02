using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.Structs
{
    static partial class StructExtension
    {
        internal static byte[] GetBytesByMarshal<T>(in T source) where T : struct
        {
            var bytes = new byte[Marshal.SizeOf<T>()];
            var ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocCoTaskMem(bytes.Length);
                Marshal.StructureToPtr(source, ptr, false);
                Marshal.Copy(ptr, bytes, 0, bytes.Length);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(ptr);
            }
            return bytes;
        }

        internal static byte[] GetBytesByGCHandle<T>(in T source) where T : struct
        {
            var bytes = new byte[Marshal.SizeOf<T>()];
            var gchw = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                Marshal.StructureToPtr(source, gchw.AddrOfPinnedObject(), false);
            }
            finally
            {
                gchw.Free();
            }
            return bytes;
        }

        internal static byte[] GetBytesBySpanCast<T>(T source) where T : struct
        {
            var spanT = MemoryMarshal.CreateReadOnlySpan(ref source, length: 1);
            var spanBytes = MemoryMarshal.AsBytes(spanT);
            return spanBytes.ToArray();
        }

        internal static byte[] GetBytesByUnsafe<T>(in T source) where T : struct
        {
            var size = Unsafe.SizeOf<T>();
            var bs = new byte[size];
            Unsafe.WriteUnaligned(ref bs[0], source);
            return bs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetBytes<T>(T source) where T : struct
            => GetBytesBySpanCast(source);

    }
}
