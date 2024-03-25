using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the material used in the solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialEdgeband : IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [JsonProperty(Order = 2)]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the length of edge bands used.
        /// </summary>
        [JsonProperty(Order = 4)]
        public double Demand { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        [JsonProperty(Order = 5)]
        public double Costs { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}