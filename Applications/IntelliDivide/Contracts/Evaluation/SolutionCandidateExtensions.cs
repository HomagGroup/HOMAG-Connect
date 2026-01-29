#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    /// <summary>
    /// Extension utilities to evaluate <see cref="SolutionCandidate" />s and determine display order
    /// based on computed <see cref="SolutionCharacteristic" />s.
    /// </summary>
    public static class SolutionCandidateExtensions
    {
        /// <summary>
        /// Converts incoming <see cref="SolutionDetails" /> to <see cref="SolutionCandidate" /> and evaluates
        /// characteristics and display order. Returns an empty sequence for null input.
        /// </summary>
        /// <param name="solutionDetails">Source solution details to evaluate.</param>
        /// <returns>Ordered evaluation results with an assigned characteristic when available.</returns>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionDetails>? solutionDetails)
        {
            return solutionDetails == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionDetails.Select(sd => (SolutionCandidate)sd!).ToArray());
        }

        /// <summary>
        /// Evaluates characteristics and display order for an enumerable of <see cref="SolutionCandidate" />.
        /// Returns an empty sequence for null input.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to evaluate.</param>
        /// <returns>Ordered evaluation results with assigned characteristics when available.</returns>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionCandidate>? solutionCandidates)
        {
            return solutionCandidates == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionCandidates.ToArray());
        }

        /// <summary>
        /// Core evaluation routine that determines characteristics and generates the display order.
        /// Special characteristics are listed first (ascending enum value), followed by entries with
        /// <see cref="SolutionCharacteristic.None" />.
        /// </summary>
        /// <param name="solutionCandidates">Array of candidates to evaluate.</param>
        /// <returns>Ordered evaluation results.</returns>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(SolutionCandidate[]? solutionCandidates)
        {
            if (solutionCandidates == null || solutionCandidates.Length == 0)
            {
                return [];
            }

            var solutionCandidatesInEvaluationOrder = solutionCandidates
                .OrderBy(s => s.CalculationTime)
                .ThenBy(s => s.TotalCosts ?? double.MaxValue)
                .ThenBy(s => s.OffcutsTotal).ToArray();

            var characteristics = DetermineCharacteristics(solutionCandidatesInEvaluationOrder);

            var results = new List<SolutionCandidateEvaluationResult>(solutionCandidates.Length);

            foreach (var candidate in solutionCandidates)
            {
                // Try to find a characteristic associated with the current candidate

                var assigned = characteristics.FirstOrDefault(c => c.Value == candidate.Id);

                var characteristic = assigned.Key != 0 ? assigned.Key : SolutionCharacteristic.None;

                results.Add(new SolutionCandidateEvaluationResult
                {
                    Id = candidate.Id,
                    Characteristic = characteristic
                });
            }

            // Order: special characteristics first (by enum value), then None
            var specials = results.Where(r => r.Characteristic != SolutionCharacteristic.None)
                .OrderBy(r => (int)r.Characteristic);
            
            var none = results.Where(r => r.Characteristic == SolutionCharacteristic.None);
           
            return specials.Concat(none).ToArray();
        }

        /// <summary>
        /// Determines whether a characteristic can be assigned and the candidate Id that fulfills it.
        /// </summary>
        /// <param name="characteristic">Characteristic to compute.</param>
        /// <param name="solutionCandidates">Candidates to search.</param>
        /// <param name="solutionId">Id of the candidate if found; otherwise <see cref="Guid.Empty" />.</param>
        /// <returns>True if the characteristic could be determined; otherwise false.</returns>
        private static bool DetermineCharacteristic(SolutionCharacteristic characteristic, SolutionCandidate[] solutionCandidates, out Guid solutionId)
        {
            solutionId = Guid.Empty;

            switch (characteristic)
            {
                case SolutionCharacteristic.None:
                    return false;
                case SolutionCharacteristic.LowestTotalCosts:
                    return EvaluateLowestTotalCosts(solutionCandidates, out solutionId);
                case SolutionCharacteristic.LowestMaterialCosts:
                    return EvaluateLowestMaterialCosts(solutionCandidates, out solutionId);
                case SolutionCharacteristic.BalancedSolution:
                    return EvaluateBalancedSolution(solutionCandidates, out solutionId);
                case SolutionCharacteristic.LittleWaste:
                    return EvaluateLittleWaste(solutionCandidates, out solutionId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(characteristic), characteristic, null);
            }
        }

        /// <summary>
        /// Computes all supported characteristics across the candidate set.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to evaluate.</param>
        /// <returns>Map of characteristic to the candidate that achieves it.</returns>
        private static Dictionary<SolutionCharacteristic, Guid> DetermineCharacteristics(SolutionCandidate[] solutionCandidates)
        {
            var result = new Dictionary<SolutionCharacteristic, Guid>();

            foreach (SolutionCharacteristic value in Enum.GetValues(typeof(SolutionCharacteristic)))
            {
                if (DetermineCharacteristic(value, solutionCandidates, out var id))
                {
                    result[value] = id;
                }
            }

            return result;
        }

        /// <summary>
        /// Finds the candidate with the lowest positive material costs, preferring lower calculation time first.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to search.</param>
        /// <param name="solutionId">Id of the best candidate if found; otherwise <see cref="Guid.Empty" />.</param>
        /// <returns>True if a candidate was found; otherwise false.</returns>
        private static bool EvaluateLowestMaterialCosts(SolutionCandidate[] solutionCandidates, out Guid solutionId)
        {
            solutionId = Guid.Empty;

            // Filter candidates with positive material costs and order deterministically
            var candidate = solutionCandidates
                .Where(s => s.MaterialCosts is > 0)
                .OrderBy(s => s.CalculationTime)
                .ThenBy(s => s.MaterialCosts!.Value)
                .FirstOrDefault();

            if (candidate == null)
            {
                return false;
            }

            solutionId = candidate.Id;
            return true;
        }

        /// <summary>
        /// Finds the candidate with the lowest positive total costs, preferring lower calculation time first.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to search.</param>
        /// <param name="lowestTotalCostsSolutionId">Id of the best candidate if found; otherwise <see cref="Guid.Empty" />.</param>
        /// <returns>True if a candidate was found; otherwise false.</returns>
        private static bool EvaluateLowestTotalCosts(SolutionCandidate[] solutionCandidates, out Guid lowestTotalCostsSolutionId)
        {
            lowestTotalCostsSolutionId = Guid.Empty;

            var candidate = solutionCandidates
                .Where(s => s.TotalCosts is > 0)
                .OrderBy(s => s.CalculationTime)
                .ThenBy(s => s.TotalCosts!.Value)
                .FirstOrDefault();

            if (candidate == null)
            {
                return false;
            }

            lowestTotalCostsSolutionId = candidate.Id;

            return true;
        }

        /// <summary>
        /// Finds the candidate with a balanced score, based on weights.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to search.</param>
        /// <param name="solutionId">Id of the best candidate if found; otherwise <see cref="Guid.Empty" />.</param>
        /// <returns>True if a candidate was found; otherwise false.</returns>
        private static bool EvaluateBalancedSolution(SolutionCandidate[] solutionCandidates, out Guid solutionId)
        {
            solutionId = Guid.Empty;

            var hasMaterialCost = solutionCandidates.Any(s => s.MaterialCosts.HasValue);
            var weights = ScoreWeightsProvider.GetBalancedWeights(hasMaterialCost);
            SolutionScoresCalculator.CalculateTotalScoreValues(solutionCandidates, weights);

            var candidates = solutionCandidates
                .OrderBy(s => s.TotalScore);
            var candidate = candidates.FirstOrDefault();

            if (candidate == null)
            {
                return false;
            }

            solutionId = candidate.Id;
            return true;
        }

        /// <summary>
        /// Finds the candidate with a little waste, based on weights.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to search.</param>
        /// <param name="solutionId">Id of the best candidate if found; otherwise <see cref="Guid.Empty" />.</param>
        /// <returns>True if a candidate was found; otherwise false.</returns>
        private static bool EvaluateLittleWaste(SolutionCandidate[] solutionCandidates, out Guid solutionId)
        {
            solutionId = Guid.Empty;

            var hasMaterialCost = solutionCandidates.Any(s => s.MaterialCosts.HasValue);
            var weights = ScoreWeightsProvider.GetScrapAccentuatedWeights(hasMaterialCost);
            SolutionScoresCalculator.CalculateTotalScoreValues(solutionCandidates, weights);

            var candidates = solutionCandidates
                .OrderBy(s => s.TotalScore);
            var candidate = candidates.FirstOrDefault();

            if (candidate == null)
            {
                return false;
            }

            solutionId = candidate.Id;
            return true;
        }
    }
}