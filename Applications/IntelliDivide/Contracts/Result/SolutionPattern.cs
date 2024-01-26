using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides access to cutting or nesting pattern properties.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionPattern : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the board code.
        /// </summary>
        [JsonProperty(Order = 3)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets the cycle number.
        /// </summary>
        [JsonProperty(Order = 6)]
        public int CycleNumber { get; set; }

        /// <summary>
        /// Gets the the quantity of cycles the pattern will get produced.
        /// </summary>
        [JsonProperty(Order = 5)]
        public int Cycles { get; set; }

        /// <summary>
        /// Gets the pattern id.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Get the material code.
        /// </summary>
        [JsonProperty(Order = 2)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets a link to a preview image of the pattern.
        /// </summary>
        [JsonProperty(Order = 5)]
        public Uri? Preview { get; set; }

        /// <summary>
        /// Gets the total quantity in which the pattern will get produced.
        /// </summary>
        [JsonProperty(Order = 4)]
        public int Quantity { get; set; }

        /// <summary>
        ///     <see cref="IExtensibleDataObject.ExtensionData" />
        /// </summary>
        [JsonProperty(Order = 99)]
        public ExtensionDataObject? ExtensionData { get; set; }
    }
}