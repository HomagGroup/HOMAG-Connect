using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts
{
    /// <summary>
    /// Represents a machine available in the HOMAG Connect system.
    /// </summary>
    /// <example>
    /// { "number": "1001", "name": "productionAssist Cutting", "type": "Cutting" }
    /// </example>
    public class Machine
    {
        /// <summary>
        /// Gets or sets additional custom properties.
        /// Any JSON properties not mapped to typed members can be captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "location": "Plant 1" }</example>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        /// <example>productionAssist Cutting</example>
        [JsonProperty(Order = 2)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the machine number or unique identifier.
        /// </summary>
        /// <example>1001</example>
        [JsonProperty(Order = 1)]
        public string? Number { get; set; }

        /// <summary>
        /// Gets or sets the machine type.
        /// Supported values: <c>Unknown</c>, <c>Cutting</c>, <c>Cnc</c>.
        /// </summary>
        /// <example>Cutting</example>
        [JsonProperty(Order = 3)]
        public MachineType Type { get; set; }
    }
}