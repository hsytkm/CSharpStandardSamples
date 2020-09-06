using System;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.Unmanages
{
    /* 
     * https://github.com/hsytkm/UnmanagedMemoryCopySamples
     */
    static class MemCopyManagedToUnmanagedExtension
    {
        internal static void CopyByMarshal(IntPtr destinationPointer, byte[] sourceArray, int length = 0)
        {
            if (length == 0) length = sourceArray.Length;
            Marshal.Copy(sourceArray, startIndex: 0, destinationPointer, length);
        }

    }
}
