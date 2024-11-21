using Newtonsoft.Json;

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
    public string? MachineParameter { get; set; }
}