using System.Globalization;

namespace HomagConnect.News.Contracts;

/// <summary>
/// Defines endpoints for reading HOMAG Connect news.
/// </summary>
public interface INewsClient
{
    /// <summary>
    /// Gets the latest news articles for the specified culture and tags.
    /// </summary>
    Task<IReadOnlyList<NewsArticle>> GetLatest(CultureInfo cultureInfo, IEnumerable<string> tags, int take, CancellationToken cancellationToken = default);
}