using System;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

public class OffcutParameterSet
{
    private const int _MaterialGroupNameMaxLength = 50;

    /// <summary>
    /// Gets or sets the material codes for which the <see cref="Parameters" /> or valid.
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
    /// Gets or sets the <see cref="OffcutParameters" />.
    /// </summary>
    [JsonProperty(Order = 3)]
    public OffcutParameters? Parameters { get; set; }
}