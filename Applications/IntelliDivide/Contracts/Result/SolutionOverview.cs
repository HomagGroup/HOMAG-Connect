using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the overview data.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOverview : IExtensibleDataObject
    {
        /// <summary>
        /// Provides the overview figures.
        /// </summary>
        [JsonProperty(Order = 10)]
        public SolutionOverviewFigures Figures { get; set; } = new SolutionOverviewFigures();

        /// <summary>
        /// Gets the list of patterns.
        /// </summary>
        [JsonProperty(Order = 20)]
        public IReadOnlyCollection<SolutionPattern> Pattern { get; set; } = new List<SolutionPattern>();

        /// <summary>
        ///     <see cref="IExtensibleDataObject.ExtensionData" />
        /// </summary>
        [JsonProperty(Order = 99)]
        public ExtensionDataObject? ExtensionData { get; set; }
    }
}