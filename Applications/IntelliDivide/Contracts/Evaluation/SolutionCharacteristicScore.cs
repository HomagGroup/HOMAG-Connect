#nullable enable
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Represents the score assigned to a specific solution characteristic, including its associated weights and calculated
/// value.
/// </summary>
public class SolutionCharacteristicScore
{
    /// <summary>
    /// Gets or sets the solution characteristic.
    /// </summary>
    [JsonProperty(Order = 1)]
    public SolutionCharacteristic Characteristic { get; set; }

    /// <summary>
    /// Gets or sets the weights associated with each score component.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Dictionary<string, int> Weights { get; set; } = new Dictionary<string, int>();

    /// <summary>
    /// Gets or sets the calculated score for the characteristic.
    /// </summary>
    [JsonProperty(Order = 2)]
    public double Score { get; set; }
}