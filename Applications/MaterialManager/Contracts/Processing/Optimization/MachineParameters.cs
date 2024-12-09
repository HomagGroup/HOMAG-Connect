using Newtonsoft.Json;

using System;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization;

/// <summary>
/// Model for material dependent machine parameters.
/// </summary>
public class MachineParameters
{
    /// <summary>
    /// Gets or sets the machine parameter.
    /// </summary>
    [JsonProperty(Order = 10)]
    [Obsolete("This parameter is obsolete because it was removed. Please use MaterialParameterGroup")]
    public string? MachineParameter { get; set; }

    /// <summary>
    /// Gets or sets the material parameter group.
    /// </summary>
    [JsonProperty(Order = 11)]
    public string? MaterialParameterGroup { get; set; }
}