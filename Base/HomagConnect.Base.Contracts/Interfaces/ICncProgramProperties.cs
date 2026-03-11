using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Defines CNC program properties for a part.
/// Supports up to three program names that can be assigned for downstream CNC processing.
/// </summary>
public interface ICncProgramProperties
{
    /// <summary>
    /// Gets or sets the name of the first CNC program to execute.
    /// </summary>
    /// <example>Drilling_Main.mpr</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName1))]
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Gets or sets the name of the second CNC program to execute.
    /// </summary>
    /// <example>Milling_Contour.mpr</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName2))]

    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// Gets or sets the name of the third CNC program to execute.
    /// </summary>
    /// <example>Routing_Finish.mpr</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.CncProgramName3))]
    public string? CncProgramName3 { get; set; }
}