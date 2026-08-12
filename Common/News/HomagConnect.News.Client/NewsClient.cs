using System.Globalization;
using System.Net.Http.Headers;

using HomagConnect.News.Contracts;

using Newtonsoft.Json;

namespace HomagConnect.News.Client;

/// <summary>
/// Represents the HOMAG Connect News client.
/// </summary>
public sealed class NewsClient : INewsClient
{
    private const string _FeedRoute = "api/news/feed";

    /// <summary>
    /// Cache lifetime matching the Azure Front Door CDN TTL for the news feed.
    /// </summary>
    private static readonly int _FeedCacheMaxAgeSeconds = (int)TimeSpan.FromHours(8).TotalSeconds;

    private static readonly Uri _DefaultBaseUri = new("https://news-preview.homag.cloud");

    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of <see cref="NewsClient" /> using the default HOMAG Connect News base URI.
    /// </summary>
    public NewsClient() : this(_DefaultBaseUri) { }

    /// <summary>
    /// Initializes a new instance of <see cref="NewsClient" /> using a custom base URI.
    /// </summary>
    public NewsClient(Uri baseUri)
    {
        _httpClient = new HttpClient { BaseAddress = baseUri };
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NewsClient" /> using a pre-configured <see cref="HttpClient" />.
    /// </summary>
    public NewsClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<NewsArticle>> GetLatest(
        CultureInfo cultureInfo,
        IEnumerable<string> tags,
        int take,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var culture = cultureInfo.TwoLetterISOLanguageName;
            var tagList = tags?.ToList() ?? [];

            var query = $"?take={take}";

            if (tagList.Count > 0)
            {
                query += $"&tags={string.Join(",", tagList)}";
            }

            var uri = new Uri(_httpClient.BaseAddress!, $"{_FeedRoute}/{culture}{query}");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.CacheControl = new CacheControlHeaderValue { MaxAge = TimeSpan.FromSeconds(_FeedCacheMaxAgeSeconds) };
            var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var articles = JsonConvert.DeserializeObject<NewsArticle[]>(json) ?? Array.Empty<NewsArticle>();

            return articles;
        }
        catch
        {
            return Array.Empty<NewsArticle>();
        }
    }
}