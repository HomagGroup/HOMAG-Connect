using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HomagConnect.IntelliDivide.Contracts.Request;

/// <summary>
/// Validates that an optimization name does not contain characters that are invalid in file names.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class ValidOptimizationNameAttribute : ValidationAttribute
{
    private static readonly char[] _InvalidCharacters = ['\\', '/', ':', '*', '?', '"', '<', '>', '|'];

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string name)
        {
            return ValidationResult.Success;
        }

        var invalidCharacters = name
            .Where(_InvalidCharacters.Contains)
            .Distinct()
            .Select(character => $"'{character}'")
            .ToArray();

        if (invalidCharacters.Length == 0)
        {
            return ValidationResult.Success;
        }

        var invalidCharacterList = string.Join(", ", invalidCharacters);
        var errorMessage = string.Format(Resources.OptimizationName_InvalidCharacters, invalidCharacterList);
        return new ValidationResult(errorMessage);
    }
}
