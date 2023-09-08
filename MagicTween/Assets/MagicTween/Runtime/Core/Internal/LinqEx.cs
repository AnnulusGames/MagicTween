using System.Collections.Generic;
using System.Linq;

namespace MagicTween.Core
{
    internal static class LinqEx
    {
        public static IEnumerable<T> Concat<T>
        (
            this IEnumerable<T> first,
            params T[] second
        )
        {
            return Enumerable.Concat(first, second);
        }
    }
}