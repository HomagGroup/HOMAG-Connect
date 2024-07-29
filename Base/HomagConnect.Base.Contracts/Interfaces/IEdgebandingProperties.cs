namespace HomagConnect.Base.Contracts.Interfaces;

/// <summary>
/// Edgebanding properties
/// </summary>
public interface IEdgebandingProperties
{
    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the back.
    /// </summary>
    public string? EdgeBack { get; set; }

    /// <summary>
    /// Gets or sets how the edgebands should get applied.
    /// </summary>
    public string? EdgeDiagram { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the front.
    /// </summary>
    public string? EdgeFront { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the left.
    /// </summary>
    public string? EdgeLeft { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the right.
    /// </summary>
    public string? EdgeRight { get; set; }
}