using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if false
/* unsafe必要なので有効にしてない。
 * 決め打ちサイズになるけど使い処はあるはず。
 */
namespace CSharpStandardSamples.Core.Structs
{
    /*  fixed T[8] で固定長バッファを使えるけど、あれは使い勝手が悪いので、力業で実装。
     *    例) fixed あると readonly struct にできない。
     *    
     *  https://gist.github.com/ufcpp/b0853cff5823d49306ba693aaa5c39fb
     */

    [StructLayout(LayoutKind.Sequential)]
    public struct Struct8<T> where T : unmanaged
    {
        private T x0, x1, x2, x3, x4, x5, x6, x7;
        public int Length => 8;

        public unsafe Span<T> AsSpan() =>
            new Span<T>(Unsafe.AsPointer(ref x0), Length);

        public ref T this[int i] => ref AsSpan()[i];
    }

    // struct の宣言自体に readonly を付けられないのでイマイチ…
    [StructLayout(LayoutKind.Sequential)]
    public struct ReadOnlyStruct8<T> where T : unmanaged
    {
        private T x0, x1, x2, x3, x4, x5, x6, x7;
        public int Length => 8;

        public unsafe ReadOnlySpan<T> AsReadOnlySpan() =>
            new ReadOnlySpan<T>(Unsafe.AsPointer(ref x0), Length);

        public T this[int i] => AsReadOnlySpan()[i];
    }

}
#endif