using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Interfaces
{
    /// <summary>
    /// Information about the (sub-)article through which a configuration was added.
    /// </summary>
    public interface IArticle
    {
        /// <summary>
        /// Article number of the (sub-)article. Optional.
        /// </summary>
        /// <example>BU-600</example>
        [JsonProperty(Order = 2)]
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Localized short name of the (sub-)article. Optional.
        /// </summary>
        /// <example>Base unit 600</example>
        [JsonProperty(Order = 2)]
        public string? ArticleName { get; set; }

        /// <summary>
        /// Localized category of the (sub-)article. Optional.
        /// </summary>
        /// <example>Base units</example>
        [JsonProperty(Order = 2)]
        public string? ArticleCategory { get; set; }

        /// <summary>
        /// Localized description of the (sub-)article. Optional.
        /// </summary>
        /// <example>Base unit with one door</example>
        [JsonProperty(Order = 3)]
        public string? Description { get; set; }
    }
}
