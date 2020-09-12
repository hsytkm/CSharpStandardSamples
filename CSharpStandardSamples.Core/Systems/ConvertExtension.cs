using System;

namespace CSharpStandardSamples.Core.Systems
{
    public static class ConvertExtension
    {
        // ◆もう少しマシな実装ないのん…
        public static T GetValue<T>(string value) where T : struct
        {
            var name = typeof(T).Name;
            return name switch
            {
                nameof(Int16) => (T)(object)Convert.ToInt16(value),
                nameof(Int32) => (T)(object)Convert.ToInt32(value),
                nameof(Int64) => (T)(object)Convert.ToInt64(value),

                nameof(UInt16) => (T)(object)Convert.ToUInt16(value),
                nameof(UInt32) => (T)(object)Convert.ToUInt32(value),
                nameof(UInt64) => (T)(object)Convert.ToUInt64(value),

                nameof(Single) => (T)(object)Convert.ToSingle(value),
                nameof(Double) => (T)(object)Convert.ToDouble(value),

                nameof(SByte) => (T)(object)Convert.ToSByte(value),
                nameof(Byte) => (T)(object)Convert.ToByte(value),

                _ => throw new NotSupportedException(name)
            };
        }

        // ◆もう少しマシな実装ないのん…
        public static T GetValueFromHex<T>(string value) where T : struct
        {
            var name = typeof(T).Name;
            return name switch
            {
                nameof(Int16) => (T)(object)Convert.ToInt16(value, 16),
                nameof(Int32) => (T)(object)Convert.ToInt32(value, 16),
                nameof(Int64) => (T)(object)Convert.ToInt64(value, 16),

                nameof(UInt16) => (T)(object)Convert.ToUInt16(value, 16),
                nameof(UInt32) => (T)(object)Convert.ToUInt32(value, 16),
                nameof(UInt64) => (T)(object)Convert.ToUInt64(value, 16),

                _ => throw new NotSupportedException(name)
            };
        }

    }
}
