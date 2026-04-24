using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Represents a single selectable value for an <see cref="MDAttributeInfo"/>.
    /// </summary>
    public class MDAttributeSelection
    {
        /// <summary>
        /// Can be string, decimal, double, bool
        /// </summary>
        [JsonProperty("value")]
        public object? Value { get; set; }

        /// <summary>
        /// A localized description of this selection.
        /// </summary>
        [JsonProperty("desc")]
        public string? Description { get; set; }

        /// <summary>
        /// The localized display name of this selection.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// An optional URL pointing to a preview image for this selection.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
