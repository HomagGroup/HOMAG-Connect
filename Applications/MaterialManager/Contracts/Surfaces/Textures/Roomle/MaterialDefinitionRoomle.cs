namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Root wrapper for a single Roomle material payload in JSON.
/// </summary>
public class MaterialDefinitionRoomle
{
    /// <summary>
    /// The contained material definition with textures, shading, and metadata.
    /// </summary>
    public Material? Material { get; set; }
}