using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed board in a production protocol for cutting machines.
/// </summary>
public class ProcessedBoardDividing : ProcessedBoard
{
    private readonly MachineType _machineType;

    /// <summary>
    /// Initiantes the MachineType for the dividing process. Allowed types are Cutting and Nesting
    /// </summary>
    /// <param name="machineType"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public ProcessedBoardDividing(MachineType machineType = MachineType.Cutting)
    {
        if (machineType != MachineType.Cutting && machineType != MachineType.Nesting)
            throw new ArgumentOutOfRangeException(nameof(machineType), "MachineType must be Cutting or Nesting.");

        _machineType = machineType;
    }

    public override MachineType MachineType => _machineType;

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