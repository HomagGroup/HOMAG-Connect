using System.Net.Http.Headers;

namespace HomagConnect.Base.Client;

/// <summary>
/// Policy for repeating requests which were rejected because of rate limiting (HTTP 429).
/// </summary>
internal static class RateLimitRetry
{
    /// <summary>
    /// Maximum number of repetitions after the initial call.
    /// </summary>
    internal const int MaxRetries = 3;

    /// <summary>
    /// Wait duration before the first repetition. It doubles with every further repetition (2s, 4s, 8s).
    /// </summary>
    internal static readonly TimeSpan InitialDelay = TimeSpan.FromSeconds(2);

    /// <summary>
    /// Longest <c>Retry-After</c> duration which is awaited. If the service asks for a longer pause, the call is not repeated.
    /// </summary>
    internal static readonly TimeSpan MaxRetryAfter = TimeSpan.FromSeconds(30);

    private const int _TooManyRequestsStatusCode = 429;

    internal static bool IsRateLimited(HttpResponseMessage response)
    {
        return (int)response.StatusCode == _TooManyRequestsStatusCode;
    }

    /// <summary>
    /// Determines how long to wait before the next repetition.
    /// </summary>
    /// <param name="retry">Zero based number of the repetition which is about to be started.</param>
    /// <param name="response">The rate limited response.</param>
    /// <param name="delay">The wait duration. The longer one of the increasing default and the <c>Retry-After</c> header.</param>
    /// <returns><c>false</c>, if the service asks for a pause longer than <see cref="MaxRetryAfter" />.</returns>
    internal static bool TryGetDelay(int retry, HttpResponseMessage response, out TimeSpan delay)
    {
        delay = TimeSpan.FromTicks(InitialDelay.Ticks << retry);

        var retryAfter = GetRetryAfter(response.Headers.RetryAfter);

        if (retryAfter > MaxRetryAfter)
        {
            return false;
        }

        if (retryAfter > delay)
        {
            delay = retryAfter.Value;
        }

        return true;
    }

    private static TimeSpan? GetRetryAfter(RetryConditionHeaderValue? retryAfter)
    {
        if (retryAfter?.Delta is { } delta)
        {
            return delta;
        }

        if (retryAfter?.Date is { } date)
        {
            var wait = date - DateTimeOffset.UtcNow;

            return wait > TimeSpan.Zero ? wait : TimeSpan.Zero;
        }

        return null;
    }
}
