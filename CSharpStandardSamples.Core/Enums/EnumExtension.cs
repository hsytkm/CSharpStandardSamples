using System;

namespace CSharpStandardSamples.Core.Enums
{
    /// <summary>
    /// 標準メソッドを直で読んでるだけのメソッドもあるけど、
    /// 標準メソッドの存在を忘れことがあるので備忘として。
    /// </summary>
    static class EnumExtension
    {
        public static bool TryParse<T>(string source, out T value)
            where T : struct, Enum
        {
            return Enum.TryParse(source, out value);
        }

        public static bool TryParse<T>(object source, out T value)
            where T : struct, Enum
        {
            var result = Enum.TryParse<T>(source.ToString(), out value);
            if (!result) value = default;
            return result;
        }

        public static bool TryGetEnumIndex(object source, out int index)
        {
            if (source is Enum e)
            {
                var s = source.ToString();
                if (!string.IsNullOrEmpty(s))
                {
                    var type = e.GetType();
                    var value = Enum.Parse(type, s);
                    if (value != null)
                    {
                        index = (int)value;
                        return true;
                    }
                }
            }
            index = 0;
            return false;
        }

    }
}
