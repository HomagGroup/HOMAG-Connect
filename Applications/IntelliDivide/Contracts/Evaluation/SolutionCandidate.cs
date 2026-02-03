#nullable enable

using System;

using HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;

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
    [LowerIsBetter]
    [JsonProperty(Order = 10)]
    public double CalculationTime { get; set; }

    /// <summary>
    /// Number of cuts required by this candidate.
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public int Cuts { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the solution candidate.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Guid Id { get; set; }

    /// <summary>
    /// Material costs associated with the candidate.
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public double MaterialCosts { get; set; }

    /// <summary>
    /// Total number of offcuts.
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public int OffcutsTotal { get; set; }

    /// <summary>
    /// Production costs (may be unavailable in the source, reserved for future use).
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public double ProductionCosts { get; set; }

    /// <summary>
    /// Total production time in seconds.
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public double ProductionTime { get; set; }

    /// <summary>
    /// Quantity of parts produced by this candidate.
    /// </summary>
    [HigherIsBetterAttribute]
    [JsonProperty(Order = 20)]
    public int QuantityOfParts { get; set; }

    /// <summary>
    /// Total costs (may be unavailable in the source, reserved for future use).
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public double TotalCosts { get; set; }

    /// <summary>
    /// Waste value associated with the candidate.
    /// </summary>
    [LowerIsBetter]
    [JsonProperty(Order = 20)]
    public double Waste { get; set; }
}