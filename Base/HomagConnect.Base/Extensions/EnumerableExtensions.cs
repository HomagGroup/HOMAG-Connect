using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// See <see cref="IEnumerable{T}" /> extensions methods.
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

        /// <summary>
        /// Ensures an IEnumerable is only enumerated once.
        /// </summary>
        public static async Task<IReadOnlyCollection<T>> AsReadOnlyAsync<T>(this Task<IEnumerable<T>> original)
        {
            return (await original).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the first item in an awaited IEnumerable.
        /// </summary>
        public static async Task<T> FirstAsync<T>(this Task<IEnumerable<T>> original)
        {
            return (await original).First();
        }

        /// <summary>
        /// Gets the first item in an awaited IEnumerable.
        /// </summary>
        public static async Task<T> FirstAsync<T>(this Task<IEnumerable<T>> original, Func<T, bool> predicate)
        {
            return (await original).First(predicate);
        }

        /// <summary>
        /// Gets the first item in an awaited IEnumerable.
        /// </summary>
        public static async Task<T> FirstOrDefaultAsync<T>(this Task<IEnumerable<T>> original, Func<T, bool> predicate)
        {
            return (await original).FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets the first or default item in an awaited IEnumerable.
        /// </summary>
        public static async Task<T?> FirstOrDefaultAsync<T>(this Task<IEnumerable<T>>? original)
        {
            return original == null ? default : (await original).FirstOrDefault();
        }

        /// <summary>
        /// Ensures an IEnumerable is only enumerated once.
        /// </summary>
        public static async Task<IList<T>?> ToListAsync<T>(this Task<IEnumerable<T>>? original)
        {
            return original == null ? null : (await original).ToList();
        }
    }
}