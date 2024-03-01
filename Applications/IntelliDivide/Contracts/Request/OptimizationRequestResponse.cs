using System;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents the response of an optimization request
    /// </summary>
    public class OptimizationRequestResponse
    {
        /// <summary>
        /// Gets or sets the error details
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        /// Gets or sets the error text
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// Gets or sets the link to the optimization
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the optimization id
        /// </summary>
        public Guid OptimizationId { get; set; }

        /// <summary>
        /// Gets or sets the optimization status
        /// </summary>
        public OptimizationStatus OptimizationStatus { get; set; }

        /// <summary>
        /// Gets or sets the validation errors
        /// </summary>
        public OptimizationValidationError[] ValidationErrors { get; set; } = Array.Empty<OptimizationValidationError>();
    }
}