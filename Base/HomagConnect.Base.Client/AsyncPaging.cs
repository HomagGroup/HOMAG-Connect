#if NET10_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HomagConnect.Base.Client
{

    /// <summary>
    /// Provides helpers for streaming paged API results as asynchronous sequences.
    /// </summary>
    /// <remarks>
    /// The helpers request one page ahead before yielding the current page's items. This keeps memory usage bounded
    /// while allowing the next request to run while the caller processes the current items.
    /// </remarks>
    public static class AsyncPaging
    {
        /// <summary>
        /// Represents a page returned by an endpoint that uses continuation-token paging.
        /// </summary>
        /// <typeparam name="T">The type of item contained in the page.</typeparam>
        /// <param name="Items">The items returned in the current page.</param>
        /// <param name="ContinuationToken">The token used to request the next page, or <see langword="null" /> when no further page is available.</param>
        public sealed record ContinuationTokenPage<T>(IEnumerable<T> Items, string? ContinuationToken);

        /// <summary>
        /// Streams every item from an endpoint that uses <c>take</c>/<c>skip</c> paging.
        /// </summary>
        /// <typeparam name="T">The type of item returned by the endpoint.</typeparam>
        /// <param name="getPageAsync">A delegate that retrieves one page for the specified page size and skip count.</param>
        /// <param name="pageSize">The number of items requested per page.</param>
        /// <param name="cancellationToken">A token that can be used to cancel paging and enumeration.</param>
        /// <returns>An asynchronous sequence that yields all items returned by the paged endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="getPageAsync" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="pageSize" /> is less than or equal to zero.</exception>
        /// <exception cref="OperationCanceledException">Thrown when <paramref name="cancellationToken" /> is canceled.</exception>
        /// <remarks>
        /// A page with fewer items than <paramref name="pageSize" /> is treated as the final page.
        /// The next page is requested before yielding the current page's items, which overlaps network latency
        /// with caller-side processing while keeping at most one additional request in flight.
        /// </remarks>
        public static async IAsyncEnumerable<T> GetAllByTakeSkip<T>(
            Func<int, int, Task<IEnumerable<T>>> getPageAsync,
            int pageSize,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(getPageAsync);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

            var skip = 0;
            var pageTask = getPageAsync(pageSize, skip);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var page = await pageTask;
                var items = page.ToList();
                var hasNextPage = items.Count == pageSize;

                if (hasNextPage)
                {
                    skip += pageSize;
                    pageTask = getPageAsync(pageSize, skip);
                }

                foreach (var item in items)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }

                if (!hasNextPage)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Streams every item from an endpoint that uses a continuation token for paging.
        /// </summary>
        /// <typeparam name="T">The type of item returned by the endpoint.</typeparam>
        /// <param name="getPageAsync">A delegate that retrieves one page for the specified continuation token.</param>
        /// <param name="cancellationToken">A token that can be used to cancel paging and enumeration.</param>
        /// <returns>An asynchronous sequence that yields all items returned by the paged endpoint.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="getPageAsync" /> is <see langword="null" />.</exception>
        /// <exception cref="OperationCanceledException">Thrown when <paramref name="cancellationToken" /> is canceled.</exception>
        /// <remarks>
        /// A null or whitespace continuation token marks the end of the result stream.
        /// The next page is requested before yielding the current page's items, which overlaps network latency
        /// with caller-side processing while keeping at most one additional request in flight.
        /// </remarks>
        public static async IAsyncEnumerable<T> GetAllByContinuationToken<T>(
            Func<string?, Task<ContinuationTokenPage<T>>> getPageAsync,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(getPageAsync);

            string? continuationToken = null;
            var pageTask = getPageAsync(continuationToken);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var page = await pageTask;
                continuationToken = page.ContinuationToken;

                if (!string.IsNullOrWhiteSpace(continuationToken))
                {
                    pageTask = getPageAsync(continuationToken);
                }

                foreach (var item in page.Items)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return item;
                }

                if (string.IsNullOrWhiteSpace(continuationToken))
                {
                    break;
                }
            }
        }
    }

}

#endif
