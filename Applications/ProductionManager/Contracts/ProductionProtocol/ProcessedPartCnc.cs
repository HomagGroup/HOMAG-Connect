using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part in a production protocol for CNC machines, including program name and duration.
/// </summary>
public class ProcessedPartCnc : ProcessedPart
{
    /// <summary>
    /// Gets or sets the duration of the CNC program execution for this part.
    /// </summary>
    public TimeSpan? ProgramDuration { get; set; }

    /// <summary>
    /// Gets or sets the name of the CNC program executed for this part.
    /// </summary>
    public string? ProgramName { get; set; }

    /// <inheritdoc />
    public override MachineType MachineType { get; set; } = MachineType.Cnc;
}