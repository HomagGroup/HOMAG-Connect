using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Single selectable value of an <see cref="MDAttributeInfo"/>.
    /// </summary>
    /// <example>
    /// { "value": "oak", "desc": "Natural oak", "name": "Oak", "imageUrl": "https://example.com/images/oak.png" }
    /// </example>
    public class MDAttributeSelection
    {
        /// <summary>
        /// Selectable value. Can be a string, decimal, double or boolean.
        /// </summary>
        /// <example>oak</example>
        [JsonProperty("value")]
        public object? Value { get; set; }

        /// <summary>
        /// Localized description of the selection. Optional.
        /// </summary>
        /// <example>Natural oak</example>
        [JsonProperty("desc")]
        public string? Description { get; set; }

        /// <summary>
        /// Localized display name of the selection. Optional.
        /// </summary>
        /// <example>Oak</example>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// URL of a preview image for the selection. Optional.
        /// </summary>
        /// <example>https://example.com/images/oak.png</example>
        [JsonProperty("imageUrl")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
