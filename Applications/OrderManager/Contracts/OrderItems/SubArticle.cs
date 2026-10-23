using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Information about the sub-article (config dummy article) through which a configuration was added.
    /// </summary>
    public class SubArticle : ISupportsAdditionalData
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
        /// Gets or sets the additional data.
        /// </summary>
        [JsonProperty(Order = 998)]
        public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
