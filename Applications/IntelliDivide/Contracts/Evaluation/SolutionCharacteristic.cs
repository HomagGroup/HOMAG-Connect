namespace HomagConnect.IntelliDivide.Contracts.Evaluation;

/// <summary>
/// Specifies the characteristics used to evaluate or select a solution in the order to be displayed
/// </summary>
public enum SolutionCharacteristic
{
    /// <summary>
    /// The solution has no special characteristic and should not be displayed.
    /// </summary>
    None,

    /// <summary>
    /// Lowest total costs among the evaluated solutions
    /// </summary>
    LowestTotalCosts,

    /// <summary>
    /// Lowest material costs among the evaluated solutions
    /// </summary>
    LowestMaterialCosts
}