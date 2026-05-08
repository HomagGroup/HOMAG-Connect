using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents an article on a position, including its catalog, descriptive metadata and module roots.
    /// </summary>
    public class PosArticle
    {
        /// <summary>
        /// The id of the library this article belongs to.
        /// </summary>
        [JsonProperty("libraryId")]
        public string? LibraryId { get; set; }

        /// <summary>
        /// The catalog this article belongs to.
        /// </summary>
        [JsonProperty("catalog")]
        public string? Catalog { get; set; }

        /// <summary>
        /// The unique id of the article within its catalog.
        /// </summary>
        [JsonProperty("articleId")]
        public string? ArticleId { get; set; }

        /// <summary>
        /// The display name of the article (english value).
        /// </summary>
        [JsonProperty("articleName")]
        public string? ArticleName { get; set; }

        /// <summary>
        /// The localized article name of the article keyed by culture/language code.
        /// </summary>
        [JsonProperty("articleNameLocalized")]
        public Dictionary<string, string> ArticleNameLocalized { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// A description of the article (english value).
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The localized description of the article keyed by culture/language code.
        /// </summary>
        [JsonProperty("descriptionLocalized")]
        public Dictionary<string, string> DescriptionLocalized { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// An optional URL pointing to a preview image of the article.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// The category this article belongs to (english value).
        /// </summary>
        [JsonProperty("category")]
        public string? Category { get; set; }

        /// <summary>
        /// The localized category name keyed by culture/language code.
        /// </summary>
        [JsonProperty("categoryLocalized")]
        public Dictionary<string, string> CategoryLocalized { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Indicates whether this article is a configuration dummy (sub-article) rather than a real article.
        /// </summary>
        [JsonProperty("isConfigDummy")]
        public bool? IsConfigDummy { get; set; }

        /// <summary>
        /// One or multiple roots for this article
        /// </summary>
        [JsonProperty("roots")]
        public IList<PosModuleRootData> ModuleRoots { get; set; } = new List<PosModuleRootData>();

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
