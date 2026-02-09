#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Extensions
{
    /// <summary>
    /// Extension methods for SolutionDetails that normalize missing or inconsistent data
    /// and perform recursive data annotations validation.
    /// </summary>
    public static class SolutionDetailsExtensions
    {
        /// <summary>
        /// Validates a sequence of SolutionDetails instances by invoking Validate(SolutionDetails) on each.
        /// Throws when any item fails validation.
        /// </summary>
        /// <param name="solutionDetails">Sequence of solution details to validate.</param>
        public static void Validate(this IEnumerable<SolutionDetails> solutionDetails)
        {
            if (solutionDetails == null) throw new ArgumentNullException(nameof(solutionDetails));

            foreach (var solutionDetail in solutionDetails)
            {
                solutionDetail.Validate();
            }
        }

        /// <param name="solutionDetails">The solution details to normalize and validate.</param>
        extension(SolutionDetails? solutionDetails)
        {
            /// <summary>
            /// Normalizes derived key figures (e.g., pattern count and average),
            /// fixes empty laminate strings to null, and validates with data annotations.
            /// Returns aggregated ValidationResult entries via the out parameter.
            /// </summary>
            /// <param name="validationResult">Aggregated validation results produced during validation.</param>
            /// <returns>True if validation is successful; otherwise false.</returns>
            public bool TryValidate(out List<ValidationResult> validationResult)
            {
                validationResult = [];

                if (solutionDetails == null)
                {
                    validationResult.Add(new ValidationResult("SolutionDetails instance is null."));
                    return false;
                }

                // Normalize PatternCount and QuantityPerPatternAverage safely
                var handling = solutionDetails.KeyFigures?.Production?.Handling;
                var patterns = solutionDetails.Overview?.Pattern?.ToArray() ?? Array.Empty<SolutionPattern>();

                var patternCount = patterns.Length;

                if (handling != null)
                {
                    // TODO: Remove temporary pattern count correction once the root cause of missing patterns is resolved

                    handling.PatternCount = patternCount;

                    var patternsQuantityTotal = patterns.Sum(p => p?.Quantity ?? 0);
                    handling.QuantityPerPatternAverage = patternCount > 0
                        ? patternsQuantityTotal / (double)patternCount
                        : 0;
                }

                // Normalize laminate properties: empty strings -> null
                var parts = solutionDetails.Parts;
                foreach (var solutionPart in parts)
                {
                    // TODO: Remove temporary workaround for empty laminate strings once the root cause of missing laminate values is resolved

                    if (solutionPart == null) continue;

                    if (string.IsNullOrWhiteSpace(solutionPart.LaminateBottom))
                    {
                        solutionPart.LaminateBottom = null;
                    }

                    if (string.IsNullOrWhiteSpace(solutionPart.LaminateTop))
                    {
                        solutionPart.LaminateTop = null;
                    }
                }

                // DataAnnotations recursive validation
                return DataAnnotationsValidator.TryValidateObjectRecursive(solutionDetails, out validationResult);
            }

            /// <summary>
            /// Validates the SolutionDetails object, normalizing derived values as needed.
            /// Throws ValidationException with aggregated error details when validation fails.
            /// </summary>
            /// <exception cref="ArgumentNullException">Thrown when solutionDetails is null.</exception>
            /// <exception cref="ValidationException">Thrown when validation fails; includes aggregated error messages.</exception>
            public void Validate()
            {
                if (solutionDetails == null)
                {
                    throw new ArgumentNullException(nameof(solutionDetails));
                }

                var isValid = solutionDetails.TryValidate(out var validationResults);

                if (!isValid)
                {
                    var messages = validationResults?
                        .Where(v => !string.IsNullOrWhiteSpace(v.ErrorMessage))
                        .Select(v => v.ErrorMessage)
                        .ToArray() ?? Array.Empty<string>();

                    var details = messages.Length > 0 ? string.Join("; ", messages) : "No validation details available.";
                    throw new ValidationException($"Validation failed for solution candidate {solutionDetails.Id}. Details: {details}");
                }
            }
        }
    }
}