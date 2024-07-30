namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// CNC-program properties
/// </summary>
public interface ICncProgramProperties
{
    /// <summary>
    /// Gets or sets the program name of the CNC program to execute.
    /// </summary>
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Gets or sets the program name of the CNC program to execute.
    /// </summary>
    public string? CncProgramName2 { get; set; }
}