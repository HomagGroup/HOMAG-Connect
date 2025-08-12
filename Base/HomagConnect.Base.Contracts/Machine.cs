using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts
{
    /// <summary>
    /// Represents a machine in the Homag Connect system.
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// Gets or sets the additional properties associated with the machine.
        /// </summary>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        [JsonProperty(Order = 2)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the machine.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string? Number { get; set; }

        /// <summary>
        /// Gets or sets the machine type.
        /// </summary>
        [JsonProperty(Order = 3)]
        public MachineType Type { get; set; }
    }
}