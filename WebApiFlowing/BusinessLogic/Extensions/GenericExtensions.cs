using System;

namespace WebApiFlowing.BusinessLogic.Extensions
{
    public static class GenericExtensions
    {
        public static void ShouldNotBeNull<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException($"{typeof(T)} cannot be null");
        }
    }
}