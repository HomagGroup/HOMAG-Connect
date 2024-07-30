namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Laminating properties
/// </summary>
public interface ILaminatingProperties
{
    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the top.
    /// </summary>
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the bottom.
    /// </summary>
    public string? LaminateBottom { get; set; }
}