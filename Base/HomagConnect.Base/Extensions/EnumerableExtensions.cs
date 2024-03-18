using System;
using System.Collections.Generic;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// See <see cref="IEnumerable{T}"/> extensions methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Ensures an IEnumerable is only enumerated once. 
        /// </summary>
        public static IReadOnlyCollection<T> AsEnumerated<T>(this IEnumerable<T> original)
        {
            if (original == null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (original is IReadOnlyCollection<T> originalCollection)
            {
                return originalCollection;
            }

            return new List<T>(original).AsReadOnly();
        }
    }
}