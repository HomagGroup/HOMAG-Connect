using System.Globalization;

namespace HomagConnect.News.Contracts
{
    /// <summary>
    /// Represents a news article.
    /// </summary>
    public sealed class NewsArticle
    {
        /// <summary>
        /// Gets the unique identifier of the article.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// Gets the article title.
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// Gets the URL to the full article.
        /// </summary>
        public required Uri Url { get; init; }

        /// <summary>
        /// Gets the article lead text.
        /// </summary>
        public required string Lead { get; init; }

        /// <summary>
        /// Gets the article category.
        /// </summary>
        public required string Category { get; init; }

        /// <summary>
        /// Gets the tags assigned to the article.
        /// </summary>
        public required string[] Tags { get; init; }

        /// <summary>
        /// Gets the publication date of the article.
        /// </summary>
        public required DateTimeOffset Date { get; init; }

        /// <summary>
        /// Gets or sets the culture of the article content.
        /// </summary>
        public required CultureInfo Culture { get; init; }
    }
}
