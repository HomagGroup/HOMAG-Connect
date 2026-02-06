using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents the response of an optimization request
    /// </summary>
    public class OptimizationRequestResponse
    {
        /// <summary>
        /// Gets or sets the link to the optimization
        /// </summary>
        [JsonProperty(Order = 3)]
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the optimization id
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the optimization status
        /// </summary>
        [JsonProperty(Order = 2)]
        public OptimizationStatus OptimizationStatus { get; set; }

        /// <summary>
        /// Gets or sets the validation results
        /// </summary>
        [JsonProperty(Order = 4)]
        public OptimizationValidationResult[] ValidationResults { get; set;}

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}