using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents a validation error returned when an optimization request contains invalid data.
    /// Inspect <see cref="ValidationResult.ErrorMessage" /> for the error detail and
    /// <see cref="ValidationResult.MemberNames" /> for the affected properties.
    /// </summary>
    /// <example>{ "errorMessage": "The material code 'UNKNOWN' is not available." }</example>
    public class OptimizationValidationResult : ValidationResult
    {
        /// <inheritdoc />
        protected OptimizationValidationResult(ValidationResult validationResult) : base(validationResult)
        {
        }

        /// <inheritdoc />
        [JsonConstructor]
        public OptimizationValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        /// <inheritdoc />
        public OptimizationValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }
    }
}
