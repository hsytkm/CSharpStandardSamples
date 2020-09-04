using CSharpStandardSamples.Core.Systems;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CSharpStandardSamples.Core.Regexs
{
    static partial class RegexMatchExtension
    {
        public static T GetValue<T>(this Match match, int index) where T : struct
        {
            try
            {
                var value = match.Groups[index].Value;
                if (string.IsNullOrEmpty(value)) throw new KeyNotFoundException();

                return ConvertExtension.GetValue<T>(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T GetValue<T>(this Match match, string name) where T : struct
        {
            try
            {
                var value = match.Groups[name].Value;
                if (string.IsNullOrEmpty(value)) throw new KeyNotFoundException();

                return ConvertExtension.GetValue<T>(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T GetHexValue<T>(this Match match, int index) where T : struct
        {
            try
            {
                var value = match.Groups[index].Value;
                if (string.IsNullOrEmpty(value)) throw new KeyNotFoundException();

                return ConvertExtension.GetValueFromHex<T>(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T GetHexValue<T>(this Match match, string name) where T : struct
        {
            try
            {
                var value = match.Groups[name].Value;
                if (string.IsNullOrEmpty(value)) throw new KeyNotFoundException();

                return ConvertExtension.GetValueFromHex<T>(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
