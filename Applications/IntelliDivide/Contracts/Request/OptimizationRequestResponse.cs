using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents the response of an optimization request
    /// </summary>
    public class OptimizationRequestResponse : IExtensibleDataObject
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

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }

        #endregion
    }
}