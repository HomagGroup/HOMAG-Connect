using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

public class TensionTrimParameterSet : IValidatableObject, IContainsUnitSystemDependentProperties
{
    private const int _MaterialGroupNameMaxLength = 50;

    /// <summary>
    /// Gets or sets the material codes for which the <see cref="TensionTrimParameterSet" /> is valid.
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
    /// Gets or sets the <see cref="TensionTrimParameters" />.
    /// </summary>
    [JsonProperty(Order = 11)]
    public TensionTrimParameters Parameters { get; set; } = new();

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
        if (TensionTrimParameters.Enabled && TensionTrimParameters.MinimumCuttingLength == null)
        {
            results.Add(new ValidationResult(
                $"When {nameof(TensionTrimParameters)}.{nameof(TensionTrimParameters.Enabled)} is true, the '{nameof(TensionTrimParameters)}.{nameof(TensionTrimParameters.MinimumCuttingLength)}' parameter must not be null.",
                new[] { nameof(TensionTrimParameters.MinimumCuttingLength) }));
        }

        if (TensionTrimParameters.Enabled && TensionTrimParameters.MinimumDistanceBetweenTensionTrims == null)
        {
            results.Add(new ValidationResult(
                $"When {nameof(TensionTrimParameters)}.{nameof(TensionTrimParameters.Enabled)} is true, the '{nameof(TensionTrimParameters)}.{nameof(TensionTrimParameters.MinimumDistanceBetweenTensionTrims)}' parameter must not be null.",
                new[] { nameof(TensionTrimParameters.MinimumDistanceBetweenTensionTrims) }));
        }

        if (TensionTrimParameters.Enabled && TensionTrimParameters.WidthIncludingSawBladeThickness == null)
        {
            results.Add(new ValidationResult(
                $"When {nameof(TensionTrimParameters)}.{nameof(TensionTrimParameters.Enabled)} is true, the '{nameof(TensionTrimParameters)}.{nameof(TensionTrimParameters.WidthIncludingSawBladeThickness)}' parameter must not be null.",
                new[] { nameof(TensionTrimParameters.WidthIncludingSawBladeThickness) }));
        }

        return results;
    }

    #endregion
}