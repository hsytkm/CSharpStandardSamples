using System;
using System.Buffers;

/*  Pooling large arrays with ArrayPool
 *  https://adamsitnik.com/Array-Pool/
 *      that it has a default max array length, equal to 2^20 (1024*1024 = 1 048 576).
 *   
 *  LitJWTに見るモダンなC#のbyte[]とSpan操作法
 *  http://neue.cc/2019/05/27_578.html
 *      ようするに、今どきnew byte[]なんてしたら殺されるぞ！
 *      
 *  https://gist.github.com/ufcpp/b0853cff5823d49306ba693aaa5c39fb
 */
namespace CSharpStandardSamples.Core.Spans
{
    /// <summary>
    /// <see cref="ArrayPool{T}"/>から借りてきて、<see cref="Dispose"/>で返却するためのラッパー。
    /// 小サイズなら stackalloc<T> を使った方が効率よく、バカ大なら T[] するしかない(はず)。
    /// </summary>
    public struct PooledArray<T> : IDisposable
    {
        public T[] Array { get; }
        public int Length => Array.Length;

        private readonly ArrayPool<T> _pool;

        public PooledArray(int minimumLength) :
            this(minimumLength, ArrayPool<T>.Shared)
        { }

        private PooledArray(int minimumLength, ArrayPool<T> pool) =>
            (Array, _pool) = (pool.Rent(minimumLength), pool);

        public ref T this[int index] => ref Array[index];

        public void Dispose() => _pool?.Return(Array);
    }
}
