using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Master data describing an attribute (parameter) that can occur on a module.
    /// </summary>
    public class MDAttributeInfo
    {
        /// <summary>
        /// The attribute id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// The localized short name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// A localized description of the attribute.
        /// </summary>
        [JsonProperty("desc")]
        public string? Description { get; set; }

        /// <summary>
        /// Is main attribute
        /// </summary>
        [JsonProperty("isMain")]
        public bool? IsMain { get; set; }

        /// <summary>
        /// The localized group this attribute belongs to (used to group attributes in the UI).
        /// </summary>
        [JsonProperty("group")]
        public string? Group { get; set; }

        /// <summary>
        /// An optional URL pointing to a preview image for this attribute.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// The data type of the attribute value (can be: text, dim, real, int, bool).
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = null!;

        /// <summary>
        /// User right (can be: Master, Advanced, Simple)
        /// </summary>
        [JsonProperty("userRight")]
        public string UserRight { get; set; } = null!;

        /// <summary>
        /// Implicit relevant
        /// </summary>
        [JsonProperty("implicitRelevant")]
        public bool? ImplicitRelevant { get; set; }

        /// <summary>
        /// Pos view
        /// </summary>
        [JsonProperty("posView")]
        public string? PosView { get; set; }

        /// <summary>
        /// Sorting of the attribute
        /// </summary>
        [JsonProperty("sorting")]
        public int? Sorting { get; set; }

        /// <summary>
        /// The list of predefined selectable values for this attribute, if any.
        /// </summary>
        [JsonProperty("selections")]
        public IList<MDAttributeSelection>? Selections { get; set; }
    }
}
