using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class Extensions
    {
        public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (index, item));
        }
    }
}