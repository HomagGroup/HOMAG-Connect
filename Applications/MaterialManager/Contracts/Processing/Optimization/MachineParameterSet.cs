﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Represents the parameters for the machine optimization.
/// </summary>
public class MachineParameterSet : IValidatableObject, IContainsUnitSystemDependentProperties
{
    private const int _MaterialGroupNameMaxLength = 50;

    /// <summary>
    /// Gets or sets the <see cref="IsUnassignedMaterialsGroup" /> which determines if the group contains all materials which
    /// are not manually assigned to a group.
    /// </summary>
    [JsonProperty(Order = 4)]
    public bool IsUnassignedMaterialsGroup { get; set; }

    /// <summary>
    /// Gets or sets the material codes for which the <see cref="MachineParameterSet" /> is valid.
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
    /// Gets or sets the <see cref="MachineParameters" />.
    /// </summary>
    [JsonProperty(Order = 11)]
    public MachineParameters Parameters { get; set; } = new();

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 30)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    #region IValidatableObject Members

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return new List<ValidationResult>();
    }

    #endregion
}