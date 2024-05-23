using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Request
{
    /// <summary>
    /// Represents the validation errors for an optimization
    /// This class was created in order to avoid creating custom converters for ValidationResults because the class
    /// "ValidationResult" doesn't have a default constructor and cannot be deserialized in our context.
    /// </summary>
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
        [JsonConstructor]
        public OptimizationValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }
    }
}
