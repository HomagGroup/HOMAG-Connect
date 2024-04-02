using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts
{
    /// <summary>
    /// </summary>
    public class OptimizationStatusDetails
    {
#pragma warning disable S4004 // Collection properties should be readonly
        /// <summary>Error message</summary>
        [JsonProperty("error")]
        public string Error { get; set; }

        /// <summary>The time the optimization was started</summary>
        [JsonProperty("startedAt")]
        public DateTimeOffset StartedAt { get; set; }

        /// <summary>The percentage of the optimization</summary>
        [JsonProperty("statePercentage")]
        public double? StatePercentage { get; set; }

        /// <summary>The state of the optimization</summary>
        [JsonProperty("state")]
        public OptimizationStatus Status { get; set; }

        /// <summary>A list of possible validation errors</summary>
        [JsonProperty("validationErrors")]
        public IList<string> ValidationErrors { get; set; }

#pragma warning restore S4004 // Collection properties should be readonly
    }
}