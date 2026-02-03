#nullable enable

using HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;
using HomagConnect.IntelliDivide.Contracts.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public static SolutionCandidateEvaluated[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionDetails>? solutionDetails)
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
        public static SolutionCandidateEvaluated[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionCandidate>? solutionCandidates)
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
        public static SolutionCandidateEvaluated[] DetermineCharacteristicsAndDisplayOrder(SolutionCandidate[]? solutionCandidates)
        {
            var serialized = JsonConvert.SerializeObject(solutionCandidates);
            var solutionCandidatesEvaluated = JsonConvert.DeserializeObject<SolutionCandidateEvaluated[]>(serialized) ?? [];
            
            solutionCandidatesEvaluated.CalculateAndSetPartialScores();
            solutionCandidatesEvaluated.CalculateAndSetCharacteristicScores();
            var characteristicsByType = DetermineCharacteristics(solutionCandidatesEvaluated);

            foreach (var solutionCandidate in solutionCandidatesEvaluated)
            {
                // Try to find a characteristic associated with the current candidate

                var assignedCharacteristic = characteristicsByType.FirstOrDefault(c => c.Value == solutionCandidate.Id);

                var characteristic = assignedCharacteristic.Key != 0 ? assignedCharacteristic.Key : SolutionCharacteristic.None;

                solutionCandidate.Characteristic = characteristic;
                solutionCandidate.Ranking = (int)characteristic;
            }

            var solutionCandidateEvaluateds = 
                solutionCandidatesEvaluated
                    .OrderBy(s => s.Ranking)
                    .ThenBy(s => s.CharacteristicScores[SolutionCharacteristic.BalancedSolution])
                    .ThenBy(s => s.CalculationTime);
            var ranking = 1;
            foreach (var solutionCandidateEvaluated in solutionCandidateEvaluateds)
            {
                solutionCandidateEvaluated.Ranking = ranking++;
                
            }

            return solutionCandidatesEvaluated.OrderBy(s => s.Ranking).ThenBy(s => s.CalculationTime).ToArray();
        }

        #region Private Methods

        /// <summary>
        /// Computes all supported characteristics across the candidate set.
        /// </summary>
        /// <param name="solutionCandidates">Candidates to evaluate.</param>
        /// <returns>Map of characteristic to the candidate that achieves it.</returns>
        private static Dictionary<SolutionCharacteristic, Guid> DetermineCharacteristics(SolutionCandidateEvaluated[] solutionCandidates)
        {
            var characteristicsByType = new Dictionary<SolutionCharacteristic, Guid>();

            foreach (SolutionCharacteristic characteristicValue in Enum.GetValues(typeof(SolutionCharacteristic)))
            {
                var bestSolutionId = Guid.Empty;
                var bestCharacteristicScore = 0.1;

                foreach (var solutionCandidate in solutionCandidates)
                {
                    if (solutionCandidate.CharacteristicScores.TryGetValue(characteristicValue, out var characteristicScoreValue))
                    {
                        if (characteristicScoreValue > bestCharacteristicScore)
                        {
                            bestCharacteristicScore = (int)characteristicScoreValue;
                            bestSolutionId = solutionCandidate.Id;
                        }
                    }
                }

                if (bestSolutionId != Guid.Empty)
                {
                    characteristicsByType[characteristicValue] = bestSolutionId;
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

        private static double? ScoreLowerIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0)
            {
                return null; // all zero -> no results
            }

            var range = maximumValue - minimumValue;

            if (range <= 0)
            {
                return null; // all equal -> ignore in comparison
            }

            return Math.Round((1 - (value - minimumValue) / range) * 1000, 2);
        }

        private static double? ScoreHigherIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0)
            {
                return null;
            }

            var range = maximumValue - minimumValue;

            if (range <= 0)
            {
                return null; // all equal -> -> ignore in comparison
            }

            return Math.Round((value - minimumValue) / range * 1000, 2);
        }

        private static void CalculateAndSetCharacteristicScores(this SolutionCandidateEvaluated[] solutionCandidates)
        {
            var characteristicScoreWeights = GetCharacteristicScoreWeights();

            foreach (var characteristicScoreWeight in characteristicScoreWeights)
            {
                var characteristic = characteristicScoreWeight.Key;
                var weights = characteristicScoreWeight.Value;

                var solutionCandidatesCharacteristicScores = new Dictionary<SolutionCandidateEvaluated, double>();

                foreach (var solutionCandidate in solutionCandidates)
                {
                    double? totalScore = null;

                    foreach (var weightEntry in weights)
                    {
                        var propertyName = weightEntry.Key;
                        var weight =  weightEntry.Value;
                            
                        if (solutionCandidate.PartialScores.TryGetValue(propertyName, out var scoreValue))
                        {
                            if (scoreValue != null)
                            {
                                totalScore ??= 0;
                                totalScore += scoreValue.Value * weight;
                            }
                        }
                    }

                    if (totalScore != null)
                    {
                        solutionCandidatesCharacteristicScores.Add(solutionCandidate, totalScore.Value);
                    }
                }

                foreach (var solutionCandidatesCharacteristicScore in solutionCandidatesCharacteristicScores)
                {
                    var value = solutionCandidatesCharacteristicScore.Value;
                    var minimumValue = solutionCandidatesCharacteristicScores.Values.Min();
                    var maximumValue = solutionCandidatesCharacteristicScores.Values.Max();
                    var range = maximumValue - minimumValue;

                    if (range > 0)
                    {
                        solutionCandidatesCharacteristicScore.Key.CharacteristicScores[characteristic] = ScoreHigherIsBetter(value, minimumValue, maximumValue);
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
        private static void CalculateAndSetPartialScores(this SolutionCandidateEvaluated[] solutionCandidates)
        {
            if (solutionCandidates.Length == 0)
            {
                return;
            }

            var solutionCandidateType = typeof(SolutionCandidate);
            var publicInstanceProperties = solutionCandidateType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in publicInstanceProperties)
            {
                var lowerIsBetterAttribute = sourceProperty.GetCustomAttributes(typeof(LowerIsBetterAttribute), true).FirstOrDefault();
                var higherIsBetterAttribute = sourceProperty.GetCustomAttributes(typeof(HigherIsBetterAttribute), true).FirstOrDefault();

                if (lowerIsBetterAttribute != null || higherIsBetterAttribute != null)
                {
                    var sourcePropertyType = sourceProperty.PropertyType;

                    var minimumValue = double.MaxValue;
                    var maximumValue = double.MinValue;
                    var anyNumericValueFound = false;
                    var sourceValues = new double[solutionCandidates.Length];
                    double numericValue;

                    for (var candidateIndex = 0; candidateIndex < solutionCandidates.Length; candidateIndex++)
                    {
                        var sourceValueObject = sourceProperty.GetValue(solutionCandidates[candidateIndex]);

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

                    for (var candidateIndex = 0; candidateIndex < solutionCandidates.Length; candidateIndex++)
                    {
                        if (lowerIsBetterAttribute != null)
                        {
                            var score = ScoreLowerIsBetter(sourceValues[candidateIndex], minimumValue, maximumValue);

                            if (score != null)
                            {
                                solutionCandidates[candidateIndex].PartialScores[sourceProperty.Name] = score;
                            }
                        }

                        if (higherIsBetterAttribute != null)
                        {
                            var score = ScoreHigherIsBetter(sourceValues[candidateIndex], minimumValue, maximumValue);

                            if (score != null)
                            {
                                solutionCandidates[candidateIndex].PartialScores[sourceProperty.Name] = score;
                            }

                                
                        }
                    }
                }
            }
        }

        #endregion
    }
}