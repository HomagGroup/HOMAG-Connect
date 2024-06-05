using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Model for material dependent offcut parameters.
    /// </summary>
    public class BookheightParameters
    {
        /// <summary>
        /// Gets or sets whether the offcut parameters are enabled.
        /// </summary>
        [JsonProperty(Order = 10)]
        public BookheightMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the minimum area of the offcut. The value is dependent on the unit system (Metric: m², Imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 11)]
        public double? MaximumSawBladeProjection { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the offcut. The value is dependent on the unit system (Metric: mm, Imperial: inch).
        /// </summary>
        [JsonProperty(Order = 12)]
        public double? MaximumBookHeight { get; set; }
    }
}