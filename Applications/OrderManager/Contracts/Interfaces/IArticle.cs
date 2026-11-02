using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Interfaces
{
    /// <summary>
    /// Information about the (sub-)article through which a configuration was added.
    /// </summary>
    public interface IArticle
    {
        /// <summary>
        /// The article id of the (sub-)article
        /// </summary>
        [JsonProperty(Order = 2)]
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// The localized short name of the (sub-)article
        /// </summary>
        [JsonProperty(Order = 2)]
        public string? ArticleName { get; set; }

        /// <summary>
        /// The localized category of the (sub-)article
        /// </summary>
        [JsonProperty(Order = 2)]
        public string? ArticleCategory { get; set; }

        /// <summary>
        /// The localized description of the (sub-)article
        /// </summary>
        [JsonProperty(Order = 3)]
        public string? Description { get; set; }
    }
}
