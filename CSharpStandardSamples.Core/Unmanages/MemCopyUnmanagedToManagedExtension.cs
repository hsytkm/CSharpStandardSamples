using System;
using System.Runtime.InteropServices;

namespace CSharpStandardSamples.Core.Unmanages
{
    /* 
     * https://github.com/hsytkm/UnmanagedMemoryCopySamples
     */
    static class MemCopyUnmanagedToManagedExtension
    {
        internal static void CopyByMarshal(byte[] destinationArray, IntPtr sourceIntPtr, int length = 0)
        {
            if (length == 0) length = destinationArray.Length;
            Marshal.Copy(sourceIntPtr, destinationArray, startIndex: 0, length);
        }

    }
}
