using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Represents a set of parameters for offcut optimization.
/// </summary>
public class OffcutParameterSet : IValidatableObject, IContainsUnitSystemDependentProperties
{
    private const int _MaterialGroupNameMaxLength = 50;

    /// <summary>
    /// Gets or sets the <see cref="IsUnassignedMaterialsGroup" /> which determines if the group contains all materials which are not manually assigned to a group.
    /// </summary>
    [JsonProperty(Order = 4)]
    public bool IsUnassignedMaterialsGroup { get; set; }

    /// <summary>
    /// Gets or sets the Large <see cref="OffcutParameters" />.
    /// </summary>
    [JsonProperty(Order = 21)]
    public OffcutParameters LargeOffcutParameters { get; set; } = new();

    /// <summary>
    /// Gets or sets the material codes for which the <see cref="OffcutParameterSet" /> is valid.
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
    public string MaterialManagerLink { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the <see cref="OffcutParameters" />.
    /// </summary>
    [JsonProperty(Order = 11)]
    public OffcutParameters OffcutParameters { get; set; } = new();

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 30)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    #region IValidatableObject Members

    /// <inheritdoc />
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
    {
        var results = new List<ValidationResult>();

        if (OffcutParameters.Enabled)
        {
            if (OffcutParameters.MinimumLength == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(OffcutParameters)} are enabled, the '{nameof(OffcutParameters)}.{nameof(OffcutParameters.MinimumLength)}' parameter must not be null.",
                    new[] { nameof(OffcutParameters.MinimumLength) }));
            }

            if (OffcutParameters.MinimumWidth == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(OffcutParameters)} are enabled, the '{nameof(OffcutParameters)}.{nameof(OffcutParameters.MinimumWidth)}' parameter must not be null.",
                    new[] { nameof(OffcutParameters.MinimumWidth) }));
            }

            if (OffcutParameters.MinimumArea == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(OffcutParameters)} are enabled, the '{nameof(OffcutParameters)}.{nameof(OffcutParameters.MinimumArea)}' parameter must not be null.",
                    new[] { nameof(OffcutParameters.MinimumArea) }));
            }

            if (OffcutParameters.Value == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(OffcutParameters)} are enabled, the '{nameof(OffcutParameters)}.{nameof(OffcutParameters.Value)}' parameter must not be null.",
                    new[] { nameof(OffcutParameters.Value) }));
            }
        }

        if (LargeOffcutParameters.Enabled)
        {
            if (LargeOffcutParameters.MinimumLength == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(LargeOffcutParameters)} are enabled, the '{nameof(LargeOffcutParameters)}.{nameof(LargeOffcutParameters.MinimumLength)}' parameter must not be null.",
                    new[] { nameof(LargeOffcutParameters.MinimumLength) }));
            }

            if (LargeOffcutParameters.MinimumWidth == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(LargeOffcutParameters)} are enabled, the '{nameof(LargeOffcutParameters)}.{nameof(LargeOffcutParameters.MinimumWidth)}' parameter must not be null.",
                    new[] { nameof(LargeOffcutParameters.MinimumWidth) }));
            }

            if (LargeOffcutParameters.MinimumArea == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(LargeOffcutParameters)} are enabled, the '{nameof(LargeOffcutParameters)}.{nameof(LargeOffcutParameters.MinimumArea)}' parameter must not be null.",
                    new[] { nameof(LargeOffcutParameters.MinimumArea) }));
            }

            if (LargeOffcutParameters.Value == null)
            {
                results.Add(new ValidationResult(
                    $"When {nameof(LargeOffcutParameters)} are enabled, the '{nameof(LargeOffcutParameters)}.{nameof(LargeOffcutParameters.Value)}' parameter must not be null.",
                    new[] { nameof(LargeOffcutParameters.Value) }));
            }
        }

        if (LargeOffcutParameters.Enabled && !OffcutParameters.Enabled)
        {
            results.Add(
                new ValidationResult(
                    $"When large offcuts are enabled, offcuts need to be enabled as well.",
                    new[] { nameof(LargeOffcutParameters.Enabled), nameof(OffcutParameters.Enabled) }));
        }
        else if (LargeOffcutParameters.Enabled && OffcutParameters.Enabled)
        {
            if (LargeOffcutParameters.MinimumLength <= OffcutParameters.MinimumLength)
            {
                results.Add(new ValidationResult(
                    $"The '{nameof(LargeOffcutParameters.MinimumLength)}' parameter of a large offcut must be greater than the value of the default offcut.",
                    new[] { nameof(LargeOffcutParameters.MinimumLength), nameof(OffcutParameters.MinimumLength) }));
            }

            if (LargeOffcutParameters.MinimumWidth <= OffcutParameters.MinimumWidth)
            {
                results.Add(new ValidationResult(
                    $"The '{nameof(LargeOffcutParameters.MinimumWidth)}' parameter of a large offcut must be greater than the value of the default offcut.",
                    new[] { nameof(LargeOffcutParameters.MinimumWidth), nameof(OffcutParameters.MinimumWidth) }));
            }

            if (LargeOffcutParameters.MinimumArea <= OffcutParameters.MinimumArea)
            {
                results.Add(new ValidationResult(
                    $"The '{nameof(LargeOffcutParameters.MinimumArea)}' parameter of a large offcut must be greater than the value of the default offcut.",
                    new[] { nameof(LargeOffcutParameters.MinimumArea), nameof(OffcutParameters.MinimumArea) }));
            }

            if (LargeOffcutParameters.Value <= OffcutParameters.Value)
            {
                results.Add(new ValidationResult(
                    $"The '{nameof(LargeOffcutParameters.Value)}' parameter of a large offcut must be greater than the value of the default offcut.",
                    new[] { nameof(LargeOffcutParameters.Value), nameof(OffcutParameters.Value) }));
            }
        }

        return results;
    }

    #endregion
}