using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the overview figures for production.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOverviewFiguresProduction : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the average book height in mm or inch.
        /// </summary>
        [JsonProperty(Order = 6)]
        public double AverageBookHeight { get; set; }

        /// <summary>
        /// Gets the quantity of cutting cycles.
        /// </summary>
        [JsonProperty(Order = 5)]
        public int Cycles { get; set; }

        /// <summary>
        /// Gets the estimated production time.
        /// </summary>
        [JsonProperty(Order = 1)]
        public TimeSpan ProductionTime { get; set; }

        /// <summary>
        /// Gets the average production time per part in seconds.
        /// </summary>
        [JsonProperty(Order = 2)]
        public double ProductionTimePerPart { get; set; }

        /// <summary>
        /// Gets the quantity of parts.
        /// </summary>
        [JsonProperty(Order = 3)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Gets the quantity of plus parts (optional parts).
        /// </summary>
        [JsonProperty(Order = 4)]
        public int QuantityOfPlusParts { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}