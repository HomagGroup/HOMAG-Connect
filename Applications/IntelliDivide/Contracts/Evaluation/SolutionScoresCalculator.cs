#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    /// <summary>
    /// This static class provides methods to calculate total weighted scores for solution candidates
    /// </summary>
    public static class SolutionScoresCalculator
    {
        private static System.Reflection.PropertyInfo[]? _CachedProperties;

        private static System.Reflection.PropertyInfo[] GetScoredProperties()
        {
            _CachedProperties ??= typeof(SolutionCandidate)
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(PartialScoreTypeAttribute)))
                .ToArray();

            return _CachedProperties;
        }

        /// <summary>
        /// Calculates and assigns the total weighted score for each solution candidate in the collection.
        /// </summary>
        /// <remarks>Each solution candidate's TotalScore property is updated based on the weighted
        /// average of its partial scores. If the sum of weights is zero for a solution, its TotalScore is set to
        /// 0.</remarks>
        /// <param name="solutions">The collection of solution candidates for which to calculate and assign total scores. Cannot be null.</param>
        /// <param name="weights">A dictionary mapping each partial score type to its corresponding weight. Cannot be null.</param>
        public static void CalculateTotalScoreValues(ICollection<SolutionCandidate> solutions, IDictionary<PartialScoreType, int> weights)
        {
            foreach (var solution in solutions)
            {
                var (sumOfWeights, sumOfPartialScores) = CalculateWeightedScoresForSolution(solution, weights);
                solution.TotalScore = sumOfWeights > 0 ? sumOfPartialScores / sumOfWeights : 0;
            }
        }

        private static (double SumOfWeights, double SumOfPartialScores) CalculateWeightedScoresForSolution(
            SolutionCandidate solution,
            IDictionary<PartialScoreType, int> weights)
        {
            double sumOfWeights = 0;
            double sumOfPartialScores = 0;

            var properties = GetScoredProperties();

            foreach (var property in properties)
            {
                var attribute = (PartialScoreTypeAttribute)Attribute.GetCustomAttribute(property, typeof(PartialScoreTypeAttribute));
                if (attribute == null)
                    continue;

                var scoreType = attribute.ScoreType;
                if (!weights.TryGetValue(scoreType, out var weight) || weight == 0)
                    continue;

                var propertyValue = property.GetValue(solution);
                double numericValue = 0;

                if (propertyValue != null)
                {
                    if (propertyValue is double doubleValue)
                    {
                        numericValue = doubleValue;
                    }
                    else if (propertyValue is int intValue)
                    {
                        numericValue = intValue;
                    }
                }

                sumOfWeights += weight;
                sumOfPartialScores += weight * numericValue;
            }

            return (sumOfWeights, sumOfPartialScores);
        }
    }
}
