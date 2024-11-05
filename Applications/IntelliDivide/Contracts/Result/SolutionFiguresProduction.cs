using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the key figures for the production of a solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionFiguresProduction : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the production key figures for output.
        /// </summary>
        [JsonProperty(Order = 10)]
        public SolutionFiguresProductionOutput Output { get; set; } = new SolutionFiguresProductionOutput();

        /// <summary>
        /// Gets the production key figures for handling.
        /// </summary>
        [JsonProperty(Order = 20)]
        public SolutionFiguresProductionHandling Handling { get; set; } = new SolutionFiguresProductionHandling();

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
