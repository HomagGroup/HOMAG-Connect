using System;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
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

        public Uri Link { get; set; }

        public Guid OptimizationId { get; set; }

        public OptimizationStatus OptimizationStatus { get; set; }

        public OptimizationValidationError[] ValidationErrors { get; set; } = Array.Empty<OptimizationValidationError>();
    }
}