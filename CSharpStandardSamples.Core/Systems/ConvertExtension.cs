using System;

namespace CSharpStandardSamples.Core.Systems
{
    public static class ConvertExtension
    {
        // ◆もう少しマシな型の比較方法ないのん…
        private static readonly string _fullNameSByte = typeof(sbyte).FullName;
        private static readonly string _fullNameByte = typeof(byte).FullName;
        private static readonly string _fullNameInt16 = typeof(short).FullName;
        private static readonly string _fullNameInt32 = typeof(int).FullName;
        private static readonly string _fullNameInt64 = typeof(long).FullName;
        private static readonly string _fullNameUInt16 = typeof(ushort).FullName;
        private static readonly string _fullNameUInt32 = typeof(uint).FullName;
        private static readonly string _fullNameUInt64 = typeof(ulong).FullName;
        private static readonly string _fullNameFloat = typeof(float).FullName;
        private static readonly string _fullNameDouble = typeof(double).FullName;

        // ◆もう少しマシな実装ないのん…
        public static T GetValue<T>(string value) where T : struct
        {
            var fullName = typeof(T).FullName;

            if (fullName == _fullNameInt16)
                return (T)((object)Convert.ToInt16(value));
            if (fullName == _fullNameInt32)
                return (T)((object)Convert.ToInt32(value));
            if (fullName == _fullNameInt64)
                return (T)((object)Convert.ToInt64(value));

            if (fullName == _fullNameUInt16)
                return (T)((object)Convert.ToUInt16(value));
            if (fullName == _fullNameUInt32)
                return (T)((object)Convert.ToUInt32(value));
            if (fullName == _fullNameUInt64)
                return (T)((object)Convert.ToUInt64(value));

            if (fullName == _fullNameFloat)
                return (T)((object)Convert.ToSingle(value));
            if (fullName == _fullNameDouble)
                return (T)((object)Convert.ToDouble(value));

            if (fullName == _fullNameSByte)
                return (T)((object)Convert.ToSByte(value));
            if (fullName == _fullNameByte)
                return (T)((object)Convert.ToByte(value));

            throw new NotSupportedException(fullName);
        }

        // ◆もう少しマシな実装ないのん…
        public static T GetValueFromHex<T>(string value) where T : struct
        {
            var fullName = typeof(T).FullName;

            if (fullName == _fullNameInt16)
                return (T)((object)Convert.ToInt16(value, 16));
            if (fullName == _fullNameInt32)
                return (T)((object)Convert.ToInt32(value, 16));
            if (fullName == _fullNameInt64)
                return (T)((object)Convert.ToInt64(value, 16));

            if (fullName == _fullNameUInt16)
                return (T)((object)Convert.ToUInt16(value, 16));
            if (fullName == _fullNameUInt32)
                return (T)((object)Convert.ToUInt32(value, 16));
            if (fullName == _fullNameUInt64)
                return (T)((object)Convert.ToUInt64(value, 16));

            throw new NotSupportedException(fullName);
        }

    }
}
