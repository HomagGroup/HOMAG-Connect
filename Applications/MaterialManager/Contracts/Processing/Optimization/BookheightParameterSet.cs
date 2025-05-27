using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Represents a set of parameters for the book height optimization.
/// </summary>
public class BookHeightParameterSet : IValidatableObject
{
    private const int _MaterialGroupNameMaxLength = 50;

    /// <summary>
    /// Gets or sets the <see cref="BookHeightParameters" />.
    /// </summary>
    [JsonProperty(Order = 11)]
    public BookHeightParameters BookHeightParameters { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="IsUnassignedMaterialsGroup" /> which determines if the group contains all materials which are not manually assigned to a group.
    /// </summary>
    [JsonProperty(Order = 4)]
    public bool IsUnassignedMaterialsGroup { get; set; }

    /// <summary>
    /// Gets or sets the material codes for which the <see cref="BookHeightParameterSet" /> is valid.
    /// </summary>
    [JsonProperty(Order = 2)]
    [MinLength(1)]
    public string[] MaterialCodes { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the name of the material group.
    /// </summary>
    [Key]
    [JsonProperty(Order = 1)]
    [Required]
    [StringLength(_MaterialGroupNameMaxLength, MinimumLength = 1)]
    public string MaterialGroupName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the <see cref="MaterialManagerLink" />.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Obsolete("This parameter is obsolete and should not be used.")]
    public string? MaterialManagerLink { get; set; }

    #region IValidatableObject Members

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (BookHeightParameters is { Mode: BookHeightMode.SpecificValue, MaximumBookHeight: null })
        {
            results.Add(new ValidationResult(
                $"When {nameof(BookHeightParameters)}.{nameof(BookHeightParameters.Mode)} is {nameof(BookHeightMode.SpecificValue)} , the '{nameof(BookHeightParameters)}.{nameof(BookHeightParameters.MaximumBookHeight)}' parameter must not be null.",
                new[] { nameof(BookHeightParameters.MaximumBookHeight) }));
        }

        if (BookHeightParameters is { Mode: BookHeightMode.MaximumSawBladeProjectionDeduction, MaximumSawBladeProjectionDeduction: null })
        {
            results.Add(new ValidationResult(
                $"When {nameof(BookHeightParameters)}.{nameof(BookHeightParameters.Mode)} is {nameof(BookHeightMode.MaximumSawBladeProjectionDeduction)} , the '{nameof(BookHeightParameters)}.{nameof(BookHeightParameters.MaximumSawBladeProjectionDeduction)}' parameter must not be null.",
                new[] { nameof(BookHeightParameters.MaximumSawBladeProjectionDeduction) }));
        }

        if (BookHeightParameters is { Mode: BookHeightMode.LimitedBookHeight, LimitedBookHeight: null })
        {
            results.Add(new ValidationResult(
                $"When {nameof(BookHeightParameters)}.{nameof(BookHeightParameters.Mode)} is {nameof(BookHeightMode.LimitedBookHeight)} , the '{nameof(BookHeightParameters)}.{nameof(BookHeightParameters.LimitedBookHeight)}' parameter must not be null.",
                new[] { nameof(BookHeightParameters.LimitedBookHeight) }));
        }

        return results;
    }

    #endregion
}