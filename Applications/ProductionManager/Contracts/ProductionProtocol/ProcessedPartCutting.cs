using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part that includes cutting-specific details, such as optimization name, pattern information,
/// and board-related data.
/// </summary>
/// <remarks>
/// This class extends <see cref="ProcessedPart" /> to include additional properties specific to the
/// cutting process. It provides details such as the optimization name, pattern name and cycle, board code, and the
/// quantity of boards cut in a book.
/// </remarks>
public class ProcessedPartCutting : ProcessedPart
{
    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    public string? BoardCode { get; set; }

    /// <inheritdoc />
    public override MachineType MachineType { get; set; } = MachineType.Cutting;

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