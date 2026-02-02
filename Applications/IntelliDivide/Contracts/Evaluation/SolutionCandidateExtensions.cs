#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    /// <summary>
    /// Extension utilities to evaluate <see cref="SolutionCandidate" />s and determine display order
    /// based on computed <see cref="SolutionCharacteristic" />s.
    /// </summary>
    public static class SolutionCandidateExtensions
    {
        public static void CalculateAndSetCharacteristicScores(this SolutionCandidate[] solutionCandidates)
        {
            var characteristicScoreWeights = GetCharacteristicScoreWeights();

            foreach (var characteristicScoreWeight in characteristicScoreWeights)
            {
                var characteristic = characteristicScoreWeight.Key;
                var weights = characteristicScoreWeight.Value;

                foreach (var solutionCandidate in solutionCandidates)
                {
                    double totalScore = 0;
                    int totalWeight = 0;

                    foreach (var weightEntry in weights)
                    {
                        var propertyName = weightEntry.Key;
                        var weight = weightEntry.Value;

                        var scoreProperty = typeof(SolutionCandidate).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                        if (scoreProperty != null && scoreProperty.PropertyType == typeof(double))
                        {
                            var scoreValue = (double)scoreProperty.GetValue(solutionCandidate);
                            totalScore += scoreValue * weight;
                            totalWeight = totalWeight += weight;
                        }
                    }

                    if (totalWeight > 0)
                    {
                        totalScore = Math.Round(totalScore / totalWeight, 2);

                        solutionCandidate.CharacteristicScores[characteristic] = totalScore;
                    }
                }
            }
        }

        /// <summary>
        /// Calculates normalized scores for every numeric property of <see cref="SolutionCandidate" />
        /// that has a corresponding property with the same name suffixed by "Score".
        /// Example: for property "Cuts" it will set "CutsScore" using lower-is-better normalization.
        /// Supports int and double properties.
        /// </summary>
        public static void CalculateAndSetPartialScores(this SolutionCandidate[] solutionCandidates)
        {
            if (solutionCandidates.Length == 0)
            {
                return;
            }

            var solutionCandidateType = typeof(SolutionCandidate);
            var publicInstanceProperties = solutionCandidateType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in publicInstanceProperties)
            {
                // Only numeric sources (int or double) are supported
                var sourcePropertyType = sourceProperty.PropertyType;
                var scoreProperty = publicInstanceProperties.FirstOrDefault(p => p.Name == sourceProperty.Name + "Score" && p.PropertyType == typeof(double) && p.CanWrite);
                if (scoreProperty == null)
                {
                    continue;
                }

                // Build min/max across candidates for the source property
                var minimumValue = double.MaxValue;
                var maximumValue = double.MinValue;
                var sourceValues = new double[solutionCandidates.Length];
                var anyNumericValueFound = false;

                for (var candidateIndex = 0; candidateIndex < solutionCandidates.Length; candidateIndex++)
                {
                    var sourceValueObject = sourceProperty.GetValue(solutionCandidates[candidateIndex]);
                    double numericValue;
                    if (sourcePropertyType == typeof(int))
                    {
                        numericValue = (int)sourceValueObject;
                    }
                    else if (sourcePropertyType == typeof(double))
                    {
                        numericValue = (double)sourceValueObject;
                    }
                    else
                    {
                        // Non-numeric types are skipped
                        sourceValues[candidateIndex] = 0;
                        continue;
                    }

                    sourceValues[candidateIndex] = numericValue;
                    if (numericValue < minimumValue) minimumValue = numericValue;
                    if (numericValue > maximumValue) maximumValue = numericValue;
                    anyNumericValueFound = true;
                }

                if (!anyNumericValueFound)
                    continue;

                // Assign scores
                for (var candidateIndex = 0; candidateIndex < solutionCandidates.Length; candidateIndex++)
                {
                    var score = ScoreLowerIsBetter(sourceValues[candidateIndex], minimumValue, maximumValue);
                    scoreProperty.SetValue(solutionCandidates[candidateIndex], score);
                }
            }
        }

        /// <summary>
        /// Converts incoming <see cref="SolutionDetails" /> to <see cref="SolutionCandidate" /> and evaluates
        /// characteristics and display order. Returns an empty sequence for null input.
        /// </summary>
        /// <param name="solutionDetails">Source solution details to evaluate.</param>
        /// <returns>Ordered evaluation results with an assigned characteristic when available.</returns>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionDetails>? solutionDetails)
        {
            var solutionCandidates = SolutionCandidates.From((solutionDetails ?? []).ToArray());

            return solutionDetails == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionCandidates);
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

            solutionCandidates.CalculateAndSetPartialScores();
            solutionCandidates.CalculateAndSetCharacteristicScores();

            var solutionCandidatesInEvaluationOrder = solutionCandidates
                .OrderBy(s => s.CalculationTime)
                .ThenByDescending(s => s.TotalCostsScore)
                .ThenByDescending(s => s.MaterialCostsScore)
                .ThenByDescending(s => s.ProductionCostsScore)
                .ToArray();

            var characteristicsByType = DetermineCharacteristics(solutionCandidatesInEvaluationOrder);

            var evaluationResults = new List<SolutionCandidateEvaluationResult>(solutionCandidates.Length);

            foreach (var solutionCandidate in solutionCandidatesInEvaluationOrder)
            {
                // Try to find a characteristic associated with the current candidate

                var assignedCharacteristic = characteristicsByType.FirstOrDefault(c => c.Value == solutionCandidate.Id);

                var characteristic = assignedCharacteristic.Key != 0 ? assignedCharacteristic.Key : SolutionCharacteristic.None;

                evaluationResults.Add(new SolutionCandidateEvaluationResult
                {
                    Id = solutionCandidate.Id,
                    Characteristic = characteristic,
                    SolutionCandidate = solutionCandidate
                });
            }

            // Order: special characteristics first (by enum value), then None
            var resultsWithCharacteristic = evaluationResults.Where(r => r.Characteristic != SolutionCharacteristic.None)
                .OrderBy(r => (int)r.Characteristic);

            var resultsWithoutCharacteristic = evaluationResults.Where(r => r.Characteristic == SolutionCharacteristic.None);

            var orderedResults = resultsWithCharacteristic.Concat(resultsWithoutCharacteristic).ToArray();

            for (var resultIndex = 0; resultIndex < orderedResults.Length; resultIndex++)
            {
                orderedResults[resultIndex].Ranking = resultIndex + 1;
            }

            return orderedResults;
        }

        /// <summary>
        /// </summary>
        /// <param name="solutionCandidate"></param>
        /// <param name="solutionCharacteristic"></param>
        /// <returns></returns>
        private static double CalculateCharacteristicScore(this SolutionCandidate solutionCandidate, SolutionCharacteristic solutionCharacteristic)
        {
            switch (solutionCharacteristic)
            {
                case SolutionCharacteristic.LowestTotalCosts:
                    return solutionCandidate.TotalCostsScore;
                case SolutionCharacteristic.LowestMaterialCosts:
                    return solutionCandidate.MaterialCostsScore;
                case SolutionCharacteristic.BalancedSolution:
                    return
                        solutionCandidate.MaterialCostsScore * 800 +
                        solutionCandidate.ProductionTimeScore * 800 +
                        solutionCandidate.WasteScore * 800 +
                        solutionCandidate.ProductionCostsScore * 800 +
                        solutionCandidate.CutsScore * 800 +
                        solutionCandidate.OffcutsTotalScore * 100
                        ;
                case SolutionCharacteristic.LittleWaste:
                    return
                        solutionCandidate.WasteScore * 1000 +
                        solutionCandidate.ProductionTimeScore * 500;
                case SolutionCharacteristic.Offcuts:
                    return
                        solutionCandidate.OffcutsTotalScore * 1000 +
                        solutionCandidate.MaterialCostsScore * 500;
                default:
                    return 0;
            }
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
            var bestCandidate = solutionCandidates.Select(s => new
                {
                    s.Id,
                    s.CalculationTime,
                    Score = s.CalculateCharacteristicScore(characteristic)
                })
                .Where(s => s.Score > 0)
                .OrderByDescending(s => s.Score)
                .ThenBy(s => s.CalculationTime)
                .FirstOrDefault();

            if (bestCandidate == null)
            {
                solutionId = Guid.Empty;
                return false;
            }

            solutionId = bestCandidate.Id;

            return true;
        }

        /// <summary>
        /// Computes all supported characteristics across the candidate set.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to evaluate.</param>
        /// <returns>Map of characteristic to the candidate that achieves it.</returns>
        private static Dictionary<SolutionCharacteristic, Guid> DetermineCharacteristics(SolutionCandidate[] solutionCandidates)
        {
            var characteristicsByType = new Dictionary<SolutionCharacteristic, Guid>();

            foreach (SolutionCharacteristic characteristicValue in Enum.GetValues(typeof(SolutionCharacteristic)))
            {
                if (DetermineCharacteristic(characteristicValue, solutionCandidates, out var matchingSolutionId))
                {
                    characteristicsByType[characteristicValue] = matchingSolutionId;
                }
            }

            return characteristicsByType;
        }

        private static IReadOnlyDictionary<SolutionCharacteristic, IReadOnlyDictionary<string, int>> GetCharacteristicScoreWeights()
        {
            var result = new Dictionary<SolutionCharacteristic, IReadOnlyDictionary<string, int>>();

            var enumType = typeof(SolutionCharacteristic);
            foreach (SolutionCharacteristic characteristic in Enum.GetValues(enumType))
            {
                var name = Enum.GetName(enumType, characteristic);
                if (name == null)
                {
                    continue;
                }

                var field = enumType.GetField(name, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                {
                    continue;
                }

                var attr = field.GetCustomAttribute<SolutionCharacteristicScoreWeightsAttribute>();
                if (attr != null)
                {
                    result[characteristic] = attr.Weights;
                }
                else
                {
                    // No attribute: store an empty dictionary for consistency
                    result[characteristic] = new Dictionary<string, int>();
                }
            }

            return result;
        }

        private static double ScoreLowerIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0)
            {
                return 0; // all zero -> no results
            }

            var range = maximumValue - minimumValue;

            if (range <= 0)
            {
                return 1000; // all equal -> perfect score
            }

            return Math.Round((1 - (value - minimumValue) / range) * 1000, 2);
        }
    }
}