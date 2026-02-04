#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;
using HomagConnect.IntelliDivide.Contracts.Evaluation.Enums;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    public static class SolutionCandidateExtensions
    {
        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionDetails>? solutionDetails)
        {
            var solutionCandidates = SolutionCandidates.From((solutionDetails ?? []).ToArray());
            return solutionDetails == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionCandidates);
        }

        public static SolutionCandidateEvaluationResult[] DetermineCharacteristicsAndDisplayOrder(this IEnumerable<SolutionCandidate>? solutionCandidates)
        {
            return solutionCandidates == null ? [] : DetermineCharacteristicsAndDisplayOrder(solutionCandidates.ToArray());
        }

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

        private static void CalculateRanking(this SolutionCandidateEvaluationResult[] evaluated)
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

        private static void AssignCharacteristics(this SolutionCandidateEvaluationResult[] evaluated)
        {
            foreach (var candidate in evaluated)
            {
                // Determine all characteristics this candidate wins
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

                // Primary characteristic is first win or None

                candidate.Characteristic = wins.Count > 0 ? wins[0] : SolutionCharacteristic.None;

                if (wins.Count > 1)
                {
                    candidate.CharacteristicsInAddition = wins.Skip(1).ToArray();
                }
            }
        }

        private static IReadOnlyDictionary<SolutionCharacteristic, IReadOnlyDictionary<SolutionKeyFigure, int>>? _scoreWeightsByCharacteristic;
        private static SolutionKeyFigure[]? _keyFiguresUsedForAtLeastOneSolutionCharacteristic;

        private static IEnumerable<SolutionKeyFigure> KeyFiguresUsedForAtLeastOneSolutionCharacteristic
        {
            get
            {
                if (_keyFiguresUsedForAtLeastOneSolutionCharacteristic == null)
                {
                    _keyFiguresUsedForAtLeastOneSolutionCharacteristic = ScoreWeightsByCharacteristic
                        .SelectMany(c => c.Value.Keys)
                        .Distinct()
                        .ToArray();
                }

                return _keyFiguresUsedForAtLeastOneSolutionCharacteristic;
            }
        }

        private static IReadOnlyDictionary<SolutionCharacteristic, IReadOnlyDictionary<SolutionKeyFigure, int>> ScoreWeightsByCharacteristic
        {
            get
            {
                if (_scoreWeightsByCharacteristic == null)
                {
                    var result = new Dictionary<SolutionCharacteristic, IReadOnlyDictionary<SolutionKeyFigure, int>>();

                    var enumType = typeof(SolutionCharacteristic);
                    foreach (SolutionCharacteristic characteristic in Enum.GetValues(enumType))
                    {
                        var name = Enum.GetName(enumType, characteristic);
                        if (name == null) continue;

                        var field1 = enumType.GetField(name, BindingFlags.Public | BindingFlags.Static);
                        if (field1 == null) continue;

                        var attr = field1.GetCustomAttribute<SolutionCharacteristicScoreWeightsAttribute>();
                        if (attr != null)
                        {
                            result[characteristic] = attr.Weights;
                        }
                        else
                        {
                            result[characteristic] = new Dictionary<SolutionKeyFigure, int>();
                        }
                    }

                    _scoreWeightsByCharacteristic = result;
                }

                return _scoreWeightsByCharacteristic;
            }
        }

        private static double? ScoreLowerIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0) return null;
            var range = maximumValue - minimumValue;
            if (range <= 0) return null;
            return Math.Round((1 - (value - minimumValue) / range) * 1000, 2);
        }

        private static double? ScoreHigherIsBetter(double value, double minimumValue, double maximumValue)
        {
            if (minimumValue == 0 && maximumValue == 0) return null;
            var range = maximumValue - minimumValue;
            if (range <= 0) return null;
            return Math.Round((value - minimumValue) / range * 1000, 2);
        }

        private static void CalculateAndSetCharacteristicScores(this SolutionCandidateEvaluationResult[] evaluated)
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

        private static void CalculateAndSetPartialScores(this SolutionCandidateEvaluationResult[] evaluated)
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

                for (int i = 0; i < evaluated.Length; i++)
                {
                    double? score = lowerIsBetter
                        ? ScoreLowerIsBetter(values[i], min, max)
                        : ScoreHigherIsBetter(values[i], min, max);

                    if (score.HasValue)
                    {
                        evaluated[i].PartialScores[keyFigure] = score;
                    }
                }
            }
        }

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