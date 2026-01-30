#nullable enable

using System;
using System.Linq;

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
    public double CalculationTime { get; set; }

    public int Cuts { get; set; }

    public double CutsScore { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the solution candidate.
    /// </summary>
    public Guid Id { get; set; }

    public double MaterialCosts { get; set; }

    public double MaterialCostsScore { get; set; }

    public int OffcutsTotal { get; set; }

    public double OffcutsTotalScore { get; set; }

    public double ProdCostsScore { get; set; }

    public double ProductionCosts { get; set; }

    public double ProductionTime { get; set; }

    public double ProductionTimeScore { get; set; }

    public int QuantityOfParts { get; set; }

    public double TotalCosts { get; set; }

    public double TotalCostsScore { get; set; }

    public double Waste { get; set; }

    public double WasteScore { get; set; }

    /// <summary>
    /// Creates a <see cref="SolutionCandidate" /> from <see cref="SolutionDetails" />.
    /// Returns null when input is null.
    /// </summary>
    public static SolutionCandidate[] From(SolutionDetails[] solutionDetails)
    {
        var solutionCandidates = new SolutionCandidate[solutionDetails.Length];

        for (var i = 0; i < solutionDetails.Length; i++)
        {
            var solutionDetail = solutionDetails[i];
            var production = solutionDetail.KeyFigures?.Production?.Output;
            var material = solutionDetail.KeyFigures?.Material?.BoardsAndOffcuts;
            var costs = solutionDetail.Overview?.Figures?.Costs;

            var candidate = new SolutionCandidate
            {
                Id = solutionDetail.Id,
                CalculationTime = 20,
                MaterialCosts = costs?.MaterialCosts ?? 0,
                ProductionTime = production?.ProductionTime.TotalSeconds ?? 0,
                QuantityOfParts = production?.QuantityOfParts ?? 0,
                OffcutsTotal = material?.OffcutsTotal ?? 0,
                Cuts = production?.Cuts ?? 0,
                Waste = material?.Waste ?? 0
            };

            solutionCandidates[i] = candidate;
        }

        foreach (var solutionCandidate in solutionCandidates)
        {
            solutionCandidate.MaterialCostsScore = ScoreLowerIsBetter(solutionCandidate.MaterialCosts, solutionCandidates.Min(s => s.MaterialCosts), solutionCandidates.Max(s => s.MaterialCosts));
            solutionCandidate.ProductionTimeScore = ScoreLowerIsBetter(solutionCandidate.ProductionTime, solutionCandidates.Min(s => s.ProductionTime), solutionCandidates.Max(s => s.ProductionTime));
            solutionCandidate.OffcutsTotalScore = ScoreLowerIsBetter(solutionCandidate.OffcutsTotal, solutionCandidates.Min(s => s.OffcutsTotal), solutionCandidates.Max(s => s.OffcutsTotal));
            solutionCandidate.CutsScore = ScoreLowerIsBetter(solutionCandidate.Cuts, solutionCandidates.Min(s => s.Cuts), solutionCandidates.Max(s => s.Cuts));
            solutionCandidate.WasteScore = ScoreLowerIsBetter(solutionCandidate.Waste, solutionCandidates.Min(s => s.Waste), solutionCandidates.Max(s => s.Waste));
        }

        return solutionCandidates;
    }

    #region Private Methods

    private static double ScoreLowerIsBetter(double value, double min, double max)
    {
        var range = max - min;
        if (range <= 0) return 1000; // all equal -> perfect score
        return (1 - (value - min) / range) * 1000;
    }

    #endregion
}