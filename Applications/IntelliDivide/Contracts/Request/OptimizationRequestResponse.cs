#nullable enable
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents the response returned after submitting an optimization request. Contains the optimization
    /// identifier, its current status, a direct link to the optimization in the application, and any validation results.
    /// </summary>
    /// <example>
    /// {
    ///   "optimizationId": "a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    ///   "optimizationStatus": "New",
    ///   "link": "https://intelliDivide.homag.cloud/optimizations/a1b2c3d4-e5f6-7890-abcd-ef1234567890",
    ///   "validationResults": []
    /// }
    /// </example>
    public class OptimizationRequestResponse
    {
        /// <summary>
        /// Gets or sets the direct URL to the optimization in the intelliDivide application.
        /// </summary>
        /// <example>https://intelliDivide.homag.cloud/optimizations/a1b2c3d4-e5f6-7890-abcd-ef1234567890</example>
        [JsonProperty(Order = 3)]
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the created optimization. Use this value to
        /// retrieve status, start the optimization, or download results.
        /// </summary>
        /// <example>a1b2c3d4-e5f6-7890-abcd-ef1234567890</example>
        [JsonProperty(Order = 1)]
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the current status of the optimization at the time of the response.
        /// Typical initial values: <c>New</c> (when action is <c>ImportOnly</c>) or <c>Started</c> (when action is <c>Optimize</c> or <c>Send</c>).
        /// </summary>
        /// <example>New</example>
        [JsonProperty(Order = 2)]
        public OptimizationStatus OptimizationStatus { get; set; }

        /// <summary>
        /// Gets or sets the validation results. When this array contains entries, the request data has
        /// issues that must be corrected before the optimization can run successfully.
        /// </summary>
        /// <example>[]</example>
        [JsonProperty(Order = 4)]
        public OptimizationValidationResult[]? ValidationResults { get; set;}

        /// <summary>
        /// Gets or sets additional custom properties. Any JSON properties not mapped to a typed member
        /// are captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "customField1": "value1" }</example>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}