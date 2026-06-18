namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

/// <summary>
/// Optional filter criteria for texture queries.
/// All properties are optional; only non-null values are applied.
/// </summary>
public class TextureFilter
{
    /// <summary>Gets or sets the catalog to filter by.</summary>
    public string? Catalog { get; set; }

    /// <summary>Gets or sets the decor code to filter by.</summary>
    public string? DecorCode { get; set; }

    /// <summary>Gets or sets the embossing to filter by.</summary>
    public string? Embossing { get; set; }
}