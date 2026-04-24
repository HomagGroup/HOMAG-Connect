using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Master data container holding the available modules and attributes used to describe articles.
    /// </summary>
    public class MasterData
    {
        /// <summary>
        /// The id of the library this master data belongs to.
        /// </summary>
        public string LibraryId { get; set; } = null!;

        /// <summary>
        /// The available module definitions.
        /// </summary>
        [JsonProperty("modules")]
        public IList<MDModuleInfo> Modules { get; set; } = new List<MDModuleInfo>();

        /// <summary>
        /// The available attribute definitions.
        /// </summary>
        [JsonProperty("attributes")]
        public IList<MDAttributeInfo> Attributes { get; set; } = new List<MDAttributeInfo>();

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
