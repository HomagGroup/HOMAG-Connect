#nullable enable

using System;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Specifies the evaluation result for a solution candidate.
/// </summary>
public class SolutionCandidateEvaluationResult
{
    /// <summary>
    /// Gets or sets the characteristic assigned to the solution candidate.
    /// </summary>
    public SolutionCharacteristic Characteristic { get; set; } = SolutionCharacteristic.None;

    /// <summary>
    /// Gets or sets the identifier of the solution candidate.
    /// </summary>
    public Guid Id { get; set; }
}