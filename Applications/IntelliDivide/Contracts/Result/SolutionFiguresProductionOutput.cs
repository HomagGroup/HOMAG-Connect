using System;

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the key figures for production output.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionFiguresProductionOutput : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the quantity of parts.
        /// </summary>
        [JsonProperty(Order = 1)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Gets the quantity of plus parts (optional parts).
        /// </summary>
        [JsonProperty(Order = 2)]
        public int QuantityOfPlusParts { get; set; }

        /// <summary>
        /// Gets the value of the area of the parts in m² or ft².
        /// </summary>
        [JsonProperty(Order = 3)]
        public double PartArea { get; set; }

        /// <summary>
        /// Gets the value of the area of the plus parts (optional parts) in m² or ft².
        /// </summary>
        [JsonProperty(Order = 4)]
        public double PlusPartsArea { get; set; }

        /// <summary>
        /// Gets the estimated production time.
        /// </summary>
        [JsonProperty(Order = 5)]
        public TimeSpan ProductionTime { get; set; }

        /// <summary>
        /// Gets the average production time per part in seconds.
        /// </summary>
        [JsonProperty(Order = 6)]
        public double ProductionTimePerPart { get; set; }

        /// <summary>
        /// Gets the quantity of cutting cycles.
        /// </summary>
        [JsonProperty(Order = 7)]
        public int Cycles { get; set; }

        /// <summary>
        /// Gets the number of cuts.
        /// </summary>
        [JsonProperty(Order = 8)]
        public int Cuts { get; set; }

        /// <summary>
        /// Gets the value of the cutting length in m or inch.
        /// </summary>
        [JsonProperty(Order = 9)]
        public double CuttingLength { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
