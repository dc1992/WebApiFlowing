using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiFlowing.BusinessLogic.Extensions
{
    public static class GenericExtensions
    {
        public static void ShouldNotBeNull<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException($"{typeof(T)} cannot be null");
        }

        public static void ShouldContainAtLeast<T>(this IEnumerable<T> source, int numberOfElements)
        {
            if (source == null || source.Count() < numberOfElements)
                throw new ArgumentOutOfRangeException($"{typeof(T)} must contain at least {numberOfElements} elements");
        }
    }
}