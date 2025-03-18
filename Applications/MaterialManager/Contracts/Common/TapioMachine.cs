using System.Diagnostics;

namespace HomagConnect.MaterialManager.Contracts.Common;

/// <summary>
/// Machine which is licensed for the materials api.
/// </summary>
[DebuggerDisplay("{Name}")]
public class TapioMachine
{
    /// <summary>
    /// Gets the name of the machine.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the tapio machine id of the machine.
    /// </summary>
    public string TapioMachineId { get; set; } = string.Empty;
}