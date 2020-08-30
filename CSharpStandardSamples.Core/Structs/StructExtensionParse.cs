using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.Structs
{
    static partial class StructExtension
    {
        internal static T ParseByMarshal<T>(byte[] source) where T : struct
        {
            T value;
            var ptr = IntPtr.Zero;

            try
            {
                //ptr = Marshal.AllocHGlobal(source.Length);
                ptr = Marshal.AllocCoTaskMem(source.Length);
                Marshal.Copy(source, 0, ptr, source.Length);
                value = Marshal.PtrToStructure<T>(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    //Marshal.FreeHGlobal(ptr);
                    Marshal.FreeCoTaskMem(ptr);
                }
            }
            return value;
        }

        internal static T ParseByGCHandle<T>(byte[] source) where T : struct
        {
            T value;
            var gch = GCHandle.Alloc(source, GCHandleType.Pinned);

            try
            {
                value = Marshal.PtrToStructure<T>(gch.AddrOfPinnedObject());
            }
            finally
            {
                gch.Free();
            }
            return value;
        }

        internal static T ParseBySpanCast<T>(ReadOnlySpan<byte> buffer) where T : struct
        {
            return MemoryMarshal.Cast<byte, T>(buffer)[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T Parse<T>(byte[] buffer) where T : struct
            => ParseBySpanCast<T>(buffer);


        public static bool TryParse<T>(byte[] source, out T value) where T : struct
        {
            if (source.Length != Marshal.SizeOf<T>())
            {
                value = default;
                return false;
            }

            value = Parse<T>(source);
            return true;
        }

    }
}
