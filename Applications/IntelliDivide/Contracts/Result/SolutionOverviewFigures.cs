using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the overview figures.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOverviewFigures : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the overview figures for costs.
        /// </summary>
        [JsonProperty(Order = 30)]
        public SolutionOverviewFiguresCosts Costs { get; set; } = new SolutionOverviewFiguresCosts();

        /// <summary>
        /// Gets the overview figures for material.
        /// </summary>
        [JsonProperty(Order = 10)]
        public SolutionOverviewFiguresMaterial Material { get; set; } = new SolutionOverviewFiguresMaterial();

        /// <summary>
        /// Gets the overview figures for the production.
        /// </summary>
        [JsonProperty(Order = 20)]
        public SolutionOverviewFiguresProduction Production { get; set; } = new SolutionOverviewFiguresProduction();

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}