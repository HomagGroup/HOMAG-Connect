#nullable enable

using System;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Key figures of a solution candidate for evaluation purposes.
/// </summary>
public class SolutionCandidate
{
    /// <summary>
    /// Time spent calculating/evaluating this candidate (static default in mapping).
    /// </summary>
    public double CalculationTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the solution candidate.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Quantity of parts produced by this candidate.
    /// </summary>
    public int QuantityOfParts { get; set; }

    #region Keyfigures and Scores

    /// <summary>
    /// Number of cuts required by this candidate.
    /// </summary>
    public int Cuts { get; set; }

    /// <summary>
    /// Normalized score for <see cref="Cuts" /> (lower is better, 0–1000).
    /// </summary>
    public double CutsScore { get; set; }

    /// <summary>
    /// Material costs associated with the candidate.
    /// </summary>
    public double MaterialCosts { get; set; }

    /// <summary>
    /// Normalized score for <see cref="MaterialCosts" /> (lower is better, 0–1000).
    /// </summary>
    public double MaterialCostsScore { get; set; }

    /// <summary>
    /// Total number of offcuts.
    /// </summary>
    public int OffcutsTotal { get; set; }

    /// <summary>
    /// Normalized score for <see cref="OffcutsTotal" /> (lower is better, 0–1000).
    /// </summary>
    public double OffcutsTotalScore { get; set; }

    /// <summary>
    /// Production costs (may be unavailable in the source, reserved for future use).
    /// </summary>
    public double ProductionCosts { get; set; }

    /// <summary>
    /// Normalized score for <see cref="ProductionCosts" /> (lower is better, 0–1000).
    /// </summary>
    public double ProductionCostsScore { get; set; }

    /// <summary>
    /// Total production time in seconds.
    /// </summary>
    public double ProductionTime { get; set; }

    /// <summary>
    /// Normalized score for <see cref="ProductionTime" /> (lower is better, 0–1000).
    /// </summary>
    public double ProductionTimeScore { get; set; }

    /// <summary>
    /// Total costs (may be unavailable in the source, reserved for future use).
    /// </summary>
    public double TotalCosts { get; set; }

    /// <summary>
    /// Normalized score for <see cref="TotalCosts" /> (lower is better, 0–1000).
    /// </summary>
    public double TotalCostsScore { get; set; }

    /// <summary>
    /// Waste value associated with the candidate.
    /// </summary>
    public double Waste { get; set; }

    /// <summary>
    /// Normalized score for <see cref="Waste" /> (lower is better, 0–1000).
    /// </summary>
    public double WasteScore { get; set; }

    /// <summary>
    /// Gets or sets the collection of scores associated with each solution characteristic.
    /// </summary>
    /// <remarks>
    /// Each entry in the dictionary maps a <see cref="SolutionCharacteristic" /> to its corresponding
    /// score. Modifying the dictionary affects the scores used for evaluating solution characteristics.
    /// </remarks>
    public Dictionary<SolutionCharacteristic, double> CharacteristicScores { get; set; } = new();

    #endregion
}