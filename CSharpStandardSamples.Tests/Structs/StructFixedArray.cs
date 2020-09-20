using FluentAssertions;
using System.Runtime.InteropServices;
using Xunit;

namespace CSharpStandardSamples.Tests.Structs
{
    public class StructFixedArray
    {
        // �Œ�T�C�Y �o�b�t�@(�֗����������ǁA����͂���ŕs�ցc)
        [StructLayout(LayoutKind.Sequential)]
        unsafe struct FixedBuffer
        {
            private const int _length = 32;
            public static int Length => _length;
            public fixed char fixedBuffer[_length]; // fixed T[] �� readonly �s��
        }

        class BoxingContainer<T> where T : struct
        {
            // �Œ�T�C�Y�o�b�t�@�́A���[�J���܂��̓t�B�[���h����̂݃A�N�Z�X�ł���(getter�͖���)
            public T Data;
            public int Length { get; }
            public BoxingContainer(T data, int length) => (Data, Length) = (data, length);
        }

        [Fact]
        public void Equal()
        {
            var buffer = new FixedBuffer();
            var container = new BoxingContainer<FixedBuffer>(buffer, FixedBuffer.Length);

            unsafe
            {
                fixed (char* ptr = container.Data.fixedBuffer)
                {
                    *ptr = 'A';
                }
                container.Data.fixedBuffer[0].Should().Be('A');

                var index = container.Length - 1;
                container.Data.fixedBuffer[index] = 'B';
                container.Data.fixedBuffer[index].Should().Be('B');
            }
        }

    }
}
