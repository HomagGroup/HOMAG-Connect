using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Defines the workstation.
/// </summary>
public class Workstation
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the workstation type.
    /// </summary>
    public required WorkstationType WorkstationType { get; set; }
}