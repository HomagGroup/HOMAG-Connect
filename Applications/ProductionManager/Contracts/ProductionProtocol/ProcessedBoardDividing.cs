using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed board in a production protocol for cutting machines.
/// </summary>
public class ProcessedBoardDividing : ProcessedBoard
{
    /// <summary>
    /// Gets or sets the name of optimization (aka run, job, etc.).
    /// </summary>
    public string? OptimizationName { get; set; }

    /// <summary>
    /// Gets or sets the pattern cycle.
    /// </summary>
    public int? PatternCycle { get; set; }

    /// <summary>
    /// Gets or sets the pattern name.
    /// </summary>
    public string? PatternName { get; set; }
}