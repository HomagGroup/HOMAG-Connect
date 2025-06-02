using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Represents a set of parameters for the tension trim optimization.
/// </summary>
public class TensionTrimParameterSet : IValidatableObject
{
    private const int _MaterialGroupNameMaxLength = 50;

    /// <summary>
    /// Gets or sets the <see cref="IsUnassignedMaterialsGroup" /> which determines if the group contains all materials which are not manually assigned to a group.
    /// </summary>
    [JsonProperty(Order = 4)]
    public bool IsUnassignedMaterialsGroup { get; set; }

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
    [Obsolete("This parameter is obsolete and should not be used.")]
    public string? MaterialManagerLink { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="TensionTrimParameters" />.
    /// </summary>
    [JsonProperty(Order = 11)]
    public TensionTrimParameters Parameters { get; set; } = new();

    #region IValidatableObject Members

    /// <inheritdoc />
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
    {
        var results = new List<ValidationResult>();
        if (Parameters is { Enabled: true })
        {
            if (Parameters is { MinimumCuttingLength: null })
            {
                results.Add(new ValidationResult(
                    $"When {nameof(Parameters)}.{nameof(Parameters.Enabled)} is true, the '{nameof(Parameters)}.{nameof(Parameters.MinimumCuttingLength)}' parameter must not be null.",
                    new[] { nameof(Parameters.MinimumCuttingLength) }));
            }
            if (Parameters is { MinimumDistanceBetweenTensionTrims: null })
            {
                results.Add(new ValidationResult(
                    $"When {nameof(Parameters)}.{nameof(Parameters.Enabled)} is true, the '{nameof(Parameters)}.{nameof(Parameters.MinimumDistanceBetweenTensionTrims)}' parameter must not be null.",
                    new[] { nameof(Parameters.MinimumDistanceBetweenTensionTrims) }));
            }

            if (Parameters is { WidthIncludingSawBladeThickness: null })
            {
                results.Add(new ValidationResult(
                    $"When {nameof(Parameters)}.{nameof(Parameters.Enabled)} is true, the '{nameof(Parameters)}.{nameof(Parameters.WidthIncludingSawBladeThickness)}' parameter must not be null.",
                    new[] { nameof(Parameters.WidthIncludingSawBladeThickness) }));
            }

            if (Parameters is { TensionTrimType: TensionTrimType.SlotCenteredBetweenStrips, DistanceOfSlotFromEdge: null })
            {
                results.Add(new ValidationResult(
                    $"When {nameof(Parameters)}.{nameof(Parameters.Enabled)} is true and the {nameof(Parameters)}.{nameof(Parameters.TensionTrimType)} is SlotCenteredBetweenStrips, the '{nameof(Parameters)}.{nameof(Parameters.DistanceOfSlotFromEdge)}' parameter must not be null.",
                    new[] { nameof(Parameters.DistanceOfSlotFromEdge) }));
            }

            if (Parameters is { TensionTrimType: TensionTrimType.BridgeCenteredBetweenStrips, LengthOfTheMiddleBridge: null })
            {
                results.Add(new ValidationResult(
                    $"When {nameof(Parameters)}.{nameof(Parameters.Enabled)} is true and the {nameof(Parameters)}.{nameof(Parameters.TensionTrimType)} is BridgeCenteredBetweenStrips, the '{nameof(Parameters)}.{nameof(Parameters.LengthOfTheMiddleBridge)}' parameter must not be null.",
                    new[] { nameof(Parameters.LengthOfTheMiddleBridge) }));
            }
        }

        return results;
    }

    #endregion
}