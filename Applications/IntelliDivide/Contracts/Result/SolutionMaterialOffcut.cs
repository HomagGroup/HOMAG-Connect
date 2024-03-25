using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the offcuts used in the solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialOffcut : IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [JsonProperty(Order = 2)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the costs of the offcut.
        /// </summary>
        [JsonProperty(Order = 7)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the demand.
        /// </summary>
        [JsonProperty(Order = 6)]
        public int Demand { get; set; }

        /// <summary>
        /// Gets or sets the length of the offcut.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 5)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the width of the offcut.
        /// </summary>
        [JsonProperty(Order = 4)]
        public double Width { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}