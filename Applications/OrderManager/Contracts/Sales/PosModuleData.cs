using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Module within an article, including its sub modules and attributes.
    /// </summary>
    /// <example>
    /// { "id": "m1", "name": "Carcase", "modules": [], "attributes": [ { "id": "width", "value": 600, "isInput": true } ] }
    /// </example>
    public class PosModuleData
    {
        /// <summary>
        /// Unique id of the module. Stays the same across multiple calculation calls.
        /// </summary>
        /// <example>m1</example>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Name of the module. Not unique. Required to calculate the article.
        /// See also <see cref="MDModuleInfo"/> for more info about modules.
        /// </summary>
        /// <example>Carcase</example>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Sub modules of the module. Optional.
        /// </summary>
        [JsonProperty("modules")]
        public IList<PosModuleData>? Modules { get; set; }

        /// <summary>
        /// Parameters of the module. Optional.
        /// </summary>
        [JsonProperty("attributes")]
        public IList<PosModuleAttribute>? Attributes { get; set; }

        /// <summary>
        /// Additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}
