using System;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Attribute that accepts an array of string/int pairs as parameters,
/// e.g. [ScoreWeights("TotalCosts", 1000, "MaterialCosts", 500)]
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class SolutionCharacteristicScoreWeightsAttribute : Attribute
{
    /// <summary>
    /// Create score weights from alternating string/int pairs.
    /// </summary>
    /// <param name="nameWeightPairs">Alternating string (name) and int (weight) values.</param>
    public SolutionCharacteristicScoreWeightsAttribute(params object[] nameWeightPairs)
    {
        var dictionary = new Dictionary<string, int>(StringComparer.Ordinal);

        if (nameWeightPairs == null || nameWeightPairs.Length == 0)
        {
            Weights = dictionary;
            return;
        }

        if (nameWeightPairs.Length % 2 != 0)
        {
            throw new ArgumentException("ScoreWeightsAttribute requires an even number of arguments: alternating string name and int weight.");
        }

        for (int i = 0; i < nameWeightPairs.Length; i += 2)
        {
            if (nameWeightPairs[i] is not string name)
            {
                throw new ArgumentException($"Argument at index {i} must be a string (score name).");
            }

            if (nameWeightPairs[i + 1] is not int weight)
            {
                throw new ArgumentException($"Argument at index {i + 1} must be an int (weight) for score '{name}'.");
            }

            dictionary[name] = weight;
        }

        Weights = dictionary;
    }

    /// <summary>
    /// Parsed weights keyed by score name.
    /// </summary>
    public IReadOnlyDictionary<string, int> Weights { get; }
}