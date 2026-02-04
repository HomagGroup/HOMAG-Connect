#nullable enable

using System;
using System.Collections.Generic;

using HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Key figures of a solution candidate for evaluation purposes.
/// </summary>
public class SolutionCandidate
{
    /// <summary>
    /// Time spent calculating/evaluating this candidate (static default in mapping).
    /// </summary>
    [JsonProperty(Order = 10)]
    public double CalculationTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the solution candidate.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the collection of key figures associated with their corresponding keys.
    /// </summary>
    public Dictionary<SolutionKeyFigure, double?> KeyFigures { get; set; } = new();
}