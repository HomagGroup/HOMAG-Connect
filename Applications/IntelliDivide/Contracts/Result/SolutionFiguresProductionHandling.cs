using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the key figures for production handling.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionFiguresProductionHandling : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the quantity of recuts.
        /// </summary>
        [JsonProperty(Order = 1)]
        public int Recuts { get; set; }

        /// <summary>
        /// Gets the quantity of headcuts.
        /// </summary>
        [JsonProperty(Order = 2)]
        public int HeadCuts { get; set; }

        /// <summary>
        /// Gets the average book height in mm or inch.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double AverageBookHeight { get; set; }

        /// <summary>
        /// Gets the maximum book height in mm or inch.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double MaxBookHeight { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
