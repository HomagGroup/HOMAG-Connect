#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Extensions
{
    /// <summary>
    /// Extensions to evaluate <see cref="SolutionCandidate" />s and produce ranked
    /// <see cref="SolutionCandidateEvaluationResult" />s.
    /// </summary>
    public static class SolutionCandidateExtensions
    {
        /// <summary>
        /// Converts incoming <see cref="SolutionDetails" /> to <see cref="SolutionCandidate" /> and evaluates.
        /// </summary>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionDetails>? solutionDetails)
        {
            var solutionCandidates = SolutionCandidates.From((solutionDetails ?? []).ToArray());
            return solutionDetails == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionCandidates);
        }

        /// <summary>
        /// Evaluates characteristics and display order for an enumerable of <see cref="SolutionCandidate" />.
        /// </summary>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionCandidate>? solutionCandidates)
        {
            return solutionCandidates == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionCandidates.ToArray());
        }

        /// <summary>
        /// Core evaluation: compute partial scores, characteristic scores, assign characteristics, then rank.
        /// </summary>
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(SolutionCandidate[]? solutionCandidates)
        {
            if (solutionCandidates == null || solutionCandidates.Length == 0)
            {
                return [];
            }

            var evaluated = solutionCandidates.Select(s => new SolutionCandidateEvaluationResult(s)).ToArray();

            evaluated.CalculateAndSetPartialScores();
            evaluated.CalculateAndSetCharacteristicScores();
            evaluated.AssignCharacteristics();
            evaluated.CalculateRanking();

            return evaluated.OrderBy(s => s.Ranking).ToArray();
        }

        #region Private Methods

        /// <param name="evaluated">The array of evaluated solution candidate results.</param>
        extension(SolutionCandidateEvaluationResult[] evaluated)
        {
            /// <summary>
            /// Assign primary characteristic and additional ones the candidate wins.
            /// </summary>
            private void AssignCharacteristics()
            {
                foreach (var candidate in evaluated)
                {
                    var wins = new List<SolutionCharacteristic>();

                    foreach (SolutionCharacteristic characteristic in Enum.GetValues(typeof(SolutionCharacteristic)))
                    {
                        var best = evaluated
                            .Where(s => s.CharacteristicScores.TryGetValue(characteristic, out var score) && score.HasValue)
                            .OrderByDescending(s => s.CharacteristicScores[characteristic])
                            .FirstOrDefault();

                        if (best != null && best.Id == candidate.Id)
                        {
                            wins.Add(characteristic);
                        }
                    }

                    candidate.Characteristic = wins.Count > 0 ? wins[0] : SolutionCharacteristic.None;
                    if (wins.Count > 1) candidate.CharacteristicsInAddition = wins.Skip(1).ToArray();
                }
            }

            /// <summary>
            /// Rank by primary characteristic (enum order), then BalancedSolution score (desc), then calculation time.
            /// </summary>
            private void CalculateRanking()
            {
                var ordered =
                    evaluated
                        .OrderBy(s => s.Characteristic)
                        .ThenByDescending(s => s.CharacteristicScores.TryGetValue(SolutionCharacteristic.BalancedSolution, out var score) && score.HasValue ? score.Value : 0d)
                        .ThenBy(s => s.CalculationTime);

                var ranking = 1;
                foreach (var candidate in ordered)
                {
                    candidate.Ranking = ranking++;
                }
            }
        }

        /// <summary>
        /// Cache: score weights by characteristic (keyed by SolutionKeyFigure).
        /// </summary>
        private static IReadOnlyDictionary<SolutionCharacteristic, IReadOnlyDictionary<SolutionKeyFigure, int>>? _ScoreWeightsByCharacteristic;

        /// <summary>
        /// Cache: key figures referenced by at least one characteristic.
        /// </summary>
        private static SolutionKeyFigure[]? _KeyFiguresUsedForAtLeastOneSolutionCharacteristic;

        /// <summary>
        /// Key figures referenced by at least one characteristic weight configuration.
        /// </summary>
        private static IEnumerable<SolutionKeyFigure> KeyFiguresUsedForAtLeastOneSolutionCharacteristic
        {
            get
            {
                _KeyFiguresUsedForAtLeastOneSolutionCharacteristic ??= ScoreWeightsByCharacteristic
                    .SelectMany(c => c.Value.Keys)
                    .Distinct()
                    .ToArray();

                return _KeyFiguresUsedForAtLeastOneSolutionCharacteristic;
            }
        }

        /// <summary>
        /// Lazy loader for weights indexed by <see cref="SolutionCharacteristic" />.
        /// </summary>
        private static IReadOnlyDictionary<SolutionCharacteristic, IReadOnlyDictionary<SolutionKeyFigure, int>> ScoreWeightsByCharacteristic
        {
            get
            {
                if (_ScoreWeightsByCharacteristic == null)
                {
                    var result = new Dictionary<SolutionCharacteristic, IReadOnlyDictionary<SolutionKeyFigure, int>>();

                    var enumType = typeof(SolutionCharacteristic);
                    foreach (SolutionCharacteristic characteristic in Enum.GetValues(enumType))
                    {
                        var name = Enum.GetName(enumType, characteristic);
                        if (name == null) continue;

                        var f = enumType.GetField(name, BindingFlags.Public | BindingFlags.Static);
                        if (f == null) continue;

                        var attr = f.GetCustomAttribute<SolutionCharacteristicScoreWeightsAttribute>();
                        result[characteristic] = attr != null ? attr.Weights : new Dictionary<SolutionKeyFigure, int>();
                    }

                    _ScoreWeightsByCharacteristic = result;
                }

                return _ScoreWeightsByCharacteristic;
            }
        }

        /// <summary>
        /// Normalize value where lower is better; null when range is zero or all data is zero.
        /// </summary>
        private static double? ScoreLowerIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0) return null;
            var range = maximumValue - minimumValue;
            if (range <= 0) return null;
            return Math.Round((1 - (value - minimumValue) / range) * 1000, 2);
        }

        /// <summary>
        /// Normalize value where higher is better; null when range is zero or all data is zero.
        /// </summary>
        private static double? ScoreHigherIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0) return null;
            var range = maximumValue - minimumValue;
            if (range <= 0) return null;
            return Math.Round((value - minimumValue) / range * 1000, 2);
        }

        /// <param name="evaluated">The array of evaluated solution candidate results.</param>
        extension(SolutionCandidateEvaluationResult[] evaluated)
        {
            /// <summary>
            /// Compute characteristic scores as weighted sums of partial scores (normalized higher-is-better).
            /// </summary>
            private void CalculateAndSetCharacteristicScores()
            {
                foreach (var characteristicEntry in ScoreWeightsByCharacteristic)
                {
                    var characteristic = characteristicEntry.Key;
                    var weights = characteristicEntry.Value;
                    var scoresByCandidate = new Dictionary<SolutionCandidateEvaluationResult, double>();

                    foreach (var candidate in evaluated)
                    {
                        double? total = null;

                        foreach (var weight in weights)
                        {
                            var keyFigure = weight.Key;
                            var w = weight.Value;

                            if (candidate.PartialScores.TryGetValue(keyFigure, out var score) && score.HasValue)
                            {
                                total ??= 0;
                                total += score.Value * w;
                            }
                        }

                        if (total.HasValue)
                        {
                            scoresByCandidate[candidate] = total.Value;
                        }
                    }

                    if (scoresByCandidate.Count == 0) continue;

                    var min = scoresByCandidate.Values.Min();
                    var max = scoresByCandidate.Values.Max();
                    var range = max - min;
                    if (range <= 0) continue;

                    foreach (var kvp in scoresByCandidate)
                    {
                        var normalized = ScoreHigherIsBetter(kvp.Value, min, max);
                        if (normalized.HasValue)
                        {
                            kvp.Key.CharacteristicScores[characteristic] = normalized;
                        }
                    }
                }
            }

            /// <summary>
            /// Compute partial scores for each referenced key figure honoring LowerIsBetter or HigherIsBetter.
            /// </summary>
            private void CalculateAndSetPartialScores()
            {
                if (evaluated.Length == 0) return;

                var allKeyFigures = evaluated.SelectMany(s => s.KeyFigures.Keys).Distinct().ToArray();

                foreach (var keyFigure in allKeyFigures)
                {
                    if (!KeyFiguresUsedForAtLeastOneSolutionCharacteristic.Contains(keyFigure))
                        continue;

                    var min = double.MaxValue;
                    var max = double.MinValue;
                    var values = new double[evaluated.Length];

                    for (int i = 0; i < evaluated.Length; i++)
                    {
                        if (evaluated[i].KeyFigures.TryGetValue(keyFigure, out double? v) && v.HasValue)
                        {
                            var val = v.Value;
                            values[i] = val;
                            if (val < min) min = val;
                            if (val > max) max = val;
                        }
                    }

                    var lowerIsBetter = IsLowerIsBetter(keyFigure);

                    for (var i = 0; i < evaluated.Length; i++)
                    {
                        var score = lowerIsBetter
                            ? ScoreLowerIsBetter(values[i], min, max)
                            : ScoreHigherIsBetter(values[i], min, max);

                        if (score.HasValue)
                        {
                            evaluated[i].PartialScores[keyFigure] = score;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the key figure enum field is decorated with <see cref="LowerIsBetterAttribute" />.
        /// </summary>
        private static bool IsLowerIsBetter(SolutionKeyFigure keyFigure)
        {
            var enumType = typeof(SolutionKeyFigure);
            var name = Enum.GetName(enumType, keyFigure);
            if (name == null) return false;

            var field = enumType.GetField(name, BindingFlags.Public | BindingFlags.Static);
            return field != null && field.GetCustomAttributes(typeof(LowerIsBetterAttribute), false).Any();
        }

        #endregion
    }
}