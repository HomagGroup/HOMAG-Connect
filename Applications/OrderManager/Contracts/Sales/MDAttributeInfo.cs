using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Master data describing an attribute (parameter) that can occur on a module.
    /// </summary>
    /// <example>
    /// { "id": "width", "name": "Width", "desc": "Width of the module", "isMain": true, "group": "Dimensions", "type": "dim", "userRight": "Simple", "sorting": 10, "selections": [] }
    /// </example>
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
        /// <c>true</c> if this is a main attribute of the module. Optional.
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
        /// User right required to see or change the attribute: <c>Master</c>, <c>Advanced</c> or <c>Simple</c>.
        /// </summary>
        [JsonProperty("userRight")]
        public MDAttributeUserRight UserRight { get; set; }

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
        /// Sort position of the attribute. Optional.
        /// </summary>
        [JsonProperty("sorting")]
        public int? Sorting { get; set; }

        /// <summary>
        /// The list of predefined selectable values for this attribute, if any.
        /// </summary>
        [JsonProperty("selections")]
        public IList<MDAttributeSelection>? Selections { get; set; }

        /// <summary>
        /// Additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
