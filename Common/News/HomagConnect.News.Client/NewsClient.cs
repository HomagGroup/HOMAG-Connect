using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

using HomagConnect.News.Contracts;

using Newtonsoft.Json;

namespace HomagConnect.News.Client;

/// <summary>
/// Represents the HOMAG Connect News client.
/// </summary>
public sealed class NewsClient : INewsClient, IDisposable
{
    private const string _FeedRoute = "api/news/feed";

    /// <summary>
    /// Cache lifetime matching the Azure Front Door CDN TTL for the news feed.
    /// </summary>
    private static readonly TimeSpan _FeedCacheMaxAge = TimeSpan.FromHours(24);

    private static readonly Uri _DefaultBaseUri = new("https://news-preview.homag.cloud");

    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of <see cref="NewsClient" /> using the default HOMAG Connect News base URI.
    /// </summary>
    public NewsClient() : this(_DefaultBaseUri)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NewsClient" /> using a custom base URI.
    /// </summary>
    public NewsClient(Uri baseUri)
    {
        _httpClient = new HttpClient { BaseAddress = baseUri };
        _disposeHttpClient = true;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NewsClient" /> using a pre-configured <see cref="HttpClient" />.
    /// </summary>
    public NewsClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _disposeHttpClient = false;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<NewsArticle>> GetLatest(
        CultureInfo cultureInfo,
        IEnumerable<string> tags,
        int take,
        CancellationToken cancellationToken = default)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(NewsClient));
        }

        if (cultureInfo is null)
        {
            throw new ArgumentNullException(nameof(cultureInfo));
        }

        if (take < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(take), take, "Value must be greater than or equal to 1.");
        }

        try
        {
            var requestUri = BuildFeedUri(cultureInfo, tags, take);

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = _FeedCacheMaxAge };

            using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await ReadContentAsStringAsync(response, cancellationToken).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<NewsArticle[]>(json) ?? [];
        }
        catch (Exception ex)
        {
            Trace.TraceError($"{nameof(NewsClient)}.{nameof(GetLatest)} failed: {ex}");
            return [];
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (_disposeHttpClient)
        {
            _httpClient.Dispose();
        }

        _disposed = true;
    }

    private Uri BuildFeedUri(CultureInfo cultureInfo, IEnumerable<string> tags, int take)
    {
        var culture = cultureInfo.TwoLetterISOLanguageName;
        var normalizedTags = NormalizeTags(tags);
        var query = $"?take={take}";

        if (normalizedTags.Length > 0)
        {
            query += $"&tags={string.Join(",", normalizedTags)}";
        }

        return new Uri(_httpClient.BaseAddress!, $"{_FeedRoute}/{culture}{query}");
    }

    private static string[] NormalizeTags(IEnumerable<string> tags)
    {
        return tags.Where(tag => !string.IsNullOrWhiteSpace(tag))
            .Select(tag => tag.Trim())
            .ToArray() ?? [];
    }

    private static Task<string> ReadContentAsStringAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
#if NET8_0_OR_GREATER
        return response.Content.ReadAsStringAsync(cancellationToken);
#else
        return response.Content.ReadAsStringAsync();
#endif
    }
}
