using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// CNC-program properties
/// </summary>
public interface ICncProgramProperties
{
    /// <summary>
    /// Gets or sets the program name of the CNC program to execute.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName1))]
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Gets or sets the program name of the CNC program to execute.
    /// </summary>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName2))]

    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// Gets or sets the program name of the CNC program to execute.
    /// </summary>

    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName3))]
    public string? CncProgramName3 { get; set; }
}