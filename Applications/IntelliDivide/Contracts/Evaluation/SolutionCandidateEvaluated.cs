#nullable enable

using System;
using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Evaluated solution candidate with assigned characteristic and scores.
/// </summary>
public class SolutionCandidateEvaluated
{
    /// <summary>
    /// Initializes a new instance of the SolutionCandidateEvaluated class using the specified solution candidate.
    /// </summary>
    /// <param name="candidate">
    /// The solution candidate whose identifier, calculation time, and key figures are used to initialize the new
    /// instance. Cannot be null.
    /// </param>
    public SolutionCandidateEvaluated(SolutionCandidate candidate)
    {
        Id = candidate.Id;
        CalculationTime = candidate.CalculationTime;
        KeyFigures = candidate.KeyFigures;
    }

    /// <summary>
    /// Time spent calculating/evaluating this candidate (static default in mapping).
    /// </summary>
    [JsonProperty(Order = 10)]
    public double CalculationTime { get; set; }

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
    /// Gets or sets the unique identifier of the solution candidate.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the collection of partial scores associated with their corresponding keys.
    /// </summary>
    [JsonProperty(Order = 6)]
    public Dictionary<SolutionKeyFigure, double?> KeyFigures { get; set; }

    /// <summary>
    /// Gets or sets the collection of partial scores associated with their corresponding keys.
    /// </summary>
    [JsonProperty(Order = 5)]
    public Dictionary<SolutionKeyFigure, double?> PartialScores { get; set; } = new();

    /// <summary>
    /// Gets or sets the ranking position of the item.
    /// </summary>
    [JsonProperty(Order = 1)]
    public int Ranking { get; set; } = (int)SolutionCharacteristic.None;
}