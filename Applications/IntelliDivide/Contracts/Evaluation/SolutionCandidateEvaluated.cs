#nullable enable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Evaluated solution candidate with assigned characteristic and scores.
/// </summary>
public class SolutionCandidateEvaluated : SolutionCandidate
{
    /// <summary>
    /// Gets or sets the characteristic assigned to the solution candidate.
    /// </summary>
    [JsonProperty(Order = 2)]
    public SolutionCharacteristic Characteristic { get; set; } = SolutionCharacteristic.None;

    /// <summary>
    /// Gets or sets the collection of scores associated with each solution characteristic.
    /// </summary>
    /// <remarks>
    /// Each entry in the dictionary maps a <see cref="SolutionCharacteristic" /> to its corresponding
    /// score. Modifying the dictionary affects the scores used for evaluating solution characteristics.
    /// </remarks>
    [JsonProperty(Order = 4)]
    public Dictionary<SolutionCharacteristic, double?> CharacteristicScores { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of partial scores associated with their corresponding keys.
    /// </summary>
    [JsonProperty(Order = 5)]
    public Dictionary<string, double?> PartialScores { get; set; } = new();

    /// <summary>
    /// Gets or sets the ranking position of the item.
    /// </summary>
    [JsonProperty(Order = 1)]
    public int Ranking { get; set; } = (int)SolutionCharacteristic.None;
}