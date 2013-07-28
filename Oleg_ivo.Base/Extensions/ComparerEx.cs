using System.Collections.Generic;

namespace Oleg_ivo.PrismExtensions.Extensions 
{

    public static class ComparerEx
    {
        public static T Max<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) < 0 ? value2 : value1;
        }

        public static T Min<T>(this IComparer<T> comparer, T value1, T value2)
        {
            return comparer.Compare(value1, value2) < 0 ? value1 : value2;
        }
    }


    public static class Comparer
    {
        public static T Max<T>(T value1, T value2)
        {
            return Comparer<T>.Default.Max(value1, value2);
        }

        public static T Min<T>(T value1, T value2)
        {
            return Comparer<T>.Default.Min(value1, value2);
        }
    }

}
