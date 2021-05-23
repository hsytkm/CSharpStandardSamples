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
 *  
 *  (C#) ArrayPool<T>.Shared 解体新書 - ネコのために鐘は鳴る
 *  https://ikorin2.hatenablog.jp/entry/2020/07/25/113904
 *  
 */
namespace CSharpStandardSamples.Core.Spans
{
    /// <summary>
    /// <see cref="ArrayPool{T}"/>から借りてきて、<see cref="Dispose"/>で返却するためのラッパー。
    /// 小サイズなら stackalloc<T> を使った方が効率よく、バカ大なら T[] するしかない(はず)。
    /// 確保した領域はゼロクリアされない
    /// </summary>
    public struct PooledArray<T> : IDisposable
    {
        private readonly T[] _array;

        // _array.Length は要求したサイズ以上になる。一致しない
        public int Length { get; }

        private readonly ArrayPool<T> _pool;
        private bool _disposed;

        private PooledArray(int minimumLength, ArrayPool<T> pool) =>
            (_array, _pool, Length, _disposed) = (pool.Rent(minimumLength), pool, minimumLength, false);

        public PooledArray(int minimumLength) : this(minimumLength, ArrayPool<T>.Shared)
        { }

        public Span<T> Span => _array.AsSpan(0, Length);
        public ReadOnlySpan<T> RoSpan => _array.AsSpan(0, Length);

        public ref T this[int index] => ref _array[index];

        public void Dispose()
        {
            if (!_disposed)
            {
                _pool.Return(_array);
                _disposed = true;
            }
        }
    }
}
