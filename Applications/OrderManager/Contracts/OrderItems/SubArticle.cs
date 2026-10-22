using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Information about the sub-article (config dummy article) through which a configuration was added.
    /// </summary>
    public class SubArticle
    {
        /// <summary>
        /// The article id of the sub-article
        /// </summary>
        [JsonProperty(Order = 1)]
        public string? Id { get; set; }

        /// <summary>
        /// The localized short name of the sub-article
        /// </summary>
        [JsonProperty(Order = 2)]
        public string? Name { get; set; }

        /// <summary>
        /// The localized description of the sub-article
        /// </summary>
        [JsonProperty(Order = 3)]
        public string? Description { get; set; }

        /// <summary>
        /// The image url of the sub-article
        /// </summary>
        [JsonProperty(Order = 4)]
        public string? Image { get; set; }
    }
}
