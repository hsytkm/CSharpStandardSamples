using System;
using System.Collections.Generic;

namespace CSharpStandardSamples.Core.Extensions
{
    static class IEnumerableExtension
    {
        /// <summary>
        /// ForEach for IEnumerable
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="sequence">Sequence</param>
        /// <param name="action">Action</param>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            if (sequence is null) throw new ArgumentNullException(nameof(sequence));
            if (action is null) throw new ArgumentNullException(nameof(action));

            foreach (T item in sequence)
                action(item);
        }

    }
}
