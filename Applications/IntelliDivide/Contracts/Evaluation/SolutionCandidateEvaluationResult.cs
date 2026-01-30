#nullable enable

using Newtonsoft.Json;
using System;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Specifies the evaluation result for a solution candidate.
/// </summary>
public class SolutionCandidateEvaluationResult
{
    /// <summary>
    /// Gets or sets the characteristic assigned to the solution candidate.
    /// </summary>
    [JsonProperty(Order = 2)]
    public SolutionCharacteristic Characteristic { get; set; } = SolutionCharacteristic.None;

    /// <summary>
    /// Gets or sets the identifier of the solution candidate.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ranking position of the item.
    /// </summary>
    [JsonProperty(Order = 1)]
    public int Ranking { get; set; }

    /// <summary>
    /// Gets or sets the solution candidate details.
    /// </summary>
    [JsonProperty(Order = 4)]
    public SolutionCandidate SolutionCandidate { get; set; }
}