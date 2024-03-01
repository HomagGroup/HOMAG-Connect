using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the overview figures for costs.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOverviewFiguresCosts : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the costs of boards and offcuts in the currency of the subscription.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double? CostsOfBoardsPlusOffcuts { get; set; }

        /// <summary>
        /// Gets the costs of edgebands in the currency of the subscription.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double? CostsOfEdgebands { get; set; }

        /// <summary>
        /// Gets the total material costs in the currency of the subscription.
        /// </summary>
        [JsonProperty(Order = 1)]
        public double? MaterialCosts { get; set; }

        /// <summary>
        /// Gets the average material costs per part in the currency of the subscription.
        /// </summary>
        [JsonProperty(Order = 2)]
        public double? MaterialCostsPerPart { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}