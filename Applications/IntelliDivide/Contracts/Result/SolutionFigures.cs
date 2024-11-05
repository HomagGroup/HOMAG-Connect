using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Represents the key figures available in a solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionFigures : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the production key figures for the solution.
        /// </summary>
        [JsonProperty(Order = 10)]
        public SolutionFiguresProduction Production { get; set; } = new SolutionFiguresProduction();

        /// <summary>
        /// Gets the material key figures for the solution.
        /// </summary>
        [JsonProperty(Order = 20)]
        public SolutionFiguresMaterial Material { get; set; } = new SolutionFiguresMaterial();

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
