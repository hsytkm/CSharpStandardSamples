using System;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.MemCopies
{
    /* 
     * https://github.com/hsytkm/UnmanagedMemoryCopySamples
     */
    static partial class MemCopyManagedToUnmanagedExtension
    {
        internal static void CopyByMarshal(IntPtr destinationPointer, byte[] sourceArray, int length = 0)
        {
            if (length == 0) length = sourceArray.Length;
            Marshal.Copy(sourceArray, startIndex: 0, destinationPointer, length);
        }

    }
}
