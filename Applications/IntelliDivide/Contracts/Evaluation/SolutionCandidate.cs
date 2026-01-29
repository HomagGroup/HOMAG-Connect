#nullable enable

using System;

using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Key figures of a solution candidate for evaluation purposes.
/// </summary>
public class SolutionCandidate
{
    /// <summary>
    /// Time spent calculating/evaluating this candidate (static default in mapping).
    /// </summary>
    public double CalculationTime { get; private set; }

    /// <summary>
    /// Number of cuts required by the candidate.
    /// </summary>
    [PartialScoreType(PartialScoreType.Cuts)]
    public int Cuts { get; private set; }

    /// <summary>
    /// Unique identifier of the solution candidate (mirrors <see cref="SolutionDetails.Id" />).
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Material costs if available from overview figures.
    /// </summary>
    [PartialScoreType(PartialScoreType.MaterialCost)]
    public double? MaterialCosts { get; private set; }

    /// <summary>
    /// Total number of offcuts.
    /// </summary>
    public int OffcutsTotal { get; private set; }

    /// <summary>
    /// Production costs if available (not mapped currently).
    /// </summary>
    [PartialScoreType(PartialScoreType.ProductionCost)]
    public double? ProductionCosts { get; private set; }

    /// <summary>
    /// Total production time in seconds for the candidate.
    /// </summary>
    [PartialScoreType(PartialScoreType.ProductionTime)]
    public double ProductionTime { get; private set; }

    /// <summary>
    /// Quantity of parts produced.
    /// </summary>
    public int QuantityOfParts { get; private set; }

    /// <summary>
    /// Quantity of plus parts (additional parts beyond required).
    /// </summary>
    [PartialScoreType(PartialScoreType.NumberOfPlusParts)]
    public int QuantityOfPlusParts { get; private set; }

    /// <summary>
    /// Total costs if available (not mapped currently).
    /// </summary>
    [PartialScoreType(PartialScoreType.TotalCost)]
    public double? TotalCosts { get; private set; }

    /// <summary>
    /// Waste for the solution.
    /// </summary>
    [PartialScoreType(PartialScoreType.PercentageOfScrap)]
    public double Waste { get; private set; }

    /// <summary>
    /// Total score calculated from weighted partial scores.
    /// </summary>
    public double TotalScore { get; internal set; }

    /// <summary>
    /// Creates a <see cref="SolutionCandidate" /> from <see cref="SolutionDetails" />.
    /// Returns null when input is null.
    /// </summary>
    public static SolutionCandidate? From(SolutionDetails? solutionDetails)
    {
        if (solutionDetails == null)
            return null;

        var id = solutionDetails.Id;
        var production = solutionDetails.KeyFigures?.Production?.Output;
        var material = solutionDetails.KeyFigures?.Material?.BoardsAndOffcuts;
        var costs = solutionDetails.Overview?.Figures?.Costs;

        return new SolutionCandidate
        {
            Id = id,
            ProductionTime = production?.ProductionTime.TotalSeconds ?? 0d,
            Cuts = production?.Cuts ?? 0,
            QuantityOfParts = production?.QuantityOfParts ?? 0,
            QuantityOfPlusParts = production?.QuantityOfPlusParts ?? 0,
            OffcutsTotal = material?.OffcutsTotal ?? 0,
            MaterialCosts = costs?.MaterialCosts,
            CalculationTime = 20.0,
            Waste = material?.Waste ?? 0
        };
    }

    /// <summary>
    /// Implicit conversion from <see cref="SolutionDetails" /> to <see cref="SolutionCandidate" />.
    /// Delegates to <see cref="From(SolutionDetails?)" /> and returns null for null input.
    /// </summary>
    public static implicit operator SolutionCandidate?(SolutionDetails? solutionDetails) => From(solutionDetails);
}