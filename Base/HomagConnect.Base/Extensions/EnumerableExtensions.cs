using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// See <see cref="IEnumerable{T}"/> extensions methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Ensures an IAsyncEnumerable is only enumerated once. 
        /// </summary>
        public static async Task<IReadOnlyCollection<T>> AsEnumerated<T>(this IAsyncEnumerable<T> original)
        {
            if (original == null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            return await original.ToListAsync();
        }

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