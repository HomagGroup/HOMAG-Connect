namespace HomagConnect.Base.Contracts.Enumerations;

/// <summary>
/// Represents the type of machine used in the production process.
/// </summary>
public enum MachineType
{
    /// <summary>
    /// Unknown machine type (e.g. not specified or not recognized)
    /// </summary>
    Unknown,

    /// <summary>
    /// Cutting machine (e.g. SAWTEQ)
    /// </summary>
    Cutting,

    /// <summary>
    /// CNC machine (e.g. CENTATEQ)
    /// </summary>
    Cnc
}